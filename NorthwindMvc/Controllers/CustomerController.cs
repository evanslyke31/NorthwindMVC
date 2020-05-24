using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NorthwindMvc.Data;
using NorthwindMvc.Models;

namespace NorthwindMvc.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Northwind _context;

        public CustomerController(Northwind context)
        {
            _context = context;
        }

        public IEnumerable<Customer> Customers { get; set; }
        public async Task<IActionResult> Index()
        {
            var model = new CustomersViewModel
            {
                Customers = await _context.Customers.ToListAsync()
            };
            return View(model);
        }

        public async Task<IActionResult> Orders(string id)
        {
            if(id == null)
            {
                return NotFound("Invalid ID");
            }
            var Customer = await _context.Customers.FirstOrDefaultAsync(c => id == c.CustomerID);
            if(Customer == null)
            {
                return NotFound();
            }
            var Orders = _context.Orders.Where(o => o.CustomerID == Customer.CustomerID)
                                        .Include(d => d.OrderDetails)
                                            .ThenInclude(p => p.Product);
            var model = new CustomerOrderViewModel
            {
                Customer = Customer,
                Orders = Orders

            };

            return View(model);
        }
    }
}