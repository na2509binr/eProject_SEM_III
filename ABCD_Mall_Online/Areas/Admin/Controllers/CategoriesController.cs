using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ABCD_Mall_Online.Areas.Admin.Models;
using ABCD_Mall_Online.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace ABCD_Mall_Online.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5;
            var response = await MVC.GlobalVariables.client.GetAsync("Categories?name=" +name);
            var res = await response.Content.ReadAsStringAsync();
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(res);

            AdminViewModel avm = new AdminViewModel();

            avm.catList = categories.ToPagedList<Category>(page, limit);
            return View(avm);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = MVC.GlobalVariables.client.GetAsync("Categories/" + id.ToString()).Result;
            if (category == null)
            {
                return NotFound();
            }
            return View(category.Content.ReadAsAsync<Category>().Result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category c)
        {
            
            HttpResponseMessage respone = MVC.GlobalVariables.client.PostAsJsonAsync("Categories", c).Result;
            if (respone.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            
            return View();
            
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = MVC.GlobalVariables.client.GetAsync("Categories/" + id.ToString()).Result;
            if (category == null)
            {
                return NotFound();
            }
            return View(category.Content.ReadAsAsync<Category>().Result);
        }

        [HttpPost]
        public IActionResult Edit(int id,Category c)
        {
            HttpResponseMessage respone = MVC.GlobalVariables.client.PutAsJsonAsync("Categories/" + c.CategoryId, c).Result;
            if (respone.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View();  
            
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = MVC.GlobalVariables.client.GetAsync("Categories/" + id.ToString()).Result;
            if (category == null)
            {
                return NotFound();
            }
            return View(category.Content.ReadAsAsync<Category>().Result);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = MVC.GlobalVariables.client.DeleteAsync("Categories/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}