using Microsoft.EntityFrameworkCore;

public class BadDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Company> Companies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
            "Server=.;Database=BadProductsDb;Trusted_Connection=True;TrustServerCertificate=True"
        );
    }
}
