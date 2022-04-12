using Newtonsoft.Json;
using Project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Project1.Controllers
{
    public class UserController : Controller
    {

        Uri baseAddress = new Uri("https://cloudonapi.oncloud.gr/s1services/JS/updateItems/cloudOnTest");
        HttpClient client;



        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }



        // GET: User
        public ActionResult Index()
        {

            var products = new List<Product>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;


            //if (response.IsSuccessStatusCode)
            //{
            //    string data = response.Content.ReadAsStringAsync().Result;
            //    products = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
            //}


            return View(products);
        }
    }
}