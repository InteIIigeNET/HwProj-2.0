using HwProj.Models.GitHub;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    public static class DiffLineParser
    {
        #region Special for Dinara or for me :c
        public const string ADDITION_CSS_CODE_CLASS = "blob-code-addition";
        public const string DELETION_CSS_CODE_CLASS = "blob-code-deletion";
        public const string NORMAL_CSS_CODE_CLASS = "";
        public const string ADDITION_CSS_NUM_CLASS = "blob-num-addition";
        public const string DELETION_CSS_NUM_CLASS = "blob-num-deletion";
        public const string NORMAL_CSS_NUM_CLASS = "";
        #endregion

        private class ParsedDiff
        {
            public int StartNumber { get; set; }
            public string Diff { get; set; }
        }

        public static IEnumerable<DiffLine> GetDiffLines(string diffText, string fileName)
        {
            var parsedDiffs = Parse(diffText + '\n');
            if (parsedDiffs == null)
                return null;
            var codeAlias = LinguistManager.GetAlias(Path.GetExtension(fileName));
            var diffLines = new List<DiffLine>();
            foreach (var parsedDiff in parsedDiffs)
            {
                var lines = parsedDiff.Diff.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                diffLines.Add(CreateDiffLine(lines[0], parsedDiff.StartNumber - 1).SetMarkdownCodeShell(codeAlias));
                for (int i = 0; i < lines.Length - 1; i++)
                {
                    diffLines.Add(CreateDiffLine(lines[i + 1], i + parsedDiff.StartNumber).SetMarkdownCodeShell(codeAlias));
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
                    diffLine.CssCodeClass = ADDITION_CSS_CODE_CLASS;
                    diffLine.CssNumClass = ADDITION_CSS_NUM_CLASS;
                    break;
                case '-':
                    diffLine.CssCodeClass = DELETION_CSS_CODE_CLASS;
                    diffLine.CssNumClass = DELETION_CSS_NUM_CLASS;
                    break;
                default:
                    diffLine.CssCodeClass = NORMAL_CSS_CODE_CLASS;
                    diffLine.CssNumClass = NORMAL_CSS_NUM_CLASS;
                    break;
            }
            return diffLine;
        }

        private static DiffLine SetMarkdownCodeShell(this DiffLine line, string codeAlias)
        {
            if (codeAlias != null)
            {
                line.Line = $"```{codeAlias}\n{line.Line}\n```";
                line.HasMarkdown = true;
            }
            return line;
        }

        private static Regex regex = new Regex($@"\@\@ \-\d+,\d+ \+(\d+),?\d* \@\@[\w|\W]*?\n(?=\@\@|$)", RegexOptions.Compiled);

        private static ParsedDiff[] Parse(string diffText)
        { 
            var matches = regex.Matches(diffText);
            var length = matches.Count;
            if (length == 0)
                return null;
            var parsed = new ParsedDiff[length];
            for (int i = 0; i < length; i++)
            {
                var groups = matches[i].Groups;
                parsed[i] = new ParsedDiff
                {
                    StartNumber = int.Parse(groups[1].Value),
                    Diff = groups[0].Value
                };
            }
            return parsed;
        }
    }
}
