using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public CheckUser CheckUser(string userName, string email)
        {
            bool isUserNameExist= _userRepository.IsExistUserName(userName);
            bool isEmailExist = _userRepository.IsExistEmail(email);

            if(isUserNameExist & isEmailExist)
            {
                return ViewModels.CheckUser.UserNameAndEmailNotValid;
            }
            else if (isUserNameExist)
            {
                return ViewModels.CheckUser.UserNameNotValid;
            }
            else if (isEmailExist)
            {
                return ViewModels.CheckUser.EmailNotValid;
            }
            return ViewModels.CheckUser.Ok;
        }

        public int RegisterUser(User user)
        {
            _userRepository.AddUser(user);
            _userRepository.Save();
            return (user.UserId);
        }
    }
}
