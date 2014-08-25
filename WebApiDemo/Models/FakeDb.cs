using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDemo.Models
{
    // Dummy implmentation of an EF DBContext
    public class FakeDb
    {
        readonly DateTime _now = DateTime.UtcNow;
        readonly List<Product> _products
            = new List<Product>()
            {
                new Product(){ ProductId = 1, SkuNumber = "SKU1" },
                new Product(){ ProductId = 2, SkuNumber = "SKU2" },
                new Product(){ ProductId = 3, SkuNumber = "SKU3" },
                new Product(){ ProductId = 4, SkuNumber = "SKU4" },
                new Product(){ ProductId = 5, SkuNumber = "SKU5" },
                new Product(){ ProductId = 6, SkuNumber = "SKU6" },
                new Product(){ ProductId = 7, SkuNumber = "SKU7" },
                new Product(){ ProductId = 8, SkuNumber = "SKU8" },
                new Product(){ ProductId = 9, SkuNumber = "SKU9" }
            };

        public ICollection<Product> Products { get { return _products; } }

        public int SaveChanges()
        {
            return _products.Count;
        }
    }
}