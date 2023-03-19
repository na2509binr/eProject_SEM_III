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
    public class GenresController : ControllerBase
    {
        private readonly ABCDMallContext db;

        public GenresController(ABCDMallContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<Genre> GetAll(string name)
        {
            var genre = db.Genres.OrderBy(c => c.GenreId).AsEnumerable();
            if (!String.IsNullOrEmpty(name))
            {
                genre = db.Genres.Where(c => c.Name.Contains(name)).OrderBy(c => c.GenreId);
            }
            return genre;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var find = db.Genres.Find(id);
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
        public IActionResult Update(int id, Genre genre)
        {
            var find = db.Genres.Where(c => c.GenreId == id);
            if (find != null)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(genre);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult Insert(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return Ok(genre);
            }

            return Ok("Add new failed!");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var find = db.Genres.Find(id);
            if (find != null)
            {
                db.Genres.Remove(find);
                db.SaveChanges();
                return Ok(find);
            }

            return NotFound();
        }
    }
}