using System;
namespace CustomerApi.Models
{
	public class DatabaseSettings
	{
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CustomersCollectionName { get; set; } = null!;

        public string OrdersCollectionName { get; set; } = null!;

        public string ProductsCollectionName { get; set; } = null!;

        public string CategoriesCollectionName { get; set; } = null!;
    }
}

