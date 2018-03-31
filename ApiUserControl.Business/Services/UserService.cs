﻿using System;
using System.Collections.Generic;
using ApiUserControl.Common.Resources;
using ApiUserControl.Common.Validation;
using ApiUserControl.Domain.Contracts.Repositories;
using ApiUserControl.Domain.Contracts.Services;
using ApiUserControl.Domain.Models;

namespace ApiUserControl.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Authenticate(string email, string password)
        {
            var user = GetByEmail(email);

            if (user.Password != PasswordAssertionConcern.Encrypt(password))
                throw new Exception(Errors.InvalidCredentials);

            return user;
        }

        public void ChangeInformation(string email, string name)
        {
            var user = GetByEmail(email);

            user.ChangeName(name);
            user.Validate();

            _repository.Update(user);
        }

        public void ChangePassword(string email, string password, string newPassword, string confirmNewPassword)
        {
            var user = Authenticate(email, password);

            user.SetPassword(newPassword, confirmNewPassword);
            user.Validate();

            _repository.Update(user);
        }
        
        public void Register(string name, string email, string password, string confirmPassword)
        {
            var hasUser = _repository.Get(email);
            if (hasUser != null)
                throw new Exception(Errors.DuplicateEmail);

            var user = new User(name, email);
            user.SetPassword(password, confirmPassword);
            user.Validate();

            _repository.Create(user);
        }

        public User GetByEmail(string email)
        {
            var user = _repository.Get(email);
            if (user == null)
                throw new Exception(Errors.UserNotFound);

            return user;
        }

        public string ResetPassword(string email)
        {
            var user = GetByEmail(email);
            var password = user.ResetPassword();
            user.Validate();

            _repository.Update(user);
            return password;
        }

        public List<User> GetByRange(int skip, int take)
        {
            return _repository.Get(skip, take);
        }

        public void Delete(string currentEmail, string email)
        {
            if(currentEmail == email)
                _repository.Delete(email);

            else
                throw new Exception(Errors.UserNotFound);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
