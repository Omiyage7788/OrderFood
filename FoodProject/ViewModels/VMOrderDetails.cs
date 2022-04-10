using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodProject.Models;

namespace FoodProject.ViewModels
{
	public class VMOrderDetails
	{
		public Orders orders { get; set; }
		public List<OrderDetails> orderDetails { get; set; }
	}
}