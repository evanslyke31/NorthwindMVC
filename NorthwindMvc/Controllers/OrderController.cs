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
            if (!HttpContext.Request.Cookies.ContainsKey("order") || Request.Cookies["order"] == "")
            {
                return RedirectToAction("Index");
            }

            var model = new OrderViewModel
            {
                Order = CookieToList(Request.Cookies["order"])
            };
            return View(model);
        }

        public IActionResult Confirm()
        {
            foreach(OrderEntry order in CookieToList(Request.Cookies["order"]))
            {
                order.Product.UnitsInStock -= (short)order.Amount;
            }
            _context.SaveChanges();
            HttpContext.Response.Cookies.Delete("order");
            return RedirectToAction("Index", "Home");
        }

        private List<OrderEntry> CookieToList(string cookie)
        {
            string[] splt = cookie.Substring(0,cookie.Length - 1).Split("-");
            List<OrderEntry> orders = new List<OrderEntry>();
            foreach (string s in splt)
            {
                int id = int.Parse(s.Split(":")[0]);
                orders.Add(new OrderEntry { ID = id, Product = _context.Products.First(p => p.ProductID == id), Amount = int.Parse(s.Split(":")[1]) });
            }
            return orders;

        }
    }
}