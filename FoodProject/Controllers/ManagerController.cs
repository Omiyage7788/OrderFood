using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodProject.Models;
using FoodProject.ViewModels;
using PagedList;

namespace FoodProject.Controllers
{
	[CheckLoginState(IsUse = true)]
	public class ManagerController : Controller
    {
        OrderMealContext db = new OrderMealContext();

		[CheckLoginState(IsUse = false)]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(string AdminAccount, string AdminPassword)
		{
			var adminID = db.AdminAccPwd.Where(a => a.AdminAccount == AdminAccount && a.AdminPassword == AdminPassword).FirstOrDefault();

			if (adminID == null)
			{
				ViewBag.LoginMsg = "帳號或密碼輸入錯誤!";
				return View();
			}

			var admin = db.Administrators.Find(adminID.AdminID);
			Session["adminID"] = admin.AdminID;

			if (admin.AdminAuthority)
			{
				Session["highest"] = true;
			}

			return RedirectToAction("MembersIndex");
		}

		public ActionResult Logout()
		{
			Session["adminID"] = null;
			Session["highest"] = null;

			return RedirectToAction("Login");
		}

		public ActionResult MembersIndex(int page = 1)
		{
			var mem = db.Members.ToList();

			int pageSize = 10;
			int currentPage = page < 1 ? 1 : page;
			var pagedMembers = mem.ToPagedList(currentPage, pageSize);

			ViewBag.page = page;

			return View(pagedMembers);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult MemberEdit(int id)
		{
			var mem = db.Members.Find(id);

			mem.MAuthority = !mem.MAuthority;
			db.SaveChanges();

			return RedirectToAction("MembersIndex");
		}

		public ActionResult MemberOrders(int? id, int pageMember, int page = 1)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var mem = db.Members.Find(id);
			if (mem == null)
			{
				return HttpNotFound();
			}

			ViewBag.memName = mem.MName;
			var order = db.Orders.Where(o => o.MemberID == id).OrderByDescending(o => o.OrderID).ToList();

			int pageSize = 10;
			int currentPage = page < 1 ? 1 : page;
			var pagedOrders = order.ToPagedList(currentPage, pageSize);

			if (order.Count() == 0)
				ViewBag.NoOrder = "目前尚無訂單";

			ViewBag.pageMember = pageMember;

			return View(pagedOrders);
		}

		public ActionResult SuppliersIndex(int page = 1)
		{
			var sup = db.Suppliers.ToList();

			int pageSize = 10;
			int currentPage = page < 1 ? 1 : page;
			var pagedSuppliers = sup.ToPagedList(currentPage, pageSize);

			ViewBag.page = page;

			return View(pagedSuppliers);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SupplierEdit(int id, string route, int? page)
		{
			var sup = db.Suppliers.Find(id);

			sup.SAuthority = !sup.SAuthority;
			db.SaveChanges();

			if (route.Equals("index"))
				return RedirectToAction("SuppliersIndex");

			return RedirectToAction("SupplierDetails", new { id, page});
		}

		public ActionResult SupplierDetails(int? id, int page)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var sup = db.Suppliers.Find(id);
			if (sup == null)
			{
				return HttpNotFound();
			}

			var categories = db.Categories.ToList<Categories>();
			var selectedList = sup.SCategory.ToList();
			for (int i = 0; i < sup.SCategory.Count; i++)
			{
				int j = selectedList[i].CategoryID - 1;
				categories[j].IsSelected = true;
			}
			

			VMSuppliers vms = new VMSuppliers()
			{
				Supplier = sup,
				Categories = categories,
				Products = db.Products.Where(p => p.SupplierID == sup.SupplierID).ToList()
			};

			ViewBag.page = page;

			return View(vms);
		}

		public ActionResult SupplierNotifications(int? id, int page = 1)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var sup = db.Suppliers.Find(id);
			if (sup == null)
			{
				return HttpNotFound();
			}

			ViewBag.supName = sup.SupplierName;
			var notifications = db.SupMessage.Where(s => s.SupplierID == sup.SupplierID).OrderByDescending(s => s.MessageID).ToList();

			int pageSize = 10;
			int currentPage = page < 1 ? 1 : page;
			var pagedNotifications = notifications.ToPagedList(currentPage, pageSize);

			if (notifications.Count().Equals(0))
			{
				ViewBag.NoNotifications = "目前無推播訊息";
			}

			return View(pagedNotifications);
		}

		public ActionResult OrdersIndex(int page = 1)
		{
			var order = db.Orders.OrderByDescending(o => o.OrderID).ToList();

			int pageSize = 10;
			int currentPage = page < 1 ? 1 : page;
			var pagedOrders = order.ToPagedList(currentPage, pageSize);

			return View(pagedOrders);
		}

		public ActionResult VMOrderDetails(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}

			VMOrderDetails vmod = new VMOrderDetails()
			{
				orders = order,
				orderDetails = db.OrderDetails.Where(od => od.OrderID == id).ToList()
			};

			return View(vmod);
		}

		public ActionResult AdminNotificationIndex(int page = 1)
		{
			var adminMessage = db.AdminMessages.OrderByDescending(m => m.MessageID).ToList();

			int pageSize = 10;
			int currentPage = page < 1 ? 1 : page;
			var pagedNotifications = adminMessage.ToPagedList(currentPage, pageSize);

			if (adminMessage.Count().Equals(0))
			{
				ViewBag.NoData = "目前無推播訊息";
			}

			ViewBag.page = page;

			return View(pagedNotifications);
		}

		public ActionResult AdminNotificationCreate(int page)
		{
			ViewBag.MessageRecipient = new SelectList(db.ReceiveGroup, "RecipientID", "Recipient");
			ViewBag.page = page;

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AdminNotificationCreate(AdminMessage adminMessage, int page)
		{
			adminMessage.AdminID = int.Parse(Session["adminID"].ToString());

			if (ModelState.IsValid)
			{
				db.AdminMessages.Add(adminMessage);

				if (adminMessage.MessageRecipient == 2)
				{
					NotifySuppliers(adminMessage);
				}
				else if (adminMessage.MessageRecipient == 3)
				{
					NotifyMembers(adminMessage);
				}
				else
				{
					NotifySuppliers(adminMessage);
					NotifyMembers(adminMessage);
				}

				db.SaveChanges();
				return RedirectToAction("AdminNotificationIndex");
			}

			return View("AdminNotificationCreate", adminMessage);
		}

		public ActionResult AdminNotificationDelete(int? id, int page)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AdminMessage adminMessage = db.AdminMessages.Find(id);
			if (adminMessage == null)
			{
				return HttpNotFound();
			}

			ViewBag.page = page;

			return View("AdminNotificationDelete", adminMessage);
		}

		[HttpPost, ActionName("AdminNotificationDelete")]
		[ValidateAntiForgeryToken]
		public ActionResult NotificationDeleteConfirmed(int id, int page)
		{
			AdminMessage adminMessage = db.AdminMessages.Find(id);

			if (adminMessage.MessageRecipient == 2)
			{
				DeleteSupNotify(adminMessage);
			}
			else if (adminMessage.MessageRecipient == 3)
			{
				DeleteMemNotify(adminMessage);
			}
			else
			{
				DeleteSupNotify(adminMessage);
				DeleteMemNotify(adminMessage);
			}

			db.AdminMessages.Remove(adminMessage);
			db.SaveChanges();
			return RedirectToAction("AdminNotificationIndex", new { page });
		}

		public ActionResult SupNotificationsIndex(int page = 1)
		{
			var notifications = db.SupMessage.OrderByDescending(m => m.MessageID).ToList();

			int pageSize = 10;
			int currentPage = page < 1 ? 1 : page;
			var pagedNotifications = notifications.ToPagedList(currentPage, pageSize);

			return View(pagedNotifications);
		}

		public ActionResult AdminManagementIndex(int page = 1)
		{
			if (Session["highest"] == null)
			{
				return RedirectToAction("Login");
			}
			
			var admins = db.Administrators.Where(a => a.AdminID != 1).ToList();

			int pageSize = 10;
			int currentPage = page < 1 ? 1 : page;
			var pagedAdmins = admins.ToPagedList(currentPage, pageSize);

			return View(pagedAdmins);
		}

		public ActionResult AdminManagementCreate()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AdminManagementCreate(Administrators admin)
		{
			if (ModelState.IsValid)
			{
				db.Administrators.Add(admin);
				db.SaveChanges();
				return RedirectToAction("AdminManagementIndex");
			}

			return View(admin);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AdminManagementEdit(int id)
		{
			var admin = db.Administrators.Find(id);

			admin.AdminAuthority = !admin.AdminAuthority;
			db.SaveChanges();

			return RedirectToAction("AdminManagementIndex");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AdminManagementDelete(int id)
		{
			var admin = db.Administrators.Find(id);

			db.AdminAccPwd.Remove(admin.AdminAccPwd);
			db.Administrators.Remove(admin);
			db.SaveChanges();

			return RedirectToAction("AdminManagementIndex");
		}

		public ActionResult AdminDetails(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Administrators admin = db.Administrators.Find(id);
			if (admin == null)
			{
				return HttpNotFound();
			}
			return View(admin);
		}

		public ActionResult AdminEdit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Administrators admin = db.Administrators.Find(id);
			if (admin == null)
			{
				return HttpNotFound();
			}
			return View(admin);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AdminEdit(Administrators admin)
		{
			if (ModelState.IsValid)
			{
				db.Entry(admin).State = EntityState.Modified;
				db.Entry(admin.AdminAccPwd).State = EntityState.Modified;
				db.SaveChanges();
				//ViewBag.MemDataChanged = "會員資料已更新!";
				return RedirectToAction("AdminDetails", new { id = admin.AdminID });
			}
			return View(admin);
		}

		public void NotifyMembers(AdminMessage adminMessage)
		{
			var mem = db.Members.ToList();

			foreach (var item in mem)
			{
				AdminMemNotification notification = new AdminMemNotification()
				{ 
					MemberID = item.MemberID,
					MessageID = adminMessage.MessageID,
					AdminMemDisplay = true
				};

				db.AdminMemNotification.Add(notification);
			}
		}

		public void NotifySuppliers(AdminMessage adminMessage)
		{
			var sup = db.Suppliers.ToList();

			foreach (var item in sup)
			{
				AdminSupNotification notification = new AdminSupNotification() 
				{ 
					SupplierID = item.SupplierID,
					MessageID = adminMessage.MessageID,
					AdminSupDisplay = true
				};

				db.AdminSupNotification.Add(notification);
			}
		}

		public void DeleteMemNotify(AdminMessage adminMessage)
		{
			var notification = db.AdminMemNotification.Where(a => a.MessageID == adminMessage.MessageID).ToList();
			foreach (var item in notification)
			{
				db.AdminMemNotification.Remove(item);
			}
		}

		public void DeleteSupNotify(AdminMessage adminMessage)
		{
			var notification = db.AdminSupNotification.Where(a => a.MessageID == adminMessage.MessageID).ToList();
			foreach (var item in notification)
			{
				db.AdminSupNotification.Remove(item);
			}
		}
	}
}