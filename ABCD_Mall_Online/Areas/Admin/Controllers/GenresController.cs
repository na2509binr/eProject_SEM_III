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
    public class GenresController : Controller
    {
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5;
            var response = await MVC.GlobalVariables.client.GetAsync("Genres?name=" + name);
            var res = await response.Content.ReadAsStringAsync();
            List<Genre> genre = JsonConvert.DeserializeObject<List<Genre>>(res);

            AdminViewModel avm = new AdminViewModel();

            avm.genreList = genre.ToPagedList<Genre>(page, limit);
            return View(avm);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = MVC.GlobalVariables.client.GetAsync("Genres/" + id.ToString()).Result;
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre.Content.ReadAsAsync<Genre>().Result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre g)
        {

            HttpResponseMessage respone = MVC.GlobalVariables.client.PostAsJsonAsync("Genres", g).Result;
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
            var genre = MVC.GlobalVariables.client.GetAsync("Genres/" + id.ToString()).Result;
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre.Content.ReadAsAsync<Genre>().Result);
        }

        [HttpPost]
        public IActionResult Edit(int id, Genre g)
        {
            HttpResponseMessage respone = MVC.GlobalVariables.client.PutAsJsonAsync("Genres/" + g.GenreId, g).Result;
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
            var genre = MVC.GlobalVariables.client.GetAsync("Genres/" + id.ToString()).Result;
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre.Content.ReadAsAsync<Genre>().Result);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var genre = MVC.GlobalVariables.client.DeleteAsync("Genres/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}
