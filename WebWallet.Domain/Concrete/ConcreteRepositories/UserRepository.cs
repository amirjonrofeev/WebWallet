using System;
using System.Linq;
using WebWallet.Domain.Model;
using WebWallet.Domain.Abstract;
using System.Collections.Generic;

namespace WebWallet.Domain.Concrete
{
    public class UserRepository : IUserRepository
    {
        private WebWalletDbContext _entities;

        public UserRepository()
        {
            _entities = new WebWalletDbContext();
        }

        public IEnumerable<User> UserList
        {
            get => _entities.Users.ToList();
        }

        public bool AddUser(User user)
        {
            try
            {
                user.WalletID = Guid.NewGuid().ToString();
                _entities.Users.Add(user);
                _entities.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            _entities.Dispose();
        }

        public void SaveChanges()
        {
            _entities.SaveChanges();
        }
    }
}