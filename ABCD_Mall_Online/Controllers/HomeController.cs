using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ABCD_Mall_Online.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace ABCD_Mall_Online.Controllers
{
    public class HomeController : Controller
    {
        public ABCDMallContext _db;
        public HomeController(ABCDMallContext db)
        {
            
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {


            return View();
        }

        [Route("Shopping")]
        public async Task<IActionResult> Shopping(string name)
        {
            var response = await MVC.GlobalVariables.client.GetAsync("Stores?name"+name);
            var res = await response.Content.ReadAsStringAsync();
            List<Store> store = JsonConvert.DeserializeObject<List<Store>>(res);
            List<Store> store1 = new List<Store>();

            foreach (var item in store)
            {
                if (item.CategoryId.Equals("Thời trang"))
                {
                    store1.Add(item);
                }
            }

            return View(store1);
        }


        [Route("Entertainment")]
        public IActionResult Entertainment()
        {
            return View();
        }


        [Route("Dining")]
        public async Task<IActionResult> Dining()
        {
            var response = await MVC.GlobalVariables.client.GetAsync("Stores");
            var res = await response.Content.ReadAsStringAsync();
            List<Store> store = JsonConvert.DeserializeObject<List<Store>>(res);
            List<Store> store1 = new List<Store>();

            foreach (var item in store)
            {
                if (item.CategoryId.Equals("Đồ ăn"))
                {
                    store1.Add(item);
                }
            }

            return View(store1);
        }


        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Customer cus)
        {
            var checkCustomer = _db.Customers.FirstOrDefault(x => x.Email == cus.Email && x.Password == cus.Password);
            if (checkCustomer != null)
            {   
                HttpContext.Session.SetString("CustomerLogin", cus.Email);
                HttpContext.Session.SetString("CustomerLoginPass", cus.Password);
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError("CustomerLoginPass", "Tài khoản Email hoặc mật khẩu không đúng");
                return Redirect("/Login");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CustomerLogin");
            return RedirectToAction("/");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
