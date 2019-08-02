using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynet.Infra.Helpers
{
    public class VersionsUtilities
    {
        /// <summary>
        /// Compare two version strings
        ///     If the first version is newer, return 1
        ///     If the second version is newer, return -1
        ///     If both versions are equal, return 0
        /// </summary>
        /// <param name="version1">
        ///     A string containing the major, minor, build, and revision numbers, where each
        ///     number is delimited with a period character ('.').
        /// </param>
        /// <param name="version2">
        ///     A string containing the major, minor, build, and revision numbers, where each
        ///     number is delimited with a period character ('.').
        /// </param>
        /// <returns></returns>
        public int CompareVersions(string version1, string version2)
        {
            Version firstVersion = new Version(version1);
            Version secondVersion = new Version(version2);

            return firstVersion.CompareTo(secondVersion);
        }
    }
}
