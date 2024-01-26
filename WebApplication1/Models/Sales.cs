namespace WebApplication1.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
