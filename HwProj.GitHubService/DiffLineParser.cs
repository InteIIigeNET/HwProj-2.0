using HwProj.Models.GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    internal static class DiffLineParser
    {
        public static IEnumerable<DiffLine> GetDiffLines(string diffText)
        {
            var lines = diffText.Split('\n'); //maybe works, maybe not
            var length = lines.Length;
            var diffLines = new List<DiffLine>(length);
            GetStartNumberAndCount(out int startNumber, out int count, lines[0]);
            for (int i = 1; i < length; i++)
            {
                diffLines.Add(CreateDiffLine(lines[i]));
            }
            return diffLines;
        }

        private static void GetStartNumberAndCount(out int start, out int count, string line)
        {
            throw new NotImplementedException();
        }

        private static DiffLine CreateDiffLine(string line)
        {
            throw new NotImplementedException();
        }
    }
}
