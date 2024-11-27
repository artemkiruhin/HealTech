namespace HealTech.Application.Dto
{
    public class ProductCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<ProductDto> Products { get; set; } = new();
    }

}
