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
        modelBuilder.Entity<UserProfile>()
            .Property(u => u.Created)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Credential>().HasKey("UserId");
    }

    public DbSet<Credential> Credentials { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

}
