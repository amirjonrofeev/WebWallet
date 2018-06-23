using System;
using System.Linq;
using System.Web.Mvc;
using WebWallet.Domain.Model;
using WebWallet.WebUI.Models;
using WebWallet.Domain.Concrete;
using WebWallet.Domain.Abstract;
using System.Collections.Generic;

namespace WebWallet.WebUI.Controllers
{
    [Authorize]
    public class CabinetController : Controller
    {
        private IUserService _serviceUsers;
        private IBlockChainService _blockChainService;
        private BlockChain _blockChain;

        public CabinetController()
        {
            _serviceUsers = new UserService(new ModelStateWrapper(ModelState), new UserRepository());
            _blockChainService = new BlockChainService(new ModelStateWrapper(ModelState), new BlockChainRepository());
            _blockChain = BlockChain.Instance(_blockChainService);
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                CurrentUser currentUser = new CurrentUser(new ModelStateWrapper(ModelState));
                User user = currentUser.GetCurrentUser(User.Identity.Name);
                user.Money = _blockChain.GetBalanceOfUser(user);

                return View(user);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            if (User.Identity.IsAuthenticated)
            {
                CurrentUser currentUser = new CurrentUser(new ModelStateWrapper(ModelState));
                User user = currentUser.GetCurrentUser(User.Identity.Name);
                user.Money = _blockChain.GetBalanceOfUser(user);

                return View(user);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Support()
        {
            User user = new CurrentUser(new ModelStateWrapper(ModelState)).GetCurrentUser(User.Identity.Name);
            return View(new MoneySend { UserName = user.FirstName, Amount = _blockChain.GetBalanceOfUser(user) });
        }

        public ActionResult TransactionHistory()
        {
            CurrentUser currentUser = new CurrentUser(new ModelStateWrapper(ModelState));
            User user = currentUser.GetCurrentUser(User.Identity.Name);
            IEnumerable<Transactions> transactions = _blockChain.TransactionsHistory(user);

            return View(transactions);
        }

        [HttpGet]
        public ActionResult FullMoney()
        {
            User user = new CurrentUser(new ModelStateWrapper(ModelState)).GetCurrentUser(User.Identity.Name);
            return View(new MoneySend { UserName = user.FirstName, Amount = _blockChain.GetBalanceOfUser(user)});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FullMoney(MoneySend sendMoney)
        {
            User user = new CurrentUser(new ModelStateWrapper(ModelState)).GetCurrentUser(User.Identity.Name);

            if (user != null)
            {
                sendMoney.UserName = user.Login;
                _blockChain.BuyCoins(sendMoney);
            }

            return View(new MoneySend { UserName = user.FirstName, Amount = _blockChain.GetBalanceOfUser(user) });
        }

        [HttpGet]
        public ActionResult SendMoney()
        {
            if (User.Identity.IsAuthenticated)
            {
                User user = new CurrentUser(new ModelStateWrapper(ModelState)).GetCurrentUser(User.Identity.Name);
                return View(new MoneySend { UserName = user.FirstName, Amount = _blockChain.GetBalanceOfUser(user) });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMoney(MoneySend moneySend)
        {
            CurrentUser currentUser = new CurrentUser(new ModelStateWrapper(ModelState));
            User FromUser = currentUser.GetCurrentUser(User.Identity.Name);
            User ToUser = _serviceUsers.ListUsers.Where(u => u.WalletID == moneySend.WalletId).FirstOrDefault();

            if (ToUser != null && ToUser.WalletID != FromUser.WalletID)
            {
                FromUser.Money = _blockChain.GetBalanceOfUser(FromUser);

                if (FromUser.Money >= moneySend.Amount)
                {
                    Transactions transactions = new Transactions
                    {
                        DateTime = DateTime.Now,
                        Amount = moneySend.Amount,
                        FromUser = FromUser.Login,
                        ToUser = ToUser.Login
                    };

                    _blockChain.AddTransaction(transactions);
                    moneySend = new MoneySend { UserName = FromUser.FirstName, Amount = _blockChain.GetBalanceOfUser(FromUser) };
                }
                else
                {
                    ModelState.AddModelError("WalletId", "У вас на счету недостаточно денег!");
                }
            }
            else
            {
                ModelState.AddModelError("WalletId", "К сожелению такого аккаунта нет в базе или вы указали хеш своего же аккаунта.");
            }

            return View(moneySend);
        }
    }
}