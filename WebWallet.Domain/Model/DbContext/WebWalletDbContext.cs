using System.Data.Entity;

namespace WebWallet.Domain.Model
{
    public class WebWalletDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
    }
}