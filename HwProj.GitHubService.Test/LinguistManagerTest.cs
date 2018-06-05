using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HwProj.GitHubService;
using System.IO;

namespace HwProj.GitHubService.Test
{
    [TestClass]
    public class LinguistManagerTest
    {
        [TestMethod]
        public void ShouldReturnCSharpFromExtension()
        {
            //arrange
            const string extension = ".cs";
            const string expectedRes = "csharp";
            //act
            var res = LinguistManager.GetAlias(extension);
            //assert
            Assert.AreEqual(expectedRes, res);
        }

        [TestMethod]
        public void ShouldReturnCPPFromFileName()
        {
            //arrange
            const string fileName = "main.cpp";
            const string expectedRes = "cpp";
            var extension = Path.GetExtension(fileName);
            //act
            var res = LinguistManager.GetAlias(extension);
            //assert
            Assert.AreEqual(expectedRes, res);
        }
    }
}
