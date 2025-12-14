using GoodProductsApi.DataAccess.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace GoodProductsApi.DataAccess.UnitTests.Persistence.Repositories;

internal abstract class BaseRepositoryTests
{
    protected DbConnection _connection = null!;
    protected DbContextOptions<ProductsDbContext> _dbContextOptions = null!;
    protected List<Company> _companies = [];
    protected List<Product> _products = [];

    [SetUp]
    public void Setup()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        _dbContextOptions = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new ProductsDbContext(_dbContextOptions);
        context.Database.EnsureCreated();

        _companies = new List<Company>
        {
            new() { Id = 1, Name = "Company A" },
            new() { Id = 2, Name = "Company B" },
            new() { Id = 3, Name = "Company C" }
        };
        context.AddRange(_companies);

        foreach (var company in _companies)
        {
            for (int i = 1; i <= 3; i++)
            {
                _products.Add(new Product
                {
                    Id = (company.Id - 1) * 3 + i,
                    Name = $"Product {i} of {company.Name}",
                    Price = 10.0m * i,
                    CompanyId = company.Id
                });
            }
        }
        context.AddRange(_products);

        context.SaveChanges();
    }

    [TearDown]
    public void TearDown()
    {
        _connection.Dispose();
        _companies.Clear();
        _products.Clear();
    }

}
