using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private BadDbContext _db = new BadDbContext();

    [HttpGet]
    public IActionResult Get()
    {
        var result = new List<object>();

        foreach (var c in _db.Companies.Include(c => c.Products))
        {
            result.Add(new
            {
                Company = c.Name,
                Count = c.Products.Count,
                Products = c.Products
            });
        }

        return Ok(result);
    }
}
