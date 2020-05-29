using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindMvc.Models
{
	public class OrderViewModel
	{
		public List<OrderEntry> Order { get; set; }
	}
}
