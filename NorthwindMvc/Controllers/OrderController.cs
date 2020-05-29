using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NorthwindMvc.Data;
using NorthwindMvc.Models;

namespace NorthwindMvc.Controllers
{
    public class OrderController : Controller
    {

        private readonly Northwind _context;
        public OrderController(Northwind context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            var model = new OrdersViewModel
            {
                Products = await _context.Products.ToListAsync(),
            };
            return View(model);
        }

        public IActionResult Buy()
        {
            var x = Request.Cookies;
            if (!HttpContext.Request.Cookies.ContainsKey("order") || Request.Cookies["order"] == "")
            {
                return RedirectToAction("Index");
            }
            string ordStr = Request.Cookies["order"];
            string[] splt = ordStr.Substring(0,ordStr.Length - 1).Split("-");
            List<OrderEntry> orders = new List<OrderEntry>();
            foreach (string s in splt)
            {
                int id = int.Parse(s.Split(":")[0]);
                orders.Add(new OrderEntry { ID=id,Product=_context.Products.First(p => p.ProductID == id),Amount= int.Parse(s.Split(":")[1]) });
            }

            var model = new OrderViewModel
            {
                Order = orders
            };
            return View(model);
        }
    }
}