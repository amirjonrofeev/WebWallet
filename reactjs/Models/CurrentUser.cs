using System.Linq;
using WebWallet.Domain.Model;
using WebWallet.Domain.Abstract;
using WebWallet.Domain.Concrete;

namespace WebWallet.WebUI.Models
{
    public class CurrentUser
    {
        private IUserService _userService;

        public CurrentUser(IValidationDictionary validationDictionary)
        {
            _userService = new UserService(validationDictionary, new UserRepository());
        }

        public User GetCurrentUser(string Name)
        {
            User user = null;
            user = _userService.ListUsers.FirstOrDefault(u => u.Login == Name);

            return user;
        }
    }
}