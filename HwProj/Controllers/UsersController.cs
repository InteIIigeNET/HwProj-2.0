using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HwProj.Models;

namespace HwProj.Controllers
{
    public class UsersController : Controller
    {
	    [HttpPost]
	    public bool SignUp(User user)
	    {
		    return true;
	    }

	    [HttpGet]
	    public bool SignIn()
	    {
		    return true;
	    }
	    [HttpGet]
	    public bool Edit()
	    {
		    return true;
	    }
	}
}