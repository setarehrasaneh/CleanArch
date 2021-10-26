using CleanArch.Application.ViewModels;
using CleanArch.Domain.Models;

namespace CleanArch.Application.Interfaces
{
    public interface IUserService
    {
        CheckUser CheckUser(string userName, string email);
        int RegisterUser(User user);
    }
}
