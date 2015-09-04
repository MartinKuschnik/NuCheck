namespace NuCheck.NuGet
{
    using System.Diagnostics;

    /// <summary>
    /// Represents a NuGet-Package.
    /// </summary>
    [DebuggerDisplay("{ID,nq} {Version,nq}")]
    internal class Package
    {
        /// <summary>
        /// The package identifier.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string id;

        /// <summary>
        /// The version.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string version;

        /// <summary>
        /// Initializes a new instance of the <see cref="Package"/> class.
        /// </summary>
        /// <param name="id">The package identifier.</param>
        /// <param name="version">The version.</param>
        public Package(string id, string version)
        {
            this.id = id;
            this.version = version;
        }

        /// <summary>
        /// Gets the package identifier.
        /// </summary>
        /// <value>
        /// The package identifier.
        /// </value>
        public string ID
        {
            get { return this.id; }
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version
        {
            get { return this.version; }
        }
    }
}
