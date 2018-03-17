using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HwProj.Models;
using HwProj.Models.Contexts;
using HwProj.Models.Enums;

namespace HwProj.Repository
{
	public class UserRepository : IRepository<User>
	{
		private static Lazy<UserRepository> instanceHolder = 
			new Lazy<UserRepository>(() => new UserRepository());
		public static UserRepository Instance => instanceHolder.Value;

		private UserRepository() { }
		private readonly AuthContext dbContext = new AuthContext();
		public void Dispose()
		{
			dbContext?.Dispose();
		}

		public bool Add(User item)
		{
				dbContext.Users.Add(item);
				dbContext.SaveChanges();

				return  dbContext.Users.FirstOrDefault(u => u.Email == item.Email && 
							  u.EncryptedPassword == item.EncryptedPassword) != null;
		}

		public bool Remove(User item)
		{
			if (dbContext.Users.FirstOrDefault(u => u.Email == item.Email &&
			                  u.EncryptedPassword == item.EncryptedPassword) == null)
				return false;

			dbContext.Users.Remove(item);
			dbContext.SaveChanges();
			return true;
		}

		public User Get(Func<User, bool> check)
		{
			return new User()
			{
				Name = "Michail",
				Surname = "Bondarko",
				Email = "miha.bond@yandex.ru",
				EncryptedPassword = "MatobesIsPTU",
				Gender = Gender.Male
			}; //dbContext.Users.FirstOrDefault(check);
		}

	}
}