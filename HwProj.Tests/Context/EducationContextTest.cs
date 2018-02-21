using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HwProj.Models;
using HwProj.Models.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HwProj.Tests.Context
{
    [TestClass]
    public class EducationContextTest
    {

        private List<Guid> usersId = new List<Guid>();

        [TestMethod]
        public void ShouldCreateUser()
        {
            //arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Max",
                Surname = "Vortman",
                Email = "vortmanmax@gmail.com",
                Gender = Models.Enums.Gender.Male,
                UserType = Models.Enums.UserType.Student
            };

            usersId.Add(user.Id);

            //act
            using (var userContext = new UserContext())
            {
                userContext.Users.Add(user);
                userContext.SaveChanges();                
            }

            //assert
            using (var userContext = new UserContext())
            {
                var actualUser = userContext.Users.Find(user.Id);
                Assert.AreEqual(user.Id, actualUser.Id);
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            using (var userContext = new UserContext())
            {
                foreach (var id in usersId)
                {
                    var u = userContext.Users.Find(id);
                    if (u != null)
                    {
                        userContext.Users.Remove(u);                        
                    }
                }
                userContext.SaveChanges();
            }
        }
    }
}
