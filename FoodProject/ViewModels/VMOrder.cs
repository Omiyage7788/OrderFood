using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodProject.Models;

namespace FoodProject.ViewModels
{
	public class VMOrder
	{
		public List<Orders> Orders { get; set; }
		public List<MyOrderDetail> MyOrderDetails { get; set; }
	}

	//public class MyOrder
	//{
	//	public string SupplierName { get; set; }
	//	public Orders Orders { get; set; }
	//}

	public class MyOrderDetail
	{ 
		public string SupplierName { get; set; }
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public string PName { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public int Quantity { get; set; }
		public Nullable<decimal> SubTotal { get; set; }
		public Nullable<decimal> Total { get; set; }
		public string PMethod { get; set; }
		public string SMethod { get; set; }
	}
}