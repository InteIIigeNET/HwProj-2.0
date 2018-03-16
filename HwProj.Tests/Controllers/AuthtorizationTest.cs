using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HwProj.Controllers;
using System.Web.Mvc;
using HwProj.Models;
using HwProj.Models.Contexts;
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
            var controller = new AuthorizationController();

            var result = controller.LogIn() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RegisterViewResultNotNull()
        {
            var controller = new AuthorizationController();

            var result = controller.Register() as ViewResult;

            Assert.IsNotNull(result);
        }
        #endregion

        private List<string> usersEmails = new List<string>();

        [TestMethod]
        public void ShouldRegisterUser()
        {
            //arrange
            var controller = new AuthorizationController();
            var regModel = new RegisterModel
            {
                Name = "Max",
                Surname = "Vortman",
                Email = "vortmanmax@gmail.com",
                Password = "123",
                ConfirmPassword = "123",
                Gender = Models.Enums.Gender.Male
            };
            usersEmails.Add(regModel.Email);
            //act
            var result = controller.Register(regModel); 
        }

        [TestCleanup]
        public void CleanDb()
        {
            using (var context = new AuthContext())
            {
                foreach (var email in usersEmails)
                {
                    var user = context.Users.FirstOrDefault(u => u.Email == email);

                    if (user != default(User))
                    {
                        context.Users.Remove(user);
                    }
                }                
            }
        }
    }
}
