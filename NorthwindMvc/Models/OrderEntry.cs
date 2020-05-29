using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindMvc.Models
{

	public class OrderEntry
	{
		public int ID { get; set; }
		public Product Product { get; set; }
		public int Amount { get; set; }
	}

}
