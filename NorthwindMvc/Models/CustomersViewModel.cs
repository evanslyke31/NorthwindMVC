﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindMvc.Models
{
	public class CustomersViewModel
	{
		public IEnumerable<Customer> Customers { get; set; }
	}
}
