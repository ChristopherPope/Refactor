using Bogus;
using LoadDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoadDatabase;

internal class Loader
{
    private const int TotalCompanies = 10;
    private const int TotalProductsPerCompany = 15;
    private readonly ProductsContext _db;

    public Loader()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductsContext>();
        optionsBuilder.UseSqlServer("data source=localhost;initial catalog=Products;MultipleActiveResultSets=True;Encrypt=yes;App=EntityFramework;Integrated Security=True;Encrypt=false;");


        _db = new ProductsContext(optionsBuilder.Options);
        Randomizer.Seed = new Random(8675309);
    }

    public void LoadData()
    {
        var companies = new List<Company>();
        for (var i = 0; i < TotalCompanies; i++)
        {
            companies.Add(MakeCompany());
        }
        _db.Companies.AddRange(companies);
        _db.SaveChanges();

        foreach (var company in companies)
        {
            CreateProducts(company);
        }
    }

    private void CreateProducts(Company company)
    {
        var products = new List<Product>();
        for (var i = 0; i <= TotalProductsPerCompany; i++)
        {
            products.Add(MakeProduct(company));
        }

        _db.Products.AddRange(products);
        _db.SaveChanges();
    }

    private Product MakeProduct(Company company)
    {
        return new Faker<Product>()
            .RuleFor(c => c.CompanyId, company.Id)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(c => c.Price, f => f.Finance.Amount(10, 5000));
    }

    private Company MakeCompany()
    {
        return new Faker<Company>()
            .RuleFor(p => p.Name, f => f.Company.CompanyName());
    }
}
