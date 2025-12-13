using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // Direct dependency on concrete DbContext
    private BadDbContext _db = new BadDbContext();

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _db.Products.Include(p => p.Company).ToList();

        if (products.Count == 0)
            return Ok("no data");

        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = _db.Products.Include(p => p.Company)
                                  .FirstOrDefault(p => p.Id == id);

        if (product == null)
            return BadRequest("not found");

        return Ok(product);
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        // Random business rule
        if (product.Price <= 0)
            product.Price = 123;

        var company = _db.Companies.FirstOrDefault(c => c.Id == product.CompanyId);
        if (company == null)
        {
            company = new Company { Name = "Created Automatically" };
            _db.Companies.Add(company);
            _db.SaveChanges();
            product.CompanyId = company.Id;
        }

        _db.Products.Add(product);
        _db.SaveChanges();

        return Ok(product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product input)
    {
        var product = _db.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return Ok("ignored");

        product.Name = input.Name;
        product.Price = input.Price;
        product.CompanyId = input.CompanyId;

        _db.SaveChanges();
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _db.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
        }

        return Ok("maybe deleted");
    }
}
