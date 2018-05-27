using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService.Test
{
    [TestClass]
    public class GitHubClientTest
    {
        const string token = "[token]";

        [TestMethod]
        public void ShouldCreateNewPullRequest()
        {
            //arrange
            const string headBranchName = "MaxVortman-patch-1";
            const string repName = "TestGitHubIntegration";
            const string title = "test2";
            const string owner = "MaxVortman";
            const string baseBranchName = "master";
            var client = new GitHubClient(token);
            //act
            var pr = client.GetNewPullRequestManagerAsync(title, repName, headBranchName, baseBranchName).Result;
            //assert
            Assert.AreEqual(owner, pr.PullRequest.Owner.Login);
        }

        [TestMethod]
        public void ShouldGetPullRequest()
        {
            //arrange
            const string repName = "F_Sharp_Homework";
            const string title = "Phonebook";
            const int number = 15;
            const string owner = "MaxVortman";
            var client = new GitHubClient(token);
            //act
            var pr = client.GetExistPullRequestManagerAsync(repName, number).Result;
            //assert
            Assert.AreEqual(owner, pr.PullRequest.Owner.Login);
            Assert.AreEqual(number, pr.PullRequest.Number);
            Assert.AreEqual(title, pr.PullRequest.Title);
        }
    }
}
