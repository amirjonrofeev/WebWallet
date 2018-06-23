using System;
using System.ComponentModel.DataAnnotations;

namespace WebWallet.Domain.Model
{
    public class Transactions
    {
        [Key]
        public int TransactionID { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
    }
}
