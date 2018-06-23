using WebWallet.Domain.Model;
using WebWallet.Domain.Abstract;
using System.Collections.Generic;

namespace WebWallet.Domain.Concrete
{
    public class BlockChainService : IBlockChainService
    {

        protected IBlockChainRepository _blockChainRepository;

        protected IValidationDictionary _validationDictionary;

        public IEnumerable<Block> Blocks => _blockChainRepository.Blocks;

        public IList<Transactions> PendingTransactions
        {
            get
            {
                return _blockChainRepository.PendingTransactions;
            }
            set
            {
                _blockChainRepository.PendingTransactions = value;
            }
        }

        public BlockChainService(IValidationDictionary validationDictionary, IBlockChainRepository blockChainRepository)
        {
            _blockChainRepository = blockChainRepository;
            _validationDictionary = validationDictionary;
        }

        public bool IsBlockValid(Block block)
        {
            if (string.IsNullOrEmpty(block.CurrentHash))
                _validationDictionary.AddError("CurrentHash", "У вас некорректный CurrentHash.");

            if (string.IsNullOrEmpty(block.PreviousHash))
                _validationDictionary.AddError("PreviousHash", "У вас некорректный PreviousHash.");

            if (block.DateTime == null)
                _validationDictionary.AddError("DateTime", "У вас пустой DateTime.");

            if (block.Transactions == null)
                _validationDictionary.AddError("Transactions", "У вас пустой Transactions.");

            return _validationDictionary.IsValid;
        }

        public void AddBlock(Block block)
        {
            if (IsBlockValid(block)) 
            {
                _blockChainRepository.AddBlock(block);
            }
        }

    }
}
