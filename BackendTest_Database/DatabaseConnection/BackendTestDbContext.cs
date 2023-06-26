using BackendTest_Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendTest_Database.DatabaseConnection;

public class BackendTestDbContext : IdentityDbContext<Student>
{
    public DbSet<Portfolio> Portfolios { get; set; }
    public BackendTestDbContext(DbContextOptions<BackendTestDbContext> options) : base(options)
    { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
