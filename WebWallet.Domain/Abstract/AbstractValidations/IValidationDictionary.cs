using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWallet.Domain.Abstract
{
    public interface IValidationDictionary
    {
        bool IsValid { get; }
        void AddError(string key, string errorMessage);
    }
}
