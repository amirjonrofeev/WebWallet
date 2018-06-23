using WebWallet.Domain.Model;
using WebWallet.Domain.Abstract;
using System.Collections.Generic;

namespace WebWallet.Domain.Concrete
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;

        IValidationDictionary _validationDictionary;

        public UserService(IValidationDictionary modelStateDictionary, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _validationDictionary = modelStateDictionary;
        }

        public IEnumerable<User> ListUsers => _userRepository.UserList;

        protected bool IsUserValid(User user)
        {
            if (string.IsNullOrEmpty(user.Login))
                _validationDictionary.AddError("Login", "Вы указали пустое значения для Login.");

            if (string.IsNullOrEmpty(user.Password))
                _validationDictionary.AddError("Password", "Вы указали пустое значения для Password.");

            if (!user.Password.Equals(user.RepeatPassword))
                _validationDictionary.AddError("Password", "Ваш пароли не совпали!");

            if (string.IsNullOrEmpty(user.Email))
                _validationDictionary.AddError("Email", "Вы указали пустое значения для Email.");

            if (string.IsNullOrEmpty(user.FirstName))
                _validationDictionary.AddError("FirstName", "Вы указали пустое значения для FirstName.");

            if (string.IsNullOrEmpty(user.MiddleName))
                _validationDictionary.AddError("MiddleName", "Вы указали пустое значения для MiddleName.");

            if (string.IsNullOrEmpty(user.LastName))
                _validationDictionary.AddError("LastName", "Вы указали пустое значения для LastName.");

            return _validationDictionary.IsValid;

        }

        public bool CreateUser(User user)
        {
            if (!IsUserValid(user))
            {
                return false;
            }

            try
            {
                _userRepository.AddUser(user);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void SaveChanges()
        {
            _userRepository.SaveChanges();
        }
    }
}
