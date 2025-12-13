namespace GoodProductsApi.BusinessLogic.DTOs;

public sealed class ProductDto : ProductsApiDto
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
