namespace NuCheck.NuGet
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Represents the content of a packages.config file.
    /// </summary>
    internal class NugetPackageConfig
    {
        /// <summary>
        /// All installed NuGet-packages.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IEnumerable<Package> installedPackages;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetPackageConfig"/> class.
        /// </summary>
        /// <param name="installedPackages">The installed packages.</param>
        private NugetPackageConfig(IEnumerable<Package> installedPackages)
        {
            this.installedPackages = installedPackages;
        }

        /// <summary>
        /// Gets all installed NuGet-Packages.
        /// </summary>
        /// <value>
        /// The installed packages.
        /// </value>
        public IEnumerable<Package> InstalledPackages
        {
            get { return this.installedPackages.ToArray(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetPackageConfig"/> class from a file.
        /// </summary>
        /// <param name="path">The path to the packages.config file.</param>
        /// <returns>The object which represents the NuGet configuration.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="path"/> parameter is <c>null</c>.</exception>
        internal static NugetPackageConfig FromFile(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            using (Stream stream = File.OpenRead(path))
            {
                XDocument doc = XDocument.Load(path);

                return new NugetPackageConfig(doc.Descendants("package").Select(x => new Package(x.Attribute("id").Value, x.Attribute("version").Value)).ToArray());
            }
        }
    }
}
