using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project1.Models;
using System.Data.SqlClient;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;

namespace Project1.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        private static readonly HttpClient HttpClient;


        string BaseUrl = "https://localhost:44362/";




        // GET: Products
       
        public ActionResult Index()
        {

            var products = db.Products.ToList();



            //returning the product list to view
            return View(products);


        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {



            var product = db.Products.SingleOrDefault(p => p.Id == id);


            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create


        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");


            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,externalId,code,description,barcode,retailPrice,wholesalePrice,discount")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




        public async Task<ActionResult> LoadList()
        {
            List<Product> products = new List<Product>();

            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(BaseUrl);
                //client.DefaultRequestHeaders.Clear();

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage result = await client.GetAsync("https://cloudonapi.oncloud.gr/s1services/JS/updateItems/cloudOnTest");

                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadAsStreamAsync();

                    //deserializing the rsponse
                        

                    using (var streamReader = new StreamReader(response))
                    {
                        using (var jsonTextReader = new JsonTextReader(streamReader))
                        {
                            var jsonSerializer = new JsonSerializer();
                            var data = jsonSerializer.Deserialize<Response>(jsonTextReader);
                        }
                    }



                    //   products = JsonConvert.DeserializeObject<List<Product>>(response);
                }

                return View(products);
            }
        }


    }

    
}


