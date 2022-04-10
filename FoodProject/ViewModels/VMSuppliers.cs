using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodProject.Models;

namespace FoodProject.ViewModels
{
	public class VMSuppliers
	{
		public Suppliers Supplier { get; set; }
		public List<Categories> Categories { get; set; }
		public List<Products> Products { get; set; }
	}
}