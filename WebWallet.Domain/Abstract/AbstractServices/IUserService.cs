using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWallet.Domain.Model;

namespace WebWallet.Domain.Abstract
{
    public interface IUserService
    {
        IEnumerable<User> ListUsers { get; }
        bool CreateUser(User user);
        void SaveChanges();
    }
}
