using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebApiDemo.Models;

namespace WebApiDemo.Tests.Api
{
    [TestClass]
    public class ProductsControllerTests : ApiControllerTestBase
    {
        [TestMethod]
        public async Task ProductsController_GetsAllProducts()
        {
            var resp = await Client.GetAsync("/api/products");
            var products = await resp.Content.ReadAsAsync<List<Product>>();

            // Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(9, products.Count);
        }

        [TestMethod]
        public async Task ProductsController_GetsTopSellers()
        {
            var resp = await Client.GetAsync("/api/products/topsellers");
            var products = await resp.Content.ReadAsAsync<List<Product>>();

            // Assert
            Assert.IsNotNull(products);
            Assert.AreEqual(5, products.Count);
        }

        [TestMethod]
        public async Task ProductsController_CreatesProduct()
        {
            // Arrange
            var product = new Product() { ProductId = 11, SkuNumber = "SKU11" };

            // Act
            var resp = await Client.PostAsJsonAsync("/api/products", product);
            
            // Assert
            Assert.IsTrue(resp.IsSuccessStatusCode);

            var saved = await resp.Content.ReadAsAsync<Product>();
            Assert.AreEqual(11, saved.ProductId);
        }
    }
}
