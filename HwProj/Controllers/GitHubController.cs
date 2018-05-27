using HwProj.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HwProj.Tools;
using HwProj.GitHubService;
using HwProj.Filters;
using HwProj.Models.ViewModels;
using System.Threading.Tasks;

namespace HwProj.Controllers
{
    [GitHubAccess]
    public class GitHubController : Controller
    {
        public async Task<ActionResult> FillBranch(string repository)
        {
            var branches = await GitHubInstance.GetStorageInstance().GetBranchesAsync(repository);
            return Json(branches, JsonRequestBehavior.AllowGet);
        }
    }
}