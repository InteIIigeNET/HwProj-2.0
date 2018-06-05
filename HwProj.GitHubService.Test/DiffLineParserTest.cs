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
        public void ShouldParseNotEmptyDiffModule()
        {
            //arrange
            string diffText = Resources.DiffModule;
            //act
            var diffLines = DiffLineParser.GetDiffLines(diffText, null);
            //assert
            var first = diffLines.First();
            var last = diffLines.Last();
            Assert.AreEqual(DiffLineParser.NORMAL_CSS_CODE_CLASS, first.CssCodeClass);
            Assert.AreEqual(15, first.Number);
            Assert.AreEqual(DiffLineParser.NORMAL_CSS_CODE_CLASS, last.CssCodeClass);
            Assert.AreEqual(129, last.Number);
        }

        [TestMethod]
        public void ShouldParseEmptyString()
        {
            //arrange
            string diffText = "";
            //act
            var diffLines = DiffLineParser.GetDiffLines(diffText, null);
            //assert
            Assert.AreEqual(diffLines, null);
        }
    }
}
