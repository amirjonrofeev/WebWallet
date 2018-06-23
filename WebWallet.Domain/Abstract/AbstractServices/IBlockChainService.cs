using WebWallet.Domain.Model;
using System.Collections.Generic;

namespace WebWallet.Domain.Abstract
{
    public interface IBlockChainService
    {
        IEnumerable<Block> Blocks { get; }
        IList<Transactions> PendingTransactions { get; set; }
        void AddBlock(Block block);
        bool IsBlockValid(Block block);
    }
}
