namespace NuCheck.VisualStudio
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents the content of a .sln file.
    /// </summary>
    [DebuggerDisplay("{Name,nq}")]
    internal class Solution
    {
        /// <summary>
        /// The <see cref="Regex"/> object which is used to parse the projects from the .sln file.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly Regex ProjectParsingRegex = new Regex("Project\\(\"({[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}})\"\\)\\s=\\s\"(.+?)\",\\s\"(.+?)\",\\s\"({[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}})", RegexOptions.Multiline | RegexOptions.Compiled);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string name;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable<SolutionProject> projects;

        /// <summary>
        /// Initializes a new instance of the <see cref="Solution"/> class.
        /// </summary>
        /// <param name="name">The name of the <see cref="Solution"/>.</param>
        /// <param name="projects">All projects of the solutions.</param>
        private Solution(string name, IEnumerable<SolutionProject> projects)
        {
            this.name = name;
            this.projects = projects;
        }

        /// <summary>
        /// Gets the name of the solution.
        /// </summary>
        /// <value>
        /// The name of the solution.
        /// </value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <value>
        /// All projects.
        /// </value>
        public IEnumerable<SolutionProject> Projects
        {
            get
            {
                return this.projects.ToArray();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Solution"/> class from a file.
        /// </summary>
        /// <param name="path">The path to the .sln file which should be parsed.</param>
        /// <returns>The created <see cref="Solution"/> instance.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="path"/> parameter is <c>null</c>.</exception>
        internal static Solution FromFile(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            MatchCollection matchCollection = ProjectParsingRegex.Matches(File.ReadAllText(path));

            List<SolutionProject> projects = new List<SolutionProject>(matchCollection.Count);

            foreach (Match item in matchCollection)
            {
                // e.p.: Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "NuCheck", "NuCheck\NuCheck.csproj", "{2DB7BC35-C8FE-4686-82FF-A924309843E6}"
                projects.Add(new SolutionProject(Guid.Parse(item.Groups[4].Value), Guid.Parse(item.Groups[1].Value), item.Groups[2].Value, item.Groups[3].Value));
            }

            return new Solution(Path.GetFileNameWithoutExtension(path), projects);
        }
    }
}
