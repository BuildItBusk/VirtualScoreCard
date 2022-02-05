using GolfScoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreAPI.DbContexts;

public class AccountContext : DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().ToTable("Account");
    }

    public DbSet<Account>? Accounts { get; set; }
}
