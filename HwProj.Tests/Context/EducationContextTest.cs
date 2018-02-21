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
        private List<int> coursesId = new List<int>();

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
            using (var userContext = new EduContext())
            {
                userContext.Users.Add(user);
                userContext.SaveChanges();                
            }

            //assert
            using (var userContext = new EduContext())
            {
                var actualUser = userContext.Users.Find(user.Id);
                Assert.AreEqual(user.Id, actualUser.Id);
            }
        }

        [TestMethod]
        public void ShouldCreateCourse()
        {
            //arrange
            var course = new Course
            {
                Name = "Matan",
                GroupName = "241"
            };
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "Max",
                Surname = "Vortman",
                Email = "vortmanmax@gmail.com",
                Gender = Models.Enums.Gender.Male,
                UserType = Models.Enums.UserType.Student                
            };
            course.Users.Add(user);
            user.Courses.Add(course);

            usersId.Add(user.Id);
            int courseId;
            //act
            using (var eduContext = new EduContext())
            {
                eduContext.Users.Add(user);
                courseId = eduContext.Courses.Add(course).Id;
                eduContext.SaveChanges();
            }
            coursesId.Add(courseId);
            //assert
            using (var eduContext = new EduContext())
            {
                var actualCourse = eduContext.Courses.Find(courseId);
                Assert.AreEqual(course.Name, actualCourse.Name);
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            using (var userContext = new EduContext())
            {
                foreach (var id in usersId)
                {
                    var u = userContext.Users.Find(id);
                    if (u != null)
                    {
                        userContext.Users.Remove(u);                        
                    }
                }
                foreach (var id in coursesId)
                {
                    var c = userContext.Courses.Find(id);
                    if (c != null)
                    {
                        userContext.Courses.Remove(c);
                    }
                }
                userContext.SaveChanges();
            }
        }
    }
}
