namespace HealTech.API.RequestModels
{
    public class ProductAddModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
    }
}
