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
using Newtonsoft.Json;
using X.PagedList;

namespace ABCD_Mall_Online.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FilmsController : Controller
    {
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5;
            var response = await MVC.GlobalVariables.client.GetAsync("Films?name=" + name);
            var res = await response.Content.ReadAsStringAsync();
            List<Film> film = JsonConvert.DeserializeObject<List<Film>>(res);

            AdminViewModel avm = new AdminViewModel();

            avm.filmList = film.ToPagedList<Film>(page, limit);
            return View(avm);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var film = MVC.GlobalVariables.client.GetAsync("Films/" + id.ToString()).Result;
            if (film == null)
            {
                return NotFound();
            }
            return View(film.Content.ReadAsAsync<Film>().Result);
        }

        public async Task<ActionResult> Create()
        {
            var response = await MVC.GlobalVariables.client.GetAsync("Genres");
            var res = await response.Content.ReadAsStringAsync();
            List<Genre> genre = JsonConvert.DeserializeObject<List<Genre>>(res);
            ViewData["GenreId"] = new SelectList(genre, "GenreId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Film film)
        {
            //upload Image
            var files = HttpContext.Request.Form.Files;

            if (files.Count() > 0 && files[0].Length > 0)
            {
                var file = files[0];
                var FileName = file.FileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\film", FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                    film.Image = FileName;
                }
            }

            HttpResponseMessage respone = MVC.GlobalVariables.client.PostAsJsonAsync("Films", film).Result;
            if (respone.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            var response = await MVC.GlobalVariables.client.GetAsync("Films");
            var res = await response.Content.ReadAsStringAsync();
            List<Genre> genre = JsonConvert.DeserializeObject<List<Genre>>(res);
            ViewData["GenreId"] = new SelectList(genre, "GenreId", "Name", film.GenreId);

            return View(film);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var film = MVC.GlobalVariables.client.GetAsync("Films/" + id.ToString()).Result;
            if (film == null)
            {
                return NotFound();
            }
            var response = await MVC.GlobalVariables.client.GetAsync("Genres");
            var res = await response.Content.ReadAsStringAsync();
            List<Genre> genre = JsonConvert.DeserializeObject<List<Genre>>(res);
            ViewData["GenreId"] = new SelectList(genre, "GenreId", "Name");
            return View(film.Content.ReadAsAsync<Film>().Result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Film film)
        {
            //upload Image
            var files = HttpContext.Request.Form.Files;

            if (files.Count() > 0 && files[0].Length > 0)
            {
                var file = files[0];
                var FileName = file.FileName;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\film", FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                    film.Image = FileName;
                }
            }

            HttpResponseMessage respone = MVC.GlobalVariables.client.PutAsJsonAsync("Films/" + film.FilmId, film).Result;
            if (respone.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            var response = await MVC.GlobalVariables.client.GetAsync("Genres");
            var res = await response.Content.ReadAsStringAsync();
            List<Genre> genre = JsonConvert.DeserializeObject<List<Genre>>(res);
            ViewData["GenreId"] = new SelectList(genre, "GenreId", "Name", film.GenreId);
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var film = MVC.GlobalVariables.client.GetAsync("Films/" + id.ToString()).Result;
            if (film == null)
            {
                return NotFound();
            }
            return View(film.Content.ReadAsAsync<Film>().Result);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var film = MVC.GlobalVariables.client.DeleteAsync("Films/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}