﻿using HwProj.Models.GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwProj.GitHubService
{
    internal static class Extentions
    {

        public static IEnumerable<Commit> ToCommits(this IReadOnlyList<Octokit.PullRequestCommit> commits)
        {
            return from c in commits
                   select new Commit
                   {
                       Sha = c.Commit.Sha,
                       Owner = new Models.GitHub.User
                       {
                           Login = c.Commit.User.Login,
                           Url = c.Commit.User.Url
                       },
                       CreatedAt = c.Author.Date.DateTime,
                       Title = c.Commit.Message,
                       Url = c.Commit.Url
                   };
        }


        public static PullRequest ToPullRequest(this Octokit.PullRequest pullRequest, IEnumerable<Commit> commits)
        {
            var user = pullRequest.User;
            var diffParser = new DiffParser(pullRequest.Url);
            return new Models.GitHub.PullRequest
            {
                Id = pullRequest.Id,
                DiffUrl = pullRequest.DiffUrl,
                Number = pullRequest.Number,
                Owner = new Models.GitHub.User
                {
                    Login = user.Login,
                    Url = user.Url
                },
                CreatedAt = pullRequest.CreatedAt.DateTime,
                Commits = commits,
                DiffFiles = diffParser.DiffFiles
            };
        }

        public static ReviewComment ToReviewComment(this Octokit.PullRequestReviewComment comment) => new ReviewComment
        {
            Id = comment.Id,
            ReplyToId = comment.InReplyToId,
            Content = comment.Body,
            DiffHunk = comment.DiffHunk,
            Owner = new User
            {
                Login = comment.User.Login,
                Url = comment.User.Url
            },
            Path = comment.Path,
            Position = (int)comment.Position
        };
    }
}