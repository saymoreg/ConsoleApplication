using System;
using Core.Entities;
using Core.Helpers;
using Data.Contexts;

namespace Data
{
	public static class DbInitializer
	{
		static int id;
		public static void SeedAdmins()
		{
			var admins = new List<Admin>
			{
				new Admin
				{
					Id = ++id,
					Username = "admin",
					Password = PasswordHasher.Encrypt("12345"),
					CreatedBy = "System"
				},

				new Admin
				{
					Id = ++id,
					Username = "moderator",
					Password = PasswordHasher.Encrypt("moderator123"),
					CreatedBy = "System"
				}
			};

			DbContext.Admins.AddRange(admins); // to add the admins to our virtual data base
		}
	}
}

