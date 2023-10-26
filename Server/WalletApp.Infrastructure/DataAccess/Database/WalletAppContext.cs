using Microsoft.EntityFrameworkCore;
using WalletApp.DAL.Models.Icon;
using WalletApp.DAL.Models.Transaction;

namespace WalletApp.Infrastructure.DataAccess.Database;

public class WalletAppContext : DbContext
{
    public WalletAppContext(DbContextOptions<WalletAppContext> options) : base(options) { }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Icon> Icons { get; set; }
}