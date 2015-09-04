namespace NuCheck
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using NuGet;
    using VisualStudio;

    public class Program
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string UsageDescription = "Usage: NuCheck target_solution";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string SolutionFileExtension = ".sln";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const char IndentChar = ' ';

        public static int Main(string[] args)
        {
            try
            {
                try
                {
                    Dictionary<string, HashSet<string>> packages = new Dictionary<string, HashSet<string>>();

                    string slnFile = args.LastOrDefault();

                    if (slnFile == null)
                    {
                        throw new ApplicationException(UsageDescription, ApplicationExitCode.InvalidArguments);
                    }

                    if (!Path.GetExtension(slnFile).Equals(SolutionFileExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        slnFile += SolutionFileExtension;
                    }

                    if (!Path.IsPathRooted(slnFile))
                    {
                        slnFile = Path.Combine(Environment.CurrentDirectory, slnFile);
                    }

                    if (!File.Exists(slnFile))
                    {
                        throw new ApplicationException(string.Format("Could not find file '{0}'", slnFile), ApplicationExitCode.FileNotFound);
                    }

                    Solution sln = Solution.FromFile(slnFile);

                    Console.WriteLine();
                    Console.WriteLine("Determining NuGet packages:");

                    foreach (SolutionProject proj in sln.Projects.Where(x => x.Type != SolutionProjectTypes.Folder).OrderBy(x => x.Name))
                    {
                        Console.WriteLine();
                        Console.WriteLine(string.Format("{0}{1}:", new string(IndentChar, 2), proj.Name));
                        Console.WriteLine();

                        string nugetConfigFile = Path.Combine(Path.GetDirectoryName(Path.Combine(Path.GetDirectoryName(slnFile), proj.RelativePath)), "packages.config");

                        NugetPackageConfig nugetPackageConfig;

                        if (File.Exists(nugetConfigFile) && (nugetPackageConfig = NugetPackageConfig.FromFile(nugetConfigFile)).InstalledPackages.Any())
                        {
                            foreach (Package package in nugetPackageConfig.InstalledPackages.OrderBy(x => x.ID).ThenBy(x => x.Version))
                            {
                                Console.WriteLine(string.Format("{0}- {1} [v{2}]", new string(IndentChar, 4), package.ID, package.Version));

                                HashSet<string> versionLookup;

                                if (!packages.TryGetValue(package.ID, out versionLookup))
                                {
                                    versionLookup = new HashSet<string>();
                                    packages.Add(package.ID, versionLookup);
                                }

                                versionLookup.Add(package.Version);
                            }
                        }
                        else
                        {
                            Console.WriteLine(string.Format("{0}  [None]", new string(IndentChar, 4)));
                        }
                    }

                    var packagesWithMultipleVersions = packages.Where(x => x.Value.Count > 1);

                    if (packagesWithMultipleVersions.Any())
                    {
                        StringBuilder sb = new StringBuilder();
                        LogicalStringComparer strComparer = new LogicalStringComparer();

                        sb.Append("Following NuGet packages are installed multiple times in differed versions:");

                        foreach (var packageInfo in packagesWithMultipleVersions.OrderBy(x => x.Key))
                        {
                            sb.AppendLine();
                            sb.AppendLine();
                            sb.AppendLine(string.Format("{0}{1}:", new string(IndentChar, 2), packageInfo.Key));

                            foreach (string version in packageInfo.Value.OrderBy(x => x, strComparer))
                            {
                                sb.AppendLine();
                                sb.Append(string.Format("{0}- v{1}", new string(IndentChar, 4), version));
                            }
                        }

                        throw new ApplicationException(sb.ToString(), ApplicationExitCode.MultipleNuGetPackageVersions);
                    }

                    Console.WriteLine();
                    Console.WriteLine("There are no packages with multiple versions!");

                    return (int)ApplicationExitCode.Succeeded;
                }
                catch (ApplicationException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    return (int)ex.ExitCode;
                }
                catch (Exception)
                {
                    Console.WriteLine();
                    Console.WriteLine("An internal error occurred");
                    return (int)ApplicationExitCode.Failed;
                }
            }
            finally
            {
                Console.WriteLine();

                if (Debugger.IsAttached)
                {
                    Console.Write("Press any key to exit...");
                    Console.ReadKey();
                }
            }
        }
    }
}
