using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using CleanArch.Infra.Data.Context;
using System.Linq;

namespace CleanArch.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UniversityDBContext _ctx;

        public UserRepository(UniversityDBContext ctx)
        {
            this._ctx = ctx;
        }
        public void AddUser(User user)
        {
            _ctx.Users.Add(user);
        }

        public bool IsExistEmail(string email)
        {
            return _ctx.Users.Any(u => u.Email == email);
        }

        public bool ISExistUser(string email, string password)
        {
            return _ctx.Users.Any(u => u.Email == email && u.Password == password);
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
