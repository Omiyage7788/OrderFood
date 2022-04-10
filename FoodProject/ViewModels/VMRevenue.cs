using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodProject.ViewModels
{
	public class VMRevenue
	{
		public List<DailyRevenue> DailyRevenues { get; set; }

		internal object ToPagedList(int currentPage, int pageSize)
		{
			throw new NotImplementedException();
		}
	}

	public class DailyRevenue
	{ 
		public DateTime Date { get; set; }
		public decimal Revenue { get; set; }
	}
}