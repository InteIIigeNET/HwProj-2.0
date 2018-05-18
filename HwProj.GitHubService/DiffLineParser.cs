using HwProj.Models.GitHub;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    internal static class DiffLineParser
    {
        #region Special for Dinara
        private const string ADDITION_CSS_CLASS = "";
        private const string DELETION_CSS_CLASS = "";
        private const string NORMAL_CSS_CLASS = "";
        #endregion

        private class ParsedDiff
        {
            public int StartNumber { get; set; }
            public int Count { get; set; }
            public string Diff { get; set; }
        }

        public static IEnumerable<DiffLine> GetDiffLines(string diffText)
        {
            var parsedDiffs = Parse(diffText);
            var diffLines = new List<DiffLine>();
            foreach (var parsedDiff in parsedDiffs)
            {
                var lines = parsedDiff.Diff.Split('\n');
                diffLines.Add(CreateDiffLine(lines[0], parsedDiff.StartNumber - 1));
                for (int i = 0; i < parsedDiff.Count; i++)
                {
                    diffLines.Add(CreateDiffLine(lines[i + 1], i + parsedDiff.StartNumber));
                }
            }
            return diffLines;
        }

        private static DiffLine CreateDiffLine(string line, int number)
        {
            var diffLine = new DiffLine
            {
                Line = line,
                Number = number
            };
            switch (line[0])
            {
                case '+':
                    diffLine.CssClass = ADDITION_CSS_CLASS;
                    break;
                case '-':
                    diffLine.CssClass = DELETION_CSS_CLASS;
                    break;
                default:
                    diffLine.CssClass = NORMAL_CSS_CLASS;
                    break;
            }
            return diffLine;
        }

        private static Regex regex = new Regex($@"\@\@ \-\d+,\d+ \+(\d+),(\d+) \@\@ [\w|\W]*?\n(?=\@\@|$)", RegexOptions.Compiled);

        private static ParsedDiff[] Parse(string diffText)
        { 
            var matches = regex.Matches(diffText);
            var length = matches.Count;
            var parsed = new ParsedDiff[length];
            for (int i = 0; i < length; i++)
            {
                var groups = matches[i].Groups;
                parsed[i] = new ParsedDiff
                {
                    Count = int.Parse(groups[2].Value),
                    StartNumber = int.Parse(groups[1].Value),
                    Diff = groups[0].Value
                };
            }
            return parsed;
        }
    }
}
