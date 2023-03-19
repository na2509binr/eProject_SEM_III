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
    public class CategoriesController : ControllerBase
    {
        private readonly ABCDMallContext db;

        public CategoriesController(ABCDMallContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<Category> GetAll(string name)
        {
            var cat = db.Categories.OrderBy(c => c.CategoryId).AsEnumerable();
            if (!String.IsNullOrEmpty(name))
            {
                cat = db.Categories.Where(c => c.Name.Contains(name)).OrderBy(c => c.CategoryId);
            }
            return cat;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var find = db.Categories.Find(id);
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
        public IActionResult Update(int id, Category category)
        {
            var find = db.Categories.Where(c => c.CategoryId == id);
            if (find != null)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(category);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult Insert(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return Ok(category);
            }

            return Ok("Add new failed!");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var find = db.Categories.Find(id);
            if (find != null)
            {
                db.Categories.Remove(find);
                db.SaveChanges();
                return Ok(find);
            }

            return NotFound();
        }
    }
}