using HwProj.GitHubService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Tools
{
    public static class GitHubInstance
    {
        private static GitHubRepositoryStorage storageInstance;
        private static GitHubClient clientInstance;

        public static GitHubRepositoryStorage GetStorageInstance()
        {
            if (storageInstance == null)
            {
                var token = HttpContext.Current.User.Identity.GetGitHubToken();
                storageInstance = new GitHubRepositoryStorage(token);
            }
            return storageInstance;
        }

        public static GitHubClient GetClientInstance()
        {
            if (clientInstance == null)
            {
                var token = HttpContext.Current.User.Identity.GetGitHubToken();
                clientInstance = new GitHubClient(token);
            }
            return clientInstance;
        }
    }
}