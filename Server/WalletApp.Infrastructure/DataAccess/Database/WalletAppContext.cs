using Microsoft.EntityFrameworkCore;
using WalletApp.DAL.Models.Icon;

namespace WalletApp.Infrastructure.DataAccess.Database;

public class WalletAppContext : DbContext
{
    public WalletAppContext(DbContextOptions<WalletAppContext> options) : base(options) { }

    public DbSet<Icon> Icons { get; set; }
}