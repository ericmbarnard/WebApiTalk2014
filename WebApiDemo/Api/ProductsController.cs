using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApiDemo.Infrastructure.HttpActionResults;
using WebApiDemo.Models;

namespace WebApiDemo.Api
{
    public class ProductsController : ApiController
    {
        readonly FakeDb _db;

        public ProductsController()
        {
            _db = new FakeDb();
        }

        public IHttpActionResult Get()
        {
            var products = _db.Products.ToList();
            return Ok(products);
        }

        public IHttpActionResult GetById(int id)
        {
            var product = _db.Products.FirstOrDefault(x => x.ProductId == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        public IHttpActionResult Post(Product product)
        {
            //TODO: Validation Logic

            _db.Products.Add(product);
            _db.SaveChanges();

            return Created(Request.RequestUri, product);
        }

        public IHttpActionResult Put(Product product)
        {
            // TODO: Validation Logic
            // TODO: Update Logic

            _db.Products.Add(product);
            _db.SaveChanges();

            return Created(Request.RequestUri, product);
        }

        public IHttpActionResult Delete(int id)
        {
            var product = _db.Products.FirstOrDefault(x => x.ProductId == id);

            if (product == null)
                return NotFound();

            _db.Products.Remove(product);
            _db.SaveChanges();

            return Created(Request.RequestUri, product);
        }

        #region Fancy Stuff

        [HttpGet, Route("api/products/topsellers")]
        public IHttpActionResult GetTopSellers()
        {
            var products = _db.Products.OrderBy(x => x.ProductId)
                                       .Take(5)
                                       .ToList();

            return new CustomOkResult<IEnumerable<Product>>(products, this);
        }

        [HttpPost, Route("api/products/baddata")]
        public IHttpActionResult BadData(Product product)
        {
            throw new BusinessException("Whoa, that's some bad data!");
        }

        #endregion
    }
}