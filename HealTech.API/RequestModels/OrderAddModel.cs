namespace HealTech.API.RequestModels
{
    public class OrderAddModel
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        
    }
}
