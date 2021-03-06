using DemiladeShop.Models.Database;
using DemiladeShop.Models.Database.Tables;
using DemiladeShop.Models.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemiladeShop.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("GetAllProducts")]
        public HttpResponseMessage GetAllProducts()
        {
            var db = new ShopDbContext();
            try
            {
                var prod = from c in db.Products
                           select new
                           {
                               c.Id,
                               c.Productname,
                               c.ProductDescription,
                               c.Price,
                               c.CategoryId,
                               c.VendorId,
                               c.Images
                           };
                return Request.CreateResponse(HttpStatusCode.OK, prod);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetByProductId")]
        public HttpResponseMessage GetByProductId(Guid Id)
        {
            var db = new ShopDbContext();
            try
            {
                var findbyId = from c in db.Products
                               where c.Id == Id
                               select new
                               {
                                   c.Id,
                                   c.Productname,
                                   c.ProductDescription,
                                   c.Images,
                                   c.Price,
                                   c.VendorId,
                                   c.CategoryId
                               };
                return Request.CreateResponse(HttpStatusCode.OK, findbyId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetByCategory")]
        public HttpResponseMessage GetByCategory(Guid Id)
        {
            var db = new ShopDbContext();
            try
            {
                var findall = from c in db.Products
                              where c.CategoryId == Id
                              select new
                              {
                                  c.Productname,
                                  c.Id,
                                  c.Price,
                                  c.VendorId,
                                  c.Images,
                                  c.CategoryId,
                                  c.ProductDescription
                              };
                return Request.CreateResponse(HttpStatusCode.OK,findall);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("CreateProducts")]
        public HttpResponseMessage CreateProducts([FromBody] ProductModel product)
        {
            var db = new ShopDbContext();
            try
            {
                var req = new ProductTb
                {
                    Productname = product.Productname,
                    Price = product.Price,
                    ProductDescription = product.Productdescription,
                    CategoryId = product.CategoryId,
                   // ProductImages = product.ProductImage,
                    VendorId = product.VendorId
                };
                db.Products.Add(req);
                var p = new ProductImgTb();
                foreach (var item in product.ProductImage)
                {
                    p = new ProductImgTb
                    {
                        ProductId = item.ProductId,
                        ImageBytes = item.ImageBytes
                    };
                }
                db.ProductImg.Add(p);
                
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteProduct")]
        public HttpResponseMessage DeleteProduct(Guid Id)
        {
            var db = new ShopDbContext();
            try
            {
                var findid = db.Products.Single(x => x.Id == Id);
                if (findid == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                db.Products.Remove(findid); 
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
