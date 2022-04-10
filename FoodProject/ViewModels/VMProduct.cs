using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodProject.Models;

namespace FoodProject.ViewModels
{
	public class VMProduct
	{
		public Suppliers supplier { get; set; }
		public List<Products> products { get; set; }
	}
}