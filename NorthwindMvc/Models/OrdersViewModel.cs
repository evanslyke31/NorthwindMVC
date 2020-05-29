using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindMvc.Models
{
	public class OrdersViewModel
	{
		public IEnumerable<Product> Products { get; set; }

	}
}
