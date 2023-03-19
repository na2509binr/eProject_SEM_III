using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCD_Mall_Data.Data;
using ABCD_Mall_Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABCD_Mall_Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FilmsController : ControllerBase
    {
        private readonly ABCDMallContext db;

        public FilmsController(ABCDMallContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<Film> GetAll(string name)
        {
            var film = db.Films.OrderBy(c => c.FilmId).AsEnumerable();
            if (!String.IsNullOrEmpty(name))
            {
                film = db.Films.Where(c => c.Title.Contains(name)).OrderBy(c => c.FilmId);
            }
            return film;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var find = db.Films.Find(id);
            if (find != null)
            {
                return Ok(find);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Film film)
        {
            var find = db.Stores.Where(c => c.CategoryId == id);
            if (find != null)
            {
                db.Entry(film).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return Ok(film);
                }
                catch (Exception e)
                {
                    return Ok(e.InnerException.Message);
                    throw;
                }
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult Insert(Film film)
        {
            if (ModelState.IsValid)
            {
                db.Films.Add(film);
                db.SaveChanges();
                return Ok(film);
            }

            return Ok("Add new failed!");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var find = db.Films.Find(id);
            if (find != null)
            {
                db.Films.Remove(find);
                db.SaveChanges();
                return Ok(find);
            }

            return NotFound();
        }
    }
}