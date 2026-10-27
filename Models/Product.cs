
namespace InventoryManagement.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; } = string.Empty;
    }
}
