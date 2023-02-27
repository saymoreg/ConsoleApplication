using System;
using Core.Entities;
using Core.Helpers;
using Data.Contexts;
using Data.Repositories.Abstract;

namespace Data.Repositories.Concrete
{
    public class AdminRepository : IAdminRepository
    {
        public Admin GetUsernameAndPassword(string username, string password)
        {
            return DbContext.Admins.FirstOrDefault(a => a.Username.ToLower() == username.ToLower() && PasswordHasher.Decrypt(a.Password) == password.ToLower());
        }
    }
}

