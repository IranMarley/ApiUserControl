using System;
using System.Collections.Generic;
using ApiUserControl.Domain.Models;

namespace ApiUserControl.Domain.Contracts.Repositories
{
    public interface IUserRepository : IDisposable
    {
        User Get(string email);
        User Get(Guid id);
        List<User> Get(int skip, int take);
        void Create(User user);
        void Update(User user);
        void Delete(User user);
        void Delete(string email);
    }
}
