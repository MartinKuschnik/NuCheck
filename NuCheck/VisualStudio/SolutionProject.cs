namespace NuCheck.VisualStudio
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents one of the projects of a <see cref="Solution"/>.
    /// </summary>
    [DebuggerDisplay("{RelativePath,nq}")]
    internal class SolutionProject
    {
        /// <summary>
        /// The project identifier.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Guid id;

        /// <summary>
        /// The type of the project identified over a <see cref="Guid"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Guid type;

        /// <summary>
        /// The name of the project.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string name;

        /// <summary>
        /// The relative path to the project file.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string relativePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionProject"/> class.
        /// </summary>
        /// <param name="id">The project identifier.</param>
        /// <param name="type">The type of the project identified over a <see cref="Guid"/>.</param>
        /// <param name="name">The name of the project.</param>
        /// <param name="relativePath">The relative path to the project file.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="name"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="relativePath"/> parameter is <c>null</c>.</exception>
        public SolutionProject(Guid id, Guid type, string name, string relativePath)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (relativePath == null)
            {
                throw new ArgumentNullException("relativePath");
            }

            this.id = id;
            this.type = type;
            this.name = name;
            this.relativePath = relativePath;
        }

        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public Guid ID
        {
            get { return this.id; }
        }

        /// <summary>
        /// Gets the project type.
        /// </summary>
        /// <value>
        /// The type of the project identified over a <see cref="Guid"/>.
        /// </value>
        public Guid Type
        {
            get { return this.type; }
        }

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        /// <value>
        /// The name of the project.
        /// </value>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets the relative path to the project file.
        /// </summary>
        /// <value>
        /// The relative path to the project file.
        /// </value>
        public string RelativePath
        {
            get { return this.relativePath; }
        }
    }
}
