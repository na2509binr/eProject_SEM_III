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
    public class CustomersController : ControllerBase
    {
        private readonly ABCDMallContext db;

        public CustomersController(ABCDMallContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<Customer> GetAll(string name)
        {
            var customer = db.Customers.OrderBy(c => c.CustomerId).AsEnumerable();
            if (!String.IsNullOrEmpty(name))
            {
                customer = db.Customers.Where(c => c.CustomerName.Contains(name)).OrderBy(c => c.CustomerId);
            }
            return customer;
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var find = db.Customers.Find(id);
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
        public IActionResult Update(int id, Customer customer)
        {
            var find = db.Customers.Where(c => c.CustomerId == id);
            if (find != null)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult Insert(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return Ok(customer);
            }

            return Ok("Add new failed!");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var find = db.Customers.Find(id);
            if (find != null)
            {
                db.Customers.Remove(find);
                db.SaveChanges();
                return Ok(find);
            }

            return NotFound();
        }
    }
}