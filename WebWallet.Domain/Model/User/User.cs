using System.ComponentModel.DataAnnotations;

namespace WebWallet.Domain.Model
{
    public class User
    {
        [Key]
        public string WalletID { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public double Money { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }
    }
}
