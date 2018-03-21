using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Linq;

namespace HwProj.Tests.Controllers
{

    [TestClass]
    public class AuthtorizationTest
    {
        #region Simple tests
        [TestMethod]
        public void LoginViewResultNotNull()
        {
           
        }

        [TestMethod]
        public void RegisterViewResultNotNull()
        {
           
        }
        #endregion

        private List<string> usersEmails = new List<string>();

        [TestMethod]
        public void ShouldRegisterUser()
        {
           
        }

        [TestCleanup]
        public void CleanDb()
        {
           
        }
    }
}
