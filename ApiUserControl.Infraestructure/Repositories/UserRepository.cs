using System;
using System.Collections.Generic;
using System.Linq;
using ApiUserControl.Domain.Contracts.Repositories;
using ApiUserControl.Domain.Models;
using ApiUserControl.Infraestructure.Data;

namespace ApiUserControl.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDataContext _context;

        public UserRepository(AppDataContext context)
        {
            _context = context;
        }

        public User Get(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
        }

        public User Get(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> Get(int skip, int take)
        {
            return _context.Users.OrderBy(x => x.Name).Skip(skip).Take(take).ToList();
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void Delete(string email)
        {
            var user = _context.Users.First(f => f.Email == email);
            Delete(user);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
