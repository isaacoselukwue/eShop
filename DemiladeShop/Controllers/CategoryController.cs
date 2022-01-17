using DemiladeShop.Models.Database;
using DemiladeShop.Models.Database.Tables;
using DemiladeShop.Models.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemiladeShop.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet]
        [Route("[action]")]
        public HttpResponseMessage GetAllCategories()
        {
            var DbContext = new ShopDbContext();
            var categories = from c in DbContext.Categories
                             select new
                             {
                                 c.Id,
                                 c.CategoryName,
                                 c.CategoryDescription,
                                 c.CategoryImage
                             };
            return Request.CreateResponse(HttpStatusCode.OK,categories);
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public HttpResponseMessage GetACategory(Guid Id)
        {
            var DbContext = new ShopDbContext();
            var resp = DbContext.Categories.Where(x=>x.Id == Id).FirstOrDefault();
            return Request.CreateResponse(HttpStatusCode.OK,resp);
        }
        [HttpPost]
        [Route("[action]")]
        public HttpResponseMessage CreateCategory([FromBody] CategoryModel category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                var db = new ShopDbContext();
                var insertCateg = new CategoryTb
                {
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription,
                    CategoryImage = category.CategoryImage
                };
                db.Categories.Add(insertCateg);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }
        }
        [HttpPut]
        [Route("[Action]")]
        public HttpResponseMessage UpdateCategory([FromUri] Guid Id, [FromBody] CategoryModel category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                var db = new ShopDbContext();
                var upCat = db.Categories.Where(x=>x.Id == Id).FirstOrDefault();
                if (upCat == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                if (!string.IsNullOrEmpty(category.CategoryName))
                {
                    upCat.CategoryName = category.CategoryName;
                    db.SaveChanges();
                }
                if (!string.IsNullOrEmpty(category.CategoryDescription))
                {
                    upCat.CategoryDescription = category.CategoryDescription;
                    db.SaveChanges();
                }
                    upCat.CategoryImage = category.CategoryImage;
                    db.SaveChanges();
                
                

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("[action]")]
        public HttpResponseMessage DeleteCategory(Guid Id)
        {
            var db = new ShopDbContext();
            try
            {
                var findId = db.Categories.Find(Id);
                if (findId == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                db.Categories.Remove(findId);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
