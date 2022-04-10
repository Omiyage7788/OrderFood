using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodProject.Controllers
{
    public class SetSessionController : Controller
    {
        public ActionResult SetVariable(string key, string value)
        {
            if (value == "clear")
                Session[key] = null;
            else
                Session[key] = value;

            return this.Json(new { success = true });
        }
    }
}