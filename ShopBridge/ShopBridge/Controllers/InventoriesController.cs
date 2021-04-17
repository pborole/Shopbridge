using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ShopBridge.Models;
namespace ShopBridge.Controllers
{
    public class InventoriesController : Controller
    {
        Uri apiUrl = new Uri("http://localhost:64593/api/Inventories");

        // GET: Inventories
        public ActionResult Index()
        {
            IEnumerable<Inventories> Inventories = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl.ToString());

                //HTTP GET
                var responseTask = client.GetAsync("Inventories");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Inventories>>();
                    readTask.Wait();

                    Inventories = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    Inventories = Enumerable.Empty<Inventories>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(Inventories.AsEnumerable());
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(Inventories Inventry)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl.ToString());

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Inventories>("Inventories", Inventry);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(Inventry);
        }

        public ActionResult Edit(int id)
        {
            Inventories Inventry = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl.ToString());
                //HTTP GET
                var responseTask = client.GetAsync("?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Inventories>();
                    readTask.Wait();

                    Inventry = readTask.Result;
                }
            }

            return View(Inventry);
        }

        [HttpPost]
        public ActionResult Edit(int id,Inventories Inventry)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl.ToString());

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Inventories>("?id=" + id.ToString(), Inventry);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(Inventry);
        }
        
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl.ToString());

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("?id=" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}