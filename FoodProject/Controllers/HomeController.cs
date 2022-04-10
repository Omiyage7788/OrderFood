using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodProject.Models;
using FoodProject.ViewModels;

namespace FoodProject.Controllers
{
	public class HomeController : Controller
	{
		OrderMealContext db = new OrderMealContext();
		DataConnection dc = new DataConnection();

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(string area)
		{
			if (area.Length != 0)
			{
				ViewBag.Area = area;
				return View("ListStores");
			}

			ViewBag.Msg = "請輸入縣市或地區!";
			return View();
		}

		[HttpPost]
		public PartialViewResult _ListStores(string area, string keyWord)
		{
			if (area != null)
			{
				var suppliers = db.Suppliers.Where(s => s.SDistrict.Contains(area) || s.SCity.Contains(area) || area.Contains(s.SDistrict)).ToList();
				return PartialView(suppliers);
			}
			else
			{
				var suppliers = db.Suppliers.Where(s => s.SupplierName.Contains(keyWord)).ToList();
				var products = db.Products.Where(p => p.PName.Contains(keyWord)).Select(p => p.SupplierID).Distinct().ToList();

				foreach (var id in products)
				{
					var sup = db.Suppliers.Find(id);
					if (!suppliers.Contains(sup))
					{
						suppliers.Add(sup);
					}
				}

				return PartialView(suppliers);
			}
		}

		public ActionResult ListStores()
		{
			return View();
		}

		public ActionResult ListProducts(int? id)
		{
			VMProduct vmp = new VMProduct()
			{
				supplier = db.Suppliers.Find(id),
				products = db.Products.Where(p => p.SupplierID == id && p.Discontinuted == false).ToList()
			};

			return View(vmp);
		}

		public ActionResult MyCart()
		{
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(VMLogin login, string q)
		{
			var memberID = db.MAccPwds.Where(m => m.MAccount == login.MAccPwd.MAccount && m.MPassword == login.MAccPwd.MPassword).FirstOrDefault();

			if (memberID == null)
			{
				ViewBag.LoginMem = "帳號或密碼輸入錯誤!";
				return View();
			}

			var member = db.Members.Find(memberID.MemberID);

			if (!member.MAuthority)
			{
				ViewBag.LoginMem = "帳號暫時停用，請聯絡客服人員!";
				return View("Login");
			}

			Session["memberName"] = member.MName;
			Session["memberID"] = member.MemberID;

			if (q == "1")
				return RedirectToAction("Order");

			return RedirectToAction("Index");
		}

		public ActionResult Logout()
		{
			Session["memberName"] = null;
			Session["memberID"] = null;

			return View("Index");
		}

		public ActionResult CreateMember()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateMember(Members members)
		{
			if (ModelState.IsValid)
			{
				members.MAuthority = true;
				db.Members.Add(members);
				db.SaveChanges();
				return RedirectToAction("Login");
			}

			return View(members);
		}

		[CheckLoginState(IsUse = true)]
		public ActionResult Order()
		{
			Orders order = new Orders();
			order.OrderDate = DateTime.Today;
			order.RequiredDate = DateTime.Now.AddMinutes(30);

			ViewBag.ShipVia = new SelectList(db.ShipMethod, "ShipID", "Method");
			ViewBag.PayVia = new SelectList(db.PayMethod, "PayID", "Method");

			return View(order);
		}

		[HttpPost]
		public ActionResult Order(Orders order, string cart, string orderTitle)
		{
			order.MemberID = int.Parse(Session["memberID"].ToString());
			order.PayState = false;
			order.OrderDisplay = true;
			order.OrderState = 1;

			dc.Cmd.Parameters.AddWithValue("@OrderTitle", orderTitle);
			dc.Cmd.Parameters.AddWithValue("@MemberID", order.MemberID);
			dc.Cmd.Parameters.AddWithValue("@RequiredDate", order.RequiredDate);
			if (order.ShipVia == 1)
			{
				dc.Cmd.Parameters.AddWithValue("@ShipAddress", "自取");
			}
			else 
			{
				dc.Cmd.Parameters.AddWithValue("@ShipAddress", order.ShipAddress);
			}
			if (order.Note == null)
			{
				dc.Cmd.Parameters.AddWithValue("@Note", "無");
			}
			else
			{
				dc.Cmd.Parameters.AddWithValue("@Note", order.Note);
			}
			dc.Cmd.Parameters.AddWithValue("@ShipVia", order.ShipVia);
			dc.Cmd.Parameters.AddWithValue("@PayVia", order.PayVia);
			dc.Cmd.Parameters.AddWithValue("@PayState", order.PayState);
			dc.Cmd.Parameters.AddWithValue("@OrderDisplay", order.OrderDisplay);
			dc.Cmd.Parameters.AddWithValue("@OrderState", order.OrderState);
			dc.Cmd.Parameters.AddWithValue("@cart", cart);

			dc.executeProc("addOrders");

			TempData["Order"] = "Y";

			return RedirectToAction("MyCart");
		}

		//public ActionResult SupplierLogin()
		//{
		//	return View();
		//}

		[HttpPost]
		public ActionResult SupplierLogin(VMLogin login)
		{
			var supID = db.SAccPwd.Where(s => s.SAccount == login.SAccPwd.SAccount && s.SPassword == login.SAccPwd.SPassword).FirstOrDefault();

			if (supID == null)
			{
				ViewBag.LoginSup = "帳號或密碼輸入錯誤!";
				return View("Login");
			}

			var sup = db.Suppliers.Find(supID.SupplierID);

			if (!sup.SAuthority)
			{
				ViewBag.LoginSup = "帳號暫時停用，請聯絡客服人員!";
				return View("Login");
			}

			Session["supID"] = sup.SupplierID;

			return RedirectToAction("OrderIndex", "Suppliers", new { id = sup.SupplierID});
		}

		public ActionResult CreateSupplier()
		{
			VMSuppliers newSupplier = new VMSuppliers();
			newSupplier.Categories = db.Categories.ToList<Categories>();

			return View(newSupplier);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateSupplier(VMSuppliers vmSupplier, HttpPostedFileBase image)
		{
			if (ModelState.IsValid)
			{
				vmSupplier.Supplier.SAuthority = true;
				db.Suppliers.Add(vmSupplier.Supplier);
				var seletedCategories = vmSupplier.Categories.Where(c => c.IsSelected == true).ToList();
				for (int i = 0; i < seletedCategories.Count; i++)
				{
					SCategory sCategory = new SCategory
					{
						CategoryID = seletedCategories[i].CategoryID,
						SupplierID = vmSupplier.Supplier.SupplierID
					};
					db.SCategory.Add(sCategory);
				}
				if (image != null)
				{
					SPhoto newPhoto = new SPhoto();
					newPhoto.SupplierID = vmSupplier.Supplier.SupplierID;
					newPhoto.ImageFormat = image.ContentType;
					newPhoto.Photo = new byte[image.ContentLength];
					image.InputStream.Read(newPhoto.Photo, 0, image.ContentLength);
					db.SPhoto.Add(newPhoto);
				}
				db.SaveChanges();
				return RedirectToAction("Login");
			}

			return View(vmSupplier);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public FileContentResult GetSupplierImage(int? id)
		{
			var photo = db.SPhoto.Where(p => p.SupplierID == id).FirstOrDefault();
			if (photo == null)
				return null;

			return File(photo.Photo, photo.ImageFormat);
		}

		public FileContentResult GetProductImage(int? id)
		{
			var photo = db.Products.Find(id);
			if (photo == null)
				return null;

			return File(photo.PPhoto, photo.ImageFormat);
		}
	}
}