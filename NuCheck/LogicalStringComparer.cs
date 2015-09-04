namespace NuCheck
{
    using System.Collections.Generic;

    /// <summary>
    /// Comparer for logical string comparison.
    /// </summary>
    internal class LogicalStringComparer : IComparer<string>
    {
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first string to compare.</param>
        /// <param name="y">The second string to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of x and y, as shown in the
        /// following table.Value Meaning Less than zerox is less than y.Zerox equals y.Greater
        /// than zerox is greater than y.
        /// </returns>
        public int Compare(string x, string y)
        {
            return NativeMethods.StrCmpLogicalW(x, y);
        }
    }
}
