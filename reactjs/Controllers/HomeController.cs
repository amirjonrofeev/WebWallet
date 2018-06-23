using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebWallet.Domain.Model;
using WebWallet.Domain.Abstract;
using WebWallet.Domain.Concrete;

namespace WebWallet.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _service;

        public HomeController()
        {
            _service = new UserService(new ModelStateWrapper(ModelState), new UserRepository());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin loginUser)
        {
            if (ModelState.IsValid)
            {
                User user = null;

                user = _service.ListUsers.FirstOrDefault(u => u.Login == loginUser.Login && u.Password == loginUser.Password);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(loginUser.Login, true);
                    return RedirectToAction("Index", "Cabinet");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким именем не существует!");
                }
            }
            return View(loginUser);
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User registerUser)
        {
            if (ModelState.IsValid)
            {
                User user = null;

                user = _service.ListUsers.FirstOrDefault(u => u.Login == registerUser.Login);

                if (user == null)
                {
                    _service.CreateUser(registerUser);

                    user = _service.ListUsers.Where(u => u.Login == registerUser.Login && u.Password == registerUser.Password).FirstOrDefault();

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.Login, true);
                        return RedirectToAction("Index", "Cabinet");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким именем существует!");
                }
            }

            return View(registerUser);
        }

    }
}