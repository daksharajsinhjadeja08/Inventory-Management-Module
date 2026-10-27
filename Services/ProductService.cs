
using Microsoft.Data.SqlClient;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
    public class ProductService
    {
        private readonly string _connectionString;

        public ProductService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("InventoryConnection")
                ?? throw new InvalidOperationException(
                    "InventoryConnection is missing from appsettings.json.");
        }

        // READ: Get all products
        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT ProductId, ProductName, Quantity, Price, Category FROM dbo.Products";

            using SqlCommand command = new SqlCommand(query, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductName = Convert.ToString(reader["ProductName"]) ?? string.Empty,
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Category = Convert.ToString(reader["Category"]) ?? string.Empty
                });
            }

            return products;
        }

        // READ: Get one product
        public Product? GetById(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT ProductId, ProductName, Quantity, Price, Category FROM dbo.Products WHERE ProductId = @ProductId";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProductId", id);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Product
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductName = Convert.ToString(reader["ProductName"]) ?? string.Empty,
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Price = Convert.ToDecimal(reader["Price"]),
                    Category = Convert.ToString(reader["Category"]) ?? string.Empty
                };
            }

            return null;
        }

        // CREATE: Add a product
        public void Add(Product product)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO dbo.Products
                (ProductName, Quantity, Price, Category)
                VALUES (@ProductName, @Quantity, @Price, @Category)";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@Quantity", product.Quantity);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Category", product.Category);

            command.ExecuteNonQuery();
        }

        // UPDATE: Edit a product
        public void Update(Product product)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE dbo.Products
                SET ProductName = @ProductName,
                    Quantity = @Quantity,
                    Price = @Price,
                    Category = @Category
                WHERE ProductId = @ProductId";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ProductId", product.ProductId);
            command.Parameters.AddWithValue("@ProductName", product.ProductName);
            command.Parameters.AddWithValue("@Quantity", product.Quantity);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Category", product.Category);

            command.ExecuteNonQuery();
        }

        // DELETE: Remove a product
        public void Delete(int id)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM dbo.Products WHERE ProductId = @ProductId";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProductId", id);

            command.ExecuteNonQuery();
        }
    }
}
