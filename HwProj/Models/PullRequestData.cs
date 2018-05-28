using HwProj.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HwProj.Models
{
    [Table("PullRequestsData")]
    public class PullRequestData : IModel
    {
        public PullRequestData(Homework homework, string repName, int pullRequestNumber)
        {
            Homework = homework;
            RepositoryName = repName;
            PullRequestNumber = pullRequestNumber;
            HomeworkId = homework.Id;
        }

        /// <summary>
        /// Идентификатор пула
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// Название репозитория
        /// </summary>
        public string RepositoryName { get; set; }
        /// <summary>
        /// Номер пулл реквеста
        /// </summary>
        public int PullRequestNumber { get; set; }
        /// <summary>
        /// foreign key на дз
        /// </summary>        
        public long HomeworkId { get; set; }

        public Homework Homework { get; set; }
    }
}