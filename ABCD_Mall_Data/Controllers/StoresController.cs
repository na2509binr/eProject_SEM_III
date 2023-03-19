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
using X.PagedList;

namespace ABCD_Mall_Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class StoresController : ControllerBase
    {
        private readonly ABCDMallContext db;

        public StoresController(ABCDMallContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string name)
        {
            var st = await db.Stores
                                .Join(
                                    db.Categories,
                                    store => store.CategoryId,
                                    cat => cat.CategoryId,
                                    (store, cat) => new
                                    {
                                        StoreId = store.StoreId,
                                        Name = store.Name,
                                        Phone = store.Phone,
                                        Address = store.Address,
                                        image = store.image,
                                        Description = store.Description,
                                        CreatedDate = store.CreatedDate,
                                        CategoryId = cat.Name
                                    }
                                ).ToListAsync();
            if (!String.IsNullOrEmpty(name))
            {
                st = await db.Stores
                    .Join(
                        db.Categories,
                        store => store.CategoryId,
                        cat => cat.CategoryId,
                        (store, cat) => new
                        {
                            StoreId = store.StoreId,
                            Name = store.Name,
                            Phone = store.Phone,
                            Address = store.Address,
                            image = store.image,
                            Description = store.Description,
                            CreatedDate = store.CreatedDate,
                            CategoryId = cat.Name
                        }
                    ).Where(s => s.Name.Contains(name)).ToListAsync();
            }
            return Ok(st);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var find = db.Stores.Find(id);
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
        public IActionResult Update(int id, Store store)
        {
            var find = db.Stores.Where(c => c.CategoryId == id);
            if (find != null)
            {
                db.Entry(store).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return Ok(store);
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
        public IActionResult Insert(Store store)
        {
            if (ModelState.IsValid)
            {
                db.Stores.Add(store);
                db.SaveChanges();
                return Ok(store);
            }

            return Ok("Add new failed!");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var find = db.Stores.Find(id);
            if (find != null)
            {
                db.Stores.Remove(find);
                db.SaveChanges();
                return Ok(find);
            }

            return NotFound();
        }
    }
}