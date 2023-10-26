using Microsoft.EntityFrameworkCore;

namespace WalletApp.Infrastructure.DataAccess.Database;

public class WalletAppContext : DbContext
{
    public WalletAppContext(DbContextOptions<WalletAppContext> options) : base(options) { }
}