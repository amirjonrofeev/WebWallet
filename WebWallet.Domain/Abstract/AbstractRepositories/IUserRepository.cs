using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWallet.Domain.Model;

namespace WebWallet.Domain.Abstract
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> UserList { get; }
        bool AddUser(User user);
        void SaveChanges();
    }
}
