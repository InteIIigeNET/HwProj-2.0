using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HwProj.Models.Contexts
{
	public class AuthContext : BaseContext
	{
        
        /// <summary>
        /// Пользователи (студенты и преподаватели)
        /// </summary>
        public DbSet<User> Users { get; set; }
       
    }
}