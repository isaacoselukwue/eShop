using DemiladeShop.Models.Database;
using DemiladeShop.Models.Database.Tables;
using DemiladeShop.Models.Dtos.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemiladeShop.Controllers
{
    public class ShoppingCartController : ApiController
    {
        [HttpGet]
        [Route("GetShoppingCart")]
        public HttpResponseMessage GetShoppingCart(Guid UserId)
        {
            var db = new ShopDbContext();
            try
            {
                var getShoppingDets = from c in db.ShoppingCarts.Where(x => x.CustomerId == UserId)
                                      join d in db.Products on c.ProductId equals d.Id
                                      select new
                                      {
                                          c.Price,
                                          c.TotalAmount,
                                          c.Qty,
                                          c.CustomerId,
                                          c.ProductId,
                                          d.CategoryId,
                                          d.Productname,
                                          d.ProductImages,
                                          d.VendorId,
                                          d.ProductDescription
                                      };
                return Request.CreateResponse(HttpStatusCode.OK, getShoppingDets);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("SubTotal")]
        public HttpResponseMessage SubTotal(Guid userId)
        {
            var db = new ShopDbContext();
            try
            {
                var subAmount = db.ShoppingCarts.Where(x => x.CustomerId == userId).Select(s => s.TotalAmount).Sum();

                return Request.CreateResponse(HttpStatusCode.OK, new { SubAmount = subAmount });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("TotalItems")]
        public HttpResponseMessage TotalItems(Guid userId)
        {
            var db = new ShopDbContext();
            try
            {
                var totalItemsAmount = db.ShoppingCarts.Where(x => x.CustomerId == userId).Select(p => p.Qty).Sum();
                return Request.CreateResponse(HttpStatusCode.OK, new { TotalItems = totalItemsAmount });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("UploadShoppingCart")]
        public HttpResponseMessage UploadShoppingCart([FromBody] ShoppingCartModel model)
        {
            var db = new ShopDbContext();
            try
            {
                var shoppingItem = db.ShoppingCarts.Where(x => x.ProductId == model.ProductId && x.CustomerId == model.CustomerId).FirstOrDefault();
                if (shoppingItem != null)
                {
                    shoppingItem.Qty += model.Qty;
                    shoppingItem.TotalAmount = shoppingItem.Price * shoppingItem.Qty;
                }
                else
                {
                    var sCart = new ShoppingCartTb
                    {
                        CustomerId = model.CustomerId,
                        ProductId = model.ProductId,
                        Price = model.Price,
                        Qty = model.Qty,
                        TotalAmount = model.TotalAmount
                    };
                    db.ShoppingCarts.Add(sCart);
                }
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteItem")]
        public HttpResponseMessage DeleteItem(Guid Id, Guid UserId, Guid ProductId)
        {
            var db = new ShopDbContext();
            try
            {
                var shopItem = db.ShoppingCarts.Where(x => x.Id == Id && x.ProductId == ProductId && x.CustomerId == UserId);
                if (shopItem == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                db.ShoppingCarts.RemoveRange(shopItem);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("DeleteAllItems")]
        public HttpResponseMessage DeleteAllItems(Guid UserId, Guid ProductId)
        {
            var db = new ShopDbContext();
            try
            {
                var shopItems = db.ShoppingCarts.Where(x => x.CustomerId == UserId && x.ProductId == ProductId);
                if (shopItems == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                db.ShoppingCarts.RemoveRange(shopItems);
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