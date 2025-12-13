namespace GoodProductsApi.BusinessLogic.DTOs;

public sealed class CompanyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProductDto> Products { get; set; } = [];
    public int ProductCount => Products.Count;
}
