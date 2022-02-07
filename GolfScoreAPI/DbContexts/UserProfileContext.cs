using GolfScoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GolfScoreAPI.DbContexts;

public class UserProfileContext : DbContext
{
    public UserProfileContext(DbContextOptions<UserProfileContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfile>().ToTable("Account");
    }

    public DbSet<UserProfile>? UserProfiles { get; set; }
}
