namespace GoodProductsApi.BusinessLogic.DTOs;

public sealed class CompanyDto : ProductsApiDto
{
    public string Name { get; set; } = string.Empty;
    public List<ProductDto> Products { get; set; } = [];
    public int ProductCount => Products.Count;
}
