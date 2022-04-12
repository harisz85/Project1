using Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Project1.Controllers
{
    public class ProductsApiController : ApiController
    {

        public IHttpActionResult Get()
        {
            using(ApplicationDbContext db  = new ApplicationDbContext() )
            {
                var products = db.Products.Select(prod => new
                {
                    prod.Id,
                    prod.externalId,
                    prod.code,
                    prod.description,
                    prod.name,
                    prod.barcode,
                    prod.retailPrice,
                    prod.wholesalePrice,
                    prod.discount
                }).ToList();

                return Json(new { products = products });
            }
        }


        public HttpResponseMessage Get(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);

                if (product != null)
                    return Request.CreateResponse(HttpStatusCode.OK, product);

                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with id " + id.ToString() + " not found.");
            }
        }




        public  IHttpActionResult Post([FromBody] Product product)
        {
            
            
                using(ApplicationDbContext db = new ApplicationDbContext())
                {
                    db.Products.Add(product);
                    db.SaveChanges();

                }

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);

        }



      
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Product product)
        {

            using(ApplicationDbContext db = new ApplicationDbContext())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != product.Id)
                {
                    return BadRequest();
                }

                db.Entry(product).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);

            }

        
        }




        [ResponseType(typeof(Product))]
        public IHttpActionResult Delete(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var productToDelete = db.Products.FirstOrDefault(p => p.Id == id);

                if (productToDelete == null)
                {
                    return NotFound();
                }
                else
                {
                    db.Products.Remove(productToDelete);
                    db.SaveChanges();

                    return Ok(productToDelete);
                }
            }
        }


        protected override void Dispose(bool disposing)
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (disposing)
                {
                    db.Dispose();
                }

                base.Dispose(disposing);
            }
          
        }


        private bool ProductExists(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.Products.Count(p => p.Id == id) > 0;
            }
        }


    }



}
