namespace HealTech.API.RequestModels
{
    public class ProductUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
    }
}
