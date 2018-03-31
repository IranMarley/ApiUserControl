using System;
using System.Collections.Generic;
using ApiUserControl.Domain.Models;

namespace ApiUserControl.Domain.Contracts.Services
{
    public interface IUserService : IDisposable
    {
        User Authenticate(string email, string password);
        User GetByEmail(string email);
        void Register(string name, string email, string password, string confirmPassword);
        void ChangeInformation(string email, string name);
        void ChangePassword(string email, string password, string newPassword, string confirmNewPassword);
        string ResetPassword(string email);
        List<User> GetByRange(int skip, int take);
        void Delete(string currentEmail, string email);
    }
}
