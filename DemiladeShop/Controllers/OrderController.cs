using DemiladeShop.Models.Database;
using DemiladeShop.Models.Database.Tables;
using DemiladeShop.Models.Dtos.Orders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemiladeShop.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        [Route("GetAllOrders")]//admin
        public HttpResponseMessage GetAllOrders()
        {
            var db = new ShopDbContext();
            try
            {
                var res = from a in db.Orders
                          from b in db.OrderDetails 
                          where a.Id == b.OrderId
                          select new
                          {
                              a.Id,
                              a.Phone,
                              a.IsOrderCompleted,
                              a.OrderTotal,
                              a.OrderDetails,
                              a.AmountPaid,
                              a.TimePaid,
                              a.Address,
                              a.FullName,
                              a.PaymentReference,
                              a.ResponseCode,
                              a.ResponseDescription
                          };
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetAllOrdersVendor")]//admin
        public HttpResponseMessage GetAllOrdersVendor(Guid VendorId)
        {
            var db = new ShopDbContext();
            try
            {
                var res = from a in db.Orders
                          from b in db.OrderDetails
                          where a.Id == b.OrderId && a.VendorId == VendorId
                          select new
                          {
                              a.Id,
                              a.Phone,
                              a.IsOrderCompleted,
                              a.OrderTotal,
                              a.OrderDetails,
                              a.AmountPaid,
                              a.TimePaid,
                              a.Address,
                              a.FullName,
                              a.PaymentReference,
                              a.ResponseCode,
                              a.ResponseDescription
                          };
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetAllPendingOrders")]
        public HttpResponseMessage GetAllPendingOrders(Guid VendorId)
        {
            var db = new ShopDbContext();
            try
            {
                var getItem = db.Orders.Where(x=>x.VendorId == VendorId && x.IsOrderCompleted == false)
                    .Include(order=>order.OrderDetails.Select(prod => prod.Product));
                if (!getItem.Any())
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                return Request.CreateResponse(HttpStatusCode.OK, getItem);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetAllCompletedOrders")]
        public HttpResponseMessage GetAllCompletedOrders(Guid VendorId)
        {
            var db = new ShopDbContext();
            try
            {
                var getItem = db.Orders.Where(x => x.VendorId == VendorId && x.IsOrderCompleted == true)
                    .Include(order => order.OrderDetails.Select(prod => prod.Product));
                if (!getItem.Any())
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                return Request.CreateResponse(HttpStatusCode.OK, getItem);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("GetOrderDetails")]
        public HttpResponseMessage GetOrderDetails(Guid OrderId)
        {
            var db = new ShopDbContext();
            try
            {
                var getItem = db.Orders.Where(x => x.Id == OrderId && x.IsOrderCompleted == true)
                    .Include(order => order.OrderDetails.Select(prod=>prod.Product));
                if (!getItem.Any())
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                return Request.CreateResponse(HttpStatusCode.OK, getItem);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UploadOrder")]
        public HttpResponseMessage UploadOrder([FromBody] OrderModel model)
        {
            var db = new ShopDbContext();
            try
            {
                var order = new OrderTb();
                var userId = db.Users.Where(x => x.Email == User.Identity.Name);
                var searchforItem = db.ShoppingCarts.Where(x => x.CustomerId == model.UserId).FirstOrDefault();
                var findVendorByProduct = db.Products.Where(x => x.Id == searchforItem.ProductId).FirstOrDefault();
                var findUser = db.Users.Where(x => x.Id == findVendorByProduct.VendorId).FirstOrDefault();

                order.IsOrderCompleted = false;
                order.PaymentReference = model.PaymentId;
                order.FullName = User.Identity.Name;
                order.Phone = model.Phone;
                order.Address = model.Address;
                order.UserId = Guid.NewGuid();
                order.TimePaid = DateTime.Now;
                order.VendorId = findUser.Id;
                db.Orders.Add(order);

                var shoppingItems = db.ShoppingCarts.Where(cart => cart.CustomerId == order.UserId);

                foreach (var item in shoppingItems)
                {
                    var orderDetail = new OrderDetailsTb()
                    {
                        Price = item.Price,
                        TotalAmount = item.TotalAmount,
                        Qty = item.Qty,
                        ProductId = item.ProductId,
                        OrderId = order.Id,
                    };
                    db.OrderDetails.Add(orderDetail);
                }
                db.ShoppingCarts.RemoveRange(shoppingItems);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new { OrderId = order.Id });

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut]
        [Route("FulfilOrder")]
        public HttpResponseMessage FulfilOrder([FromUri] Guid OrderId, [FromBody] OrderPut order)
        {
            var db = new ShopDbContext();
            try
            {
                var findItem = db.Orders.Where(x=>x.Id == OrderId).FirstOrDefault();
                if (findItem == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                findItem.IsOrderCompleted = order.IsOrderCompleted;
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
