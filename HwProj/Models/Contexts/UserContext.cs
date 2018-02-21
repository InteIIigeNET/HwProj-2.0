using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HwProj.Models.Contexts
{
    public class UserContext
    {
        private IEnumerable<User> students;
        public IEnumerable<User> Students
        {
            get
            {
                if (students != null)
                    return students;

                using (var eduContext = new EducationContext())
                {
                    return from u in eduContext.Users
                           where u.UserType == Enums.UserType.Student
                           select u;
                }
            }
        }
    }
}