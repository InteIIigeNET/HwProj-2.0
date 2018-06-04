using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HwProj.GitHubService;

namespace HwProj.GitHubService.Test
{
    [TestClass]
    public class LinguistManagerTest
    {
        [TestMethod]
        public void ShouldReturnCSharp()
        {
            //arrange
            const string extension = ".cs";
            const string expectedRes = "csharp";
            //act
            var res = LinguistManager.GetAlias(extension);
            //assert
            Assert.AreEqual(expectedRes, res);
        }
    }
}
