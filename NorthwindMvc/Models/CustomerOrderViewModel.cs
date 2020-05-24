using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindMvc.Models
{
	public class CustomerOrderViewModel
	{
		public Customer Customer { get; set; }

		public IEnumerable<Order> Orders { get; set; }
	}
}
