using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Contexts
{
    public class UserContext
    {
        /// <summary>
        /// Все зарегистрированные студенты
        /// </summary>
        public IEnumerable<User> Students
        {
            get
            {
                using (var eduContext = new EducationContext())
                {
                    return from u in eduContext.Users
                           where u.UserType == Enums.UserType.Student
                           select u;
                }
            }
        }
        /// <summary>
        /// Все зарегистрированные преподаватели
        /// </summary>
        public IEnumerable<User> Teachers
        {
            get
            {
                using (var eduContext = new EducationContext())
                {
                    return from u in eduContext.Users
                           where u.UserType == Enums.UserType.Teacher
                           select u;
                }
            }
        }
    }
}