using System;
using System.IO;
using System.Linq;
using HwProj.GitHubService.Test.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HwProj.GitHubService.Test
{
    [TestClass]
    public class DiffLineParserTest
    {
        [TestMethod]
        public void ShouldParseNotEmptyDiff()
        {
            //arrange
            string diffText = Resources.DiffModule;
            //act
            var diffLines = DiffLineParser.GetDiffLines(diffText);
            //assert
            var first = diffLines.First();
            var last = diffLines.Last();
            Assert.AreEqual(DiffLineParser.NORMAL_CSS_CLASS, first.CssClass);
            Assert.AreEqual(15, first.Number);
            Assert.AreEqual(DiffLineParser.NORMAL_CSS_CLASS, last.CssClass);
            Assert.AreEqual(129, last.Number);
        }
    }
}
