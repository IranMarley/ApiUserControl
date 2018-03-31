using System;
using ApiUserControl.Common.Resources;
using ApiUserControl.Common.Validation;

namespace ApiUserControl.Domain.Models
{
    public class User
    {
        #region Constructor

        protected User() { }
        public User(string name, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
        }

        #endregion

        #region Properties

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        #endregion

        #region Methods

        public void SetPassword(string password, string confirmPassword)
        {
            AssertionConcern.AssertArgumentNotNull(password, Errors.InvalidUserPassword);
            AssertionConcern.AssertArgumentNotNull(confirmPassword, Errors.InvalidUserPassword);
            AssertionConcern.AssertArgumentLength(password, 6, 20, Errors.InvalidUserPassword);
            AssertionConcern.AssertArgumentEquals(password, confirmPassword, Errors.PasswordDoesNotMatch);

            Password = PasswordAssertionConcern.Encrypt(password);
        }
        public string ResetPassword()
        {
            var password = Guid.NewGuid().ToString().Substring(0, 8);
            Password = PasswordAssertionConcern.Encrypt(password);

            return password;
        }
        public void ChangeName(string name)
        {
            Name = name;
        }
        public void Validate()
        {
            AssertionConcern.AssertArgumentLength(Name, 3, 250, Errors.InvalidUserName);
            EmailAssertionConcern.AssertIsValid(Email);
            PasswordAssertionConcern.AssertIsValid(Password);
        }

        #endregion
    }
}
