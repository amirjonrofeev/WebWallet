using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebWallet.Domain.Model;
using WebWallet.WebUI.Models;
using WebWallet.Domain.Abstract;
using WebWallet.Domain.Concrete;
using System.Collections.Generic;
using WebWallet.Tests.WebUI.Tests.AdditionalClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebWallet.Tests.WebUI.Tests
{
    [TestClass]
    public class BlockChainTests
    {
        [TestMethod]
        public void AddTransaction_ValidAdding()
        {
            WebWalletDbContext webWalletDbContext = new WebWalletDbContext();

            int previouseTransactionAmount = webWalletDbContext.Transactions.Count(), afterTransactionAmount;

            ModelStateDictionary modelState = new ModelStateDictionary();

            IBlockChainService blockChainService = new BlockChainService(new ModelStateWrapper(modelState), new BlockChainRepository());

            BlockChain blockChain = BlockChain.Instance(blockChainService);

            Transactions transactions = new Transactions { Amount = 122, DateTime = DateTime.Now, FromUser = "User1", ToUser = "User2", TransactionID = 12 };

            blockChain.AddTransaction(transactions);

            Thread.Sleep(35000);

            afterTransactionAmount = webWalletDbContext.Transactions.Count();

            Assert.AreNotEqual(previouseTransactionAmount, afterTransactionAmount);
        }

        [TestMethod]
        public void AddTransaction_MultyValidAdding()
        {
            WebWalletDbContext webWalletDbContext = new WebWalletDbContext();

            int previouseTransactionAmount = webWalletDbContext.Transactions.Count(), afterTransactionAmount;

            ModelStateDictionary modelState = new ModelStateDictionary();

            IBlockChainService blockChainService = new BlockChainService(new ModelStateWrapper(modelState), new BlockChainRepository());

            var addingTransactions = new Task[100];

            for (int i = 0; i < addingTransactions.Length; i++)
            {
                addingTransactions[i] = new Task(new Action(
                    () =>
                    {
                        BlockChain blockChain = BlockChain.Instance(blockChainService);

                        Transactions transactions = new Transactions { Amount = 122, DateTime = DateTime.Now, FromUser = "User1", ToUser = "User2", TransactionID = 12 };

                        blockChain.AddTransaction(transactions);
                    }
                    ));

                addingTransactions[i].Start();
            }

            Thread.Sleep(35000);

            afterTransactionAmount = webWalletDbContext.Transactions.Count();

            Assert.AreNotEqual(previouseTransactionAmount, afterTransactionAmount);
        }

        [TestMethod]
        public void TransactionsHistory_ValidValues()
        {
            bool workProccess = false;

            IBlockChainRepository blockChainRepository = new BlockChainRepository();

            ModelStateDictionary modelState = new ModelStateDictionary();

            IBlockChainService blockChainService = new BlockChainService(new ModelStateWrapper(modelState), blockChainRepository);

            BlockChain blockChain = BlockChain.Instance(blockChainService);

            User user = new User
            {
                Email = "AmirRofeev@mail.ru",
                FirstName = "Amir",
                LastName = "Akobirovich",
                MiddleName = "Rofeev",
                Login = "User1",
                Money = 0,
                Password = "2125954",
                RepeatPassword = "2125954",
                WalletID = "WalletID"
            };

            blockChain.AddTransaction(new Transactions { Amount = 1231, DateTime = DateTime.Now, FromUser = user.Login, ToUser = "User2", TransactionID = 13 });

            blockChain.AddTransaction(new Transactions { Amount = 32, DateTime = DateTime.Now, FromUser = "User2", ToUser = user.Login, TransactionID = 14 });

            MoneySend moneySend = new MoneySend { Amount = 100, UserName = user.Login, WalletId = user.WalletID };

            blockChain.BuyCoins(moneySend);

            Thread.Sleep(35000);

            IEnumerable<Transactions> transactionsHistory = blockChain.TransactionsHistory(user);

            foreach (var transaction in transactionsHistory)
                workProccess = Equals(transaction.FromUser, user.Login) || Equals(transaction.ToUser, user.Login);

            Assert.IsTrue(workProccess);
        }

        [TestMethod]
        public void TransactionsHistory_MultyValidValues()
        {
            bool workProccess = false;

            IBlockChainRepository blockChainRepository = new BlockChainRepository();

            ModelStateDictionary modelState = new ModelStateDictionary();

            IBlockChainService blockChainService = new BlockChainService(new ModelStateWrapper(modelState), blockChainRepository);

            BlockChain blockChain = BlockChain.Instance(blockChainService);

            var users = new User[100];

            for (int i = 0; i < users.Length; i++)
            {
                users[i] = new User
                {
                    Email = "AmirRofeev@mail.ru",
                    FirstName = "Amir",
                    LastName = "Akobirovich",
                    MiddleName = "Rofeev",
                    Login = new Random().GetHashCode().ToString(),
                    Money = 0,
                    Password = "2125954",
                    RepeatPassword = "2125954",
                    WalletID = "WalletID"
                };
            }

            var usersActions = new Task[100];

            for (int i = 0; i < users.Length; i++)
            {
                Task.Run(() =>
                {
                    blockChain.AddTransaction(new Transactions { Amount = 1231, DateTime = DateTime.Now, FromUser = users[i].Login, ToUser = "User2", TransactionID = 13 });

                    blockChain.AddTransaction(new Transactions { Amount = 32, DateTime = DateTime.Now, FromUser = "User2", ToUser = users[i].Login, TransactionID = 14 });

                    MoneySend moneySend = new MoneySend { Amount = 100, UserName = users[i].Login, WalletId = users[i].WalletID };

                    blockChain.BuyCoins(moneySend);

                }).Wait();

            }

            Thread.Sleep(35000);

            foreach (var user in users)
            {
                IEnumerable<Transactions> transactionsHistory = blockChain.TransactionsHistory(user);

                foreach (var transaction in transactionsHistory)
                    workProccess = Equals(transaction.FromUser, user.Login) || Equals(transaction.ToUser, user.Login);
            }

            Assert.IsTrue(workProccess);
        }

        [TestMethod]
        public void GetBalanceOfUser_ValidValue()
        {
            ModelStateDictionary modelState = new ModelStateDictionary();

            User user = new User
            {
                Email = "AmirRofeev@mail.ru",
                FirstName = "Amir",
                LastName = "Akobirovich",
                MiddleName = "Rofeev",
                Login = "User1",
                Money = 0,
                Password = "2125954",
                RepeatPassword = "2125954",
                WalletID = "WalletID"
            };

            double predictableBalanceOfUser = 0;

            IBlockChainService blockChainService = new BlockChainService(new ModelStateWrapper(modelState), new BlockChainRepository());

            BlockChain blockChain = BlockChain.Instance(blockChainService);

            MoneySend moneySend = new MoneySend { Amount = 100, UserName = user.Login, WalletId = user.WalletID };

            blockChain.BuyCoins(moneySend);

            predictableBalanceOfUser = 100;

            blockChain.AddTransaction(new Transactions { Amount = 50, DateTime = DateTime.Now, FromUser = user.Login, ToUser = "User2", TransactionID = 12 });

            predictableBalanceOfUser -= 50;

            Thread.Sleep(35000);

            Assert.AreEqual(blockChain.GetBalanceOfUser(user), predictableBalanceOfUser);
        }

        [TestMethod]
        public void GetBalanceOfUser_MultyValidValue()
        {
            ModelStateDictionary modelState = new ModelStateDictionary();

            IBlockChainService blockChainService = new BlockChainService(new ModelStateWrapper(modelState), new BlockChainRepository());

            BlockChain blockChain = BlockChain.Instance(blockChainService);

            var users = new User[100];

            for (int i = 0; i < users.Length; i++)
            {
                users[i] = new User
                {
                    Email = "AmirRofeev@mail.ru",
                    FirstName = "Amir",
                    LastName = "Akobirovich",
                    MiddleName = "Rofeev",
                    Login = new Random().GetHashCode().ToString(),
                    Money = 0,
                    Password = "2125954",
                    RepeatPassword = "2125954",
                    WalletID = "WalletID"
                };
            }

            var usersActions = new Task[100];

            for (int i = 0; i < users.Length; i++)
            {
                Task.Run(() =>
                {
                    MoneySend moneySend = new MoneySend { Amount = 100, UserName = users[i].Login, WalletId = users[i].WalletID };

                    blockChain.BuyCoins(moneySend);

                    blockChain.AddTransaction(new Transactions { Amount = 50, DateTime = DateTime.Now, FromUser = users[i].Login, ToUser = "BlockChainCompany", TransactionID = 12 });
                }).Wait();

            }

            Thread.Sleep(35000);

            foreach (var user in users)
                Assert.AreNotEqual(blockChain.GetBalanceOfUser(user), 0);
        }

        [TestMethod]
        public void BuyCoins_ValidValue()
        {
            ModelStateDictionary modelState = new ModelStateDictionary();

            User user = new User
            {
                Email = "AmirRofeev@mail.ru",
                FirstName = "Amir",
                LastName = "Akobirovich",
                MiddleName = "Rofeev",
                Login = "User1",
                Money = 0,
                Password = "2125954",
                RepeatPassword = "2125954",
                WalletID = "WalletID"
            };

            double sendMoney = 143.3;

            IBlockChainService blockChainService = new BlockChainService(new ModelStateWrapper(modelState), new BlockChainRepository());

            BlockChain blockChain = BlockChain.Instance(blockChainService);

            MoneySend moneySend = new MoneySend { Amount = sendMoney, UserName = user.Login, WalletId = user.WalletID };

            blockChain.BuyCoins(moneySend);

            Thread.Sleep(35000);

            Assert.AreEqual(blockChain.GetBalanceOfUser(user), sendMoney);
        }

        [TestMethod]
        public void BuyCoins_MultyValidValue()
        {
            ModelStateDictionary modelState = new ModelStateDictionary();

            var users = new User[100];

            var usersAction = new Task[100];

            double sendMoney = 143.3;

            for (int i = 0; i < users.Length; i++)
            {
                users[i] = new User
                {
                    Email = "AmirRofeev@mail.ru",
                    FirstName = "Amir",
                    LastName = "Akobirovich",
                    MiddleName = "Rofeev",
                    Login = new Random().GetHashCode().ToString(),
                    Money = 0,
                    Password = "2125954",
                    RepeatPassword = "2125954",
                    WalletID = "WalletID"
                };
            }


            IBlockChainService blockChainService = new BlockChainService(new ModelStateWrapper(modelState), new BlockChainRepository());

            BlockChain blockChain = BlockChain.Instance(blockChainService);

            for (int i = 0; i < users.Length; i++)
            {
                Task.Run(() =>
                {
                    MoneySend moneySend = new MoneySend { Amount = sendMoney, UserName = users[i].Login, WalletId = users[i].WalletID };

                    blockChain.BuyCoins(moneySend);

                }).Wait();
            }

            Thread.Sleep(35000);

            foreach (var user in users)
                Assert.AreEqual(blockChain.GetBalanceOfUser(user), sendMoney);

        }

        [TestMethod]
        public void MultyThreadUsersConnecting_ValidTest()
        {
            var tasks = new Task<int>[1000];
            int previouseResult = 0;

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task<int>(IntializeBlockChain);
                tasks[i].Start();

                if (i > 0)
                    Assert.AreEqual(previouseResult, tasks[i].Result);

                previouseResult = tasks[i].Result;
            }
        }

        [TestMethod]
        public void MultyThreadUsersConnecting_InValidTest()
        {
            var tasks = new Task<int>[1000];
            int previouseResult = 0;

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task<int>(WrongIntializeBlockChain);
                tasks[i].Start();

                if (i > 0)
                    Assert.AreNotEqual(previouseResult, tasks[i].Result);

                previouseResult = tasks[i].Result;
            }
        }

        public int IntializeBlockChain()
        {
            ModelStateDictionary modelState = new ModelStateDictionary();

            IBlockChainService blockChainService = new BlockChainService(new ModelStateWrapper(modelState), new BlockChainRepository());

            BlockChain blockChain = BlockChain.Instance(blockChainService);

            return blockChain.GetHashCode();
        }

        public int WrongIntializeBlockChain()
        {
            ModelStateDictionary modelState = new ModelStateDictionary();

            IBlockChainService blockChainService = new BlockChainService(new ModelStateWrapper(modelState), new BlockChainRepository());

            FakeBlockChain blockChain = new FakeBlockChain();

            return blockChain.GetHashCode();
        }

    }
}
