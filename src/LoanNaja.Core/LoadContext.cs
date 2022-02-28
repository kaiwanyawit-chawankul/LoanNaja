using LoanNaja.Core;
using Microsoft.EntityFrameworkCore;

public class LoanContext : DbContext
{
    public DbSet<Loan> Loans { get; set; }

    public LoanContext(DbContextOptions<LoanContext> options) : base(options)
    {
        // Creates the database !! Just for DEMO !! in production code you have to handle it differently!
        this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseSqlite(
        //     "Filename=TestDatabase.db",
        //     options =>
        //     {
        //         //options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        //     }
        // );
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder
        //     .Entity<Loan>()
        //     .HasData(
        //         new Loan(
        //             Guid.Parse("78c1c142-98c1-4101-b1ff-d495f65d1d33"),
        //             "TWSEA",
        //             2000,
        //             DateTime.Parse("2022-02-02"),
        //             30
        //         ),
        //         new Loan(
        //             Guid.Parse("6f9eebb4-621d-4eae-b47e-fc6ff1ca760b"),
        //             "TWSEA",
        //             1000,
        //             DateTime.Parse("2022-12-11"),
        //             100
        //         ),
        //         new Loan(
        //             Guid.Parse("c645b79a-90a2-45ea-ac5c-1200eba3cbe1"),
        //             "TWSEA",
        //             3000,
        //             DateTime.Parse("2022-11-02"),
        //             365
        //         )
        //     );
    }
}
