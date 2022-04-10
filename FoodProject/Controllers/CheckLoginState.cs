using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoodProject.Controllers
{
	public class CheckLoginState:ActionFilterAttribute
	{
		public bool IsUse { get; set; }

		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			HttpContext context = HttpContext.Current;
			if (IsUse)
				LoginRoute(filterContext.RouteData, context);
		}

		void LoginRoute(RouteData routeData, HttpContext context) 
		{
			var contollerName = routeData.Values["controller"].ToString();
			var actionName = routeData.Values["action"].ToString();

			if (contollerName == "Members")
				LoginStateMember(context);
			else if (contollerName == "Suppliers")
				LoginStateSupplier(context);
			else if (contollerName == "Manager" && actionName != "Login")
				LoginStateManager(context);
			else if (actionName == "Order")
				LoginStateMemberOrder(context);
		}

		void LoginStateMemberOrder(HttpContext context)
		{
			if (context.Session["memberID"] == null)
				context.Response.Redirect("/Home/Login?q=1");
		}

		void LoginStateMember(HttpContext context)
		{
			if (context.Session["memberID"] == null)
				context.Response.Redirect("/Home/Login");
		}

		void LoginStateSupplier(HttpContext context)
		{
			if (context.Session["supID"] == null)
				context.Response.Redirect("/Home/Login");
		}

		void LoginStateManager(HttpContext context)
		{
			if (context.Session["adminID"] == null)
				context.Response.Redirect("/Manager/Login");
		}
	}
}