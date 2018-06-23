using System.Linq;
using WebWallet.Domain.Model;
using WebWallet.Domain.Abstract;
using System.Collections.Generic;

namespace WebWallet.Domain.Concrete
{
    public class BlockChainRepository : IBlockChainRepository
    {
        protected WebWalletDbContext entities;

        public BlockChainRepository()
        {
            entities = new WebWalletDbContext();
            PendingTransactions = new List<Transactions>();
        }

        public IEnumerable<Block> Blocks => entities.Blocks.ToList();

        public IList<Transactions> PendingTransactions { get; set; }

        public void AddBlock(Block block)
        {
            entities.Blocks.Add(block);
            entities.SaveChangesAsync();
        }
    }
}
