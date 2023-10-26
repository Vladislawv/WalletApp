using Microsoft.EntityFrameworkCore;
using WalletApp.DAL.Models.Cards;
using WalletApp.DAL.Models.Icons;
using WalletApp.DAL.Models.Transactions;

namespace WalletApp.Infrastructure.DataAccess.Database;

public class WalletAppContext : DbContext
{
    public WalletAppContext(DbContextOptions<WalletAppContext> options) : base(options) { }

    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Icon> Icons { get; set; }
}