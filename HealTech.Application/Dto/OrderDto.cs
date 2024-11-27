namespace HealTech.Application.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
    }

}
