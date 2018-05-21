using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService.Test
{
    [TestClass]
    public class DiffParserTest
    {
        [TestMethod]
        public void ShouldParseDiffPage()
        {
            //arrange
            const string pageUri = @"https://patch-diff.githubusercontent.com/raw/MaxVortman/F_Sharp_Homework/pull/15.diff";
            const string firstFileName = "F_Sharp_Homework.sln";
            const string lastFileName = "Phonebook(Task5)/UIFactory.fs";
            const int count = 9;
            var diffParser = new DiffParser(pageUri);
            //act
            var files = diffParser.DiffFiles;
            //assert
            Assert.AreEqual(count, files.Count());
            var first = files.First();
            var last = files.Last();
            Assert.AreEqual(firstFileName, first.Name);
            Assert.AreEqual(lastFileName, last.Name);
        }
    }
}
