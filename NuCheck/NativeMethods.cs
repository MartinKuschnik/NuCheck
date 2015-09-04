namespace NuCheck
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Static class whit native methods.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// Compares two Unicode strings. Digits in the strings are considered as numerical content rather than text. This test is not case-sensitive.
        /// </summary>
        /// <param name="x">The first string to be compared.</param>
        /// <param name="y">The second string to be compared.</param>
        /// <returns>
        /// Returns zero if the strings are identical.
        /// <para />
        /// Returns 1 if the string pointed to by psz1 has a greater value than that pointed to by psz2.
        /// <para />
        /// Returns -1 if the string pointed to by psz1 has a lesser value than that pointed to by psz2.
        /// </returns>
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int StrCmpLogicalW(string x, string y);
    }
}
