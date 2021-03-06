using CleanArch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Domain.Interfaces
{
    public interface IUserRepository
    {
        bool ISExistUser(string email, string password);
        void AddUser(User user);
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        void Save();

    }
}
