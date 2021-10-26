using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanArch.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UniversityDBContext _ctx;

        public UserRepository(UniversityDBContext ctx )
        {
            this._ctx = ctx;
        }
        public void AddUser(User user)
        {
            _ctx.Users.Add(user);
        }

        public bool IsExistEmail(string email)
        {
            return _ctx.Users.Any(u=> u.Email == email);
        }

        public bool IsExistUserName(string userName)
        {
            return _ctx.Users.Any(u => u.UserName == userName);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
