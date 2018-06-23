using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebWallet.Domain.Model
{
    public class Block
    {
        [Key]
        public int BlockID { get; set; }
        public IList<Transactions> Transactions { get; set; }
        public DateTime DateTime { get; set; }
        public string PreviousHash { get; set; }
        public string CurrentHash { get; set; }
    }
}
