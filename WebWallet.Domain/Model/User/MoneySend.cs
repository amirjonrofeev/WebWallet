namespace WebWallet.Domain.Model
{
    public class MoneySend
    {
        public string UserName { get; set; }
        public string WalletId { get; set; }
        public double Amount { get; set; }
    }
}
