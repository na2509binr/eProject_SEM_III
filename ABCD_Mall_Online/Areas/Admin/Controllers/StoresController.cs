using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ABCD_Mall_Online.Areas.Admin.Models;
using ABCD_Mall_Online.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using X.PagedList;

namespace ABCD_Mall_Online.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StoresController : Controller
    {
        public async Task<IActionResult> Index(string name ,int page = 1)
        {
            int limit = 5;
            var response = await MVC.GlobalVariables.client.GetAsync("Stores?name=" +name);
            var res = await response.Content.ReadAsStringAsync();
            List<Store> stores = JsonConvert.DeserializeObject<List<Store>>(res);

            AdminViewModel avm = new AdminViewModel();

            avm.stList = stores.ToPagedList<Store>(page, limit);
            return View(avm);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var store = MVC.GlobalVariables.client.GetAsync("Stores/" + id.ToString()).Result;
            if (store == null)
            {
                return NotFound();
            }
            return View(store.Content.ReadAsAsync<Store>().Result);
        }

        public async Task<ActionResult> Create()
        {
            var response = await MVC.GlobalVariables.client.GetAsync("Categories");
            var res = await response.Content.ReadAsStringAsync();
            List<Category> category = JsonConvert.DeserializeObject<List<Category>>(res);
            ViewData["CategoryId"] = new SelectList(category, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Store st)
        {
            //upload Image
            var files = HttpContext.Request.Form.Files;
            
            if (files.Count() > 0 && files[0].Length > 0)
            {
                var file = files[0];
                var FileName = file.FileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\store", FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                    st.image = FileName; 
                }
            }

            HttpResponseMessage respone = MVC.GlobalVariables.client.PostAsJsonAsync("Stores", st).Result;
            if (respone.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            var response = await MVC.GlobalVariables.client.GetAsync("Categories");
            var res = await response.Content.ReadAsStringAsync();
            List<Category> category = JsonConvert.DeserializeObject<List<Category>>(res);
            ViewData["CategoryId"] = new SelectList(category, "CategoryId", "Name", st.CategoryId);

            return View(st);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var store = MVC.GlobalVariables.client.GetAsync("Stores/" + id.ToString()).Result;
            if (store == null)
            {
                return NotFound();
            }
            var response = await MVC.GlobalVariables.client.GetAsync("Categories");
            var res = await response.Content.ReadAsStringAsync();
            List<Category> category = JsonConvert.DeserializeObject<List<Category>>(res);
            ViewData["CategoryId"] = new SelectList(category, "CategoryId", "Name");
            return View(store.Content.ReadAsAsync<Store>().Result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Store st)
        {
            //upload Image
            var files = HttpContext.Request.Form.Files;

            if (files.Count() > 0 && files[0].Length > 0)
            {
                var file = files[0];
                var FileName = file.FileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\store", FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                    st.image = FileName;
                }
            }

            HttpResponseMessage respone = MVC.GlobalVariables.client.PutAsJsonAsync("Stores/" + st.StoreId, st).Result;
            if (respone.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            var response = await MVC.GlobalVariables.client.GetAsync("Categories");
            var res = await response.Content.ReadAsStringAsync();
            List<Category> category = JsonConvert.DeserializeObject<List<Category>>(res);
            ViewData["CategoryId"] = new SelectList(category, "CategoryId", "Name", st.CategoryId);
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var store = MVC.GlobalVariables.client.GetAsync("Stores/" + id.ToString()).Result;
            if (store == null)
            {
                return NotFound();
            }
            return View(store.Content.ReadAsAsync<Store>().Result);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = MVC.GlobalVariables.client.DeleteAsync("Stores/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}