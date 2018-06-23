using System;
using System.Linq;
using System.Threading;
using WebWallet.Domain.Model;
using System.Threading.Tasks;
using WebWallet.Domain.Abstract;
using System.Collections.Generic;

namespace WebWallet.WebUI.Models
{
    public class BlockChain
    {
        protected IBlockChainService _blockChainService;

        protected static volatile BlockChain instance;

        protected static object syncRoot = new object();

        protected BlockChain(IBlockChainService blockChainService)
        {
            _blockChainService = blockChainService;

            if (_blockChainService.Blocks.Count() == 0)
                CreateGenesisBlock();

            MiningAsync();
        }

        public static BlockChain Instance(IBlockChainService blockChainService)
        {
            if (instance == null)
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new BlockChain(blockChainService);
                }

            return instance;
        }

        public IEnumerable<Transactions> TransactionsHistory(User user)
        {
            IEnumerable<Transactions> transactions = null;

            try
            {
                foreach (var block in _blockChainService.Blocks)
                {
                    if (transactions == null)
                        try
                        {
                            transactions = block.Transactions.Where(t => t.ToUser == user.Login || t.FromUser == user.Login);
                        }
                        catch
                        {

                        }
                    else
                        transactions = block.Transactions.Where(t => t.ToUser == user.Login || t.FromUser == user.Login).Concat(transactions);
                }
            }
            catch
            {

            }

            return transactions;
        }

        public double GetBalanceOfUser(User user)
        {
            double money = 0;
            try
            {
                foreach (var block in _blockChainService.Blocks)
                    foreach (var transaction in block.Transactions)
                    {
                        if (transaction.FromUser == user.Login)
                            money -= transaction.Amount;

                        if (transaction.ToUser == user.Login)
                            money += transaction.Amount;
                    }

            }
            catch
            {

            }

            return money;
        }

        public void BuyCoins(MoneySend moneySend)
        {
            _blockChainService.PendingTransactions.Add(new Transactions { Amount = moneySend.Amount, DateTime = DateTime.Now, FromUser = "BlockChainCompany", ToUser = moneySend.UserName });
        }

        public void AddTransaction(Transactions transactions)
        {
            _blockChainService.PendingTransactions.Add(transactions);
        }

        private void AddBlock(Block block)
        {
            _blockChainService.AddBlock(block);
        }

        private void CreateGenesisBlock()
        {
            AddBlock(new Block { CurrentHash = CreateHash(), PreviousHash = "0000", DateTime = DateTime.Now, Transactions = _blockChainService.PendingTransactions });
        }

        private void Mining()
        {
            while (true)
            {
                Thread.Sleep(30000);
                if (_blockChainService.PendingTransactions.Count != 0)
                {
                    IList<Transactions> transactions = _blockChainService.PendingTransactions.ToList();
                    _blockChainService.PendingTransactions.Clear();
                    AddBlock(new Block { CurrentHash = CreateHash(), PreviousHash = _blockChainService.Blocks.LastOrDefault().CurrentHash, DateTime = DateTime.Now, Transactions = transactions });
                }
            }
        }

        private void MiningAsync()
        {
            Task task = new Task(Mining);
            task.Start();
        }

        private string CreateHash()
        {
            return Guid.NewGuid().ToString();
        }
    }
}