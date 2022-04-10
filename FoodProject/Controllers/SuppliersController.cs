using System;
using System.Collections.Generic;
using System.Data;
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
    public class SuppliersController : Controller
    {
        private OrderMealContext db = new OrderMealContext();

        public ActionResult OrderIndex(int? id, int page = 1)
        {
            var supplier = db.Suppliers.Find(id);
            var orders = db.Orders.Where(o => o.OrderTitle == supplier.SupplierName && o.OrderState != 4).Include(o => o.Members)
                                                .Include(o => o.PayMethod).Include(o => o.ShipMethod).Include(o => o.OrderStatus).OrderByDescending(o => o.OrderID)
                                                .ToList();

            int pageSize = 10;
            int currentPage = page < 1 ? 1 : page;
            var pagedOrders = orders.ToPagedList(currentPage, pageSize);

            if (orders.Count == 0)
            {
                ViewBag.NoOrder = "目前尚無訂單資訊!";
            }

            ViewBag.page = page;

            return View("OrderIndex", "_Layout_Shop", pagedOrders);
        }

        public ActionResult VMOrderDetail(int? id, int page)
        {
            VMOrderDetails vmod = new VMOrderDetails()
            {
                orders = db.Orders.Find(id),
                orderDetails = db.OrderDetails.Where(od => od.OrderID == id).ToList()
            };

            ViewBag.page = page;

            return View("VMOrderDetail", "_Layout_Shop", vmod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderEstablished(Orders orders, int page)
        {
            if (ModelState.IsValid)
            {
                orders.OrderState = 2;
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OrderIndex", new { id = Session["supID"], page });
            }

            return RedirectToAction("VMOrderDetail", new { id = orders.OrderID, page });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderCanceled(Orders orders, int page)
        {
            if (ModelState.IsValid)
            {
                orders.OrderState = 5;
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OrderIndex", new { id = Session["supID"], page });
            }

            return RedirectToAction("VMOrderDetail", new { id = orders.OrderID, page });
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers suppliers = db.Suppliers.Find(id);
            if (suppliers == null)
            {
                return HttpNotFound();
            }

            var categories = db.Categories.ToList<Categories>();
            var selectedList = suppliers.SCategory.ToList();
            for (int i = 0; i < suppliers.SCategory.Count; i++)
            {
                int j = selectedList[i].CategoryID - 1;
                categories[j].IsSelected = true;
            }


            VMSuppliers vmSupplier = new VMSuppliers
            {
                Supplier = suppliers,
                Categories = categories
            };

            return View("Details", "_Layout_Shop", vmSupplier);
        }

        public ActionResult Logout()
        {
            Session["supID"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers suppliers = db.Suppliers.Find(id);
            if (suppliers == null)
            {
                return HttpNotFound();
            }

            var categories = db.Categories.ToList<Categories>();
            var selectedList = suppliers.SCategory.ToList();
            for (int i = 0; i < suppliers.SCategory.Count; i++)
            {
                int j = selectedList[i].CategoryID - 1;
                categories[j].IsSelected = true;
            }
            

            VMSuppliers vmSupplier = new VMSuppliers
            {
                Supplier = suppliers,
                Categories = categories
            };

            return View("Edit", "_Layout_Shop", vmSupplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VMSuppliers vmSupplier, HttpPostedFileBase image, int PhotoID)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vmSupplier.Supplier).State = EntityState.Modified;
                db.Entry(vmSupplier.Supplier.SAccPwd).State = EntityState.Modified;

                List<SCategory> sc = db.SCategory.Where(s => s.SupplierID == vmSupplier.Supplier.SupplierID).ToList();
                foreach (var item in sc)
                {
                    db.SCategory.Remove(item);
                }

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
                    SPhoto newPhoto = db.SPhoto.Find(PhotoID);
                    newPhoto.ImageFormat = image.ContentType;
                    newPhoto.Photo = new byte[image.ContentLength];
                    image.InputStream.Read(newPhoto.Photo, 0, image.ContentLength);
                    db.Entry(newPhoto).State = EntityState.Modified;
                }

                db.SaveChanges();
                //ViewBag.SupDataChanged = "店家資料已更新!";
                return RedirectToAction("Details", new { id = vmSupplier.Supplier.SupplierID});
            }
            return View("Edit", "_Layout_Shop", vmSupplier);
        }

        public ActionResult ProductIndex(int? id, int page=1)
        {
            var products = db.Products.Where(p => p.SupplierID == id).OrderByDescending(p => p.ProductID).Include(p => p.Suppliers).ToList();
            ViewBag.supName = db.Suppliers.Find(id).SupplierName;

            int pageSize = 10;
            int currentPage = page < 1 ? 1 : page;
            var pagedProducts = products.ToPagedList(currentPage, pageSize);

            if (products.Count().Equals(0))
            {
                ViewBag.NoProduct = "目前尚無商品";
            }

            ViewBag.page = page;

            return View("ProductIndex", "_Layout_Shop", pagedProducts);
        }

        public ActionResult ProductCreate(int page)
        {
            ViewBag.page = page;

            return View("ProductCreate", "_Layout_Shop");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCreate(Products products, HttpPostedFileBase image)
        {
            products.SupplierID = int.Parse(Session["supID"].ToString());

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    products.ImageFormat = image.ContentType;
                    products.PPhoto = new byte[image.ContentLength];
                    image.InputStream.Read(products.PPhoto, 0, image.ContentLength);
                }
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("ProductIndex", new { id = products.SupplierID });
            }

            return View("ProductCreate", "_Layout_Shop", products);
        }

        public ActionResult ProductEdit(int? id, int page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }

            ViewBag.page = page;
            
            return View("ProductEdit", "_Layout_Shop", products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductEdit(Products products, HttpPostedFileBase image, int page)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    products.ImageFormat = image.ContentType;
                    products.PPhoto = new byte[image.ContentLength];
                    image.InputStream.Read(products.PPhoto, 0, image.ContentLength);
                }

                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProductIndex", new { id = products.SupplierID, page});
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "SupplierName", products.SupplierID);
            return View("ProductEdit", "_Layout_Shop", products);
        }

        public ActionResult NotificationIndex(int? id, int page = 1)
        {
            var supMessage = db.SupMessage.Where(m => m.SupplierID == id).OrderByDescending(m => m.MessageID).ToList();

            int pageSize = 10;
            int currentPage = page < 1 ? 1 : page;
            var pagedMessages = supMessage.ToPagedList(currentPage, pageSize);

            if (supMessage.Count().Equals(0))
            {
                ViewBag.NoData = "目前無推播訊息";
            }

            ViewBag.page = page;

            return View("NotificationIndex", "_Layout_Shop", pagedMessages);
        }

        public ActionResult NotificationCreate(int page)
        {
            ViewBag.page = page;

            return View("NotificationCreate", "_Layout_Shop");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NotificationCreate(SupMessage supMessage)
        {
            supMessage.SupplierID = int.Parse(Session["supID"].ToString());
            
            if (ModelState.IsValid)
            {
                db.SupMessage.Add(supMessage);

                var sup = db.Suppliers.Find(supMessage.SupplierID);
                var orders = db.Orders.Where(o => o.OrderTitle == sup.SupplierName && (o.OrderState == 3 || o.OrderState == 6))
                                    .Select(o => o.MemberID).Distinct().ToList();
                foreach (var item in orders)
                {
                    SupMemNotification notification = new SupMemNotification();
                    notification.MemberID = item;
                    notification.MessageID = supMessage.MessageID;
                    notification.SupMemDisplay = true;
                    db.SupMemNotification.Add(notification);
                }

                db.SaveChanges();
                return RedirectToAction("NotificationIndex", new { id = supMessage.SupplierID });
            }

            return View("NotificationCreate", "_Layout_Shop", supMessage);
        }

        public ActionResult NotificationDelete(int? id, int page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupMessage supMessage = db.SupMessage.Find(id);
            if (supMessage == null)
            {
                return HttpNotFound();
            }

            ViewBag.page = page;

            return View("NotificationDelete", "_Layout_Shop", supMessage);
        }

        [HttpPost, ActionName("NotificationDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult NotificationDeleteConfirmed(int id, int page)
        {
            SupMessage supMessage = db.SupMessage.Find(id);

            var notification = db.SupMemNotification.Where(s => s.MessageID == supMessage.MessageID).ToList();
            foreach (var item in notification)
            {
                db.SupMemNotification.Remove(item);
            }

            db.SupMessage.Remove(supMessage);
            db.SaveChanges();
            return RedirectToAction("NotificationIndex", new { id = supMessage.SupplierID, page });
        }

        public ActionResult AdminNotificationIndex(int page = 1)
        {
            var supID = int.Parse(Session["supID"].ToString());
            var msgID = db.AdminSupNotification.Where(a => a.SupplierID == supID && a.AdminSupDisplay).OrderByDescending(a => a.MessageID)
                                .Select(a => a.MessageID).ToList();

            List<AdminMessage> showList = new List<AdminMessage>();
            foreach (var item in msgID)
            {
                var msg = db.AdminMessages.Where(m => m.MessageID == item).FirstOrDefault();
                showList.Add(msg);
            }

            int pageSize = 10;
            int currentPage = page < 1 ? 1 : page;
            var pagedMessages = showList.ToPagedList(currentPage, pageSize);

            if (showList.Count() == 0)
            {
                ViewBag.NoData = "目前無推播訊息";
            }

            ViewBag.page = page;

            return View("AdminNotificationIndex", "_Layout_Shop", pagedMessages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminNotificationRead(int[] id, int page)
        {
            var supID = int.Parse(Session["supID"].ToString());
            foreach (var item in id)
            {
                AdminSupNotification notification = db.AdminSupNotification.Find(supID, item);
                notification.Read = true;
            }
            db.SaveChanges();

            return RedirectToAction("AdminNotificationIndex", new { page });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminNotificationDelete(int id, int page)
        {
            var supID = int.Parse(Session["supID"].ToString());
            AdminSupNotification notification = db.AdminSupNotification.Find(supID, id);

            notification.AdminSupDisplay = false;
            notification.Read = true;
            db.SaveChanges();

            return RedirectToAction("AdminNotificationIndex", new { page});
        }

        public ActionResult Revenue(int? id, int page = 1)
        {
            var supplier = db.Suppliers.Find(id);
            var query1 = from o in db.Orders
                        join od in db.OrderDetails on o.OrderID equals od.OrderID
                        where o.OrderTitle == supplier.SupplierName && (o.OrderState == 3 || o.OrderState == 6)
						 let t = DbFunctions.TruncateTime(o.RequiredDate)
						 group od by new { o.OrderID, t } into r
                        select new { orderID = r.Key.OrderID, date = r.Key.t, total = r.Sum(od => (od.Price - od.Discount) * od.Quantity) };

            var query2 = from q in query1
                         group q by new { q.date } into s
                         select new DailyRevenue{ Date = (DateTime)s.Key.date, Revenue = s.Sum(q => q.total)};


            VMRevenue revenue = new VMRevenue()
            {
                DailyRevenues = query2.ToList()
            };

			int pageSize = 15;
			int currentPage = page < 1 ? 1 : page;
			var pagedRevenue = revenue.DailyRevenues.ToPagedList(currentPage, pageSize);

			if (revenue.DailyRevenues.Count() == 0)
            {
                ViewBag.NoData = "目前尚無營業額資訊!";
            }

            return View("Revenue", "_Layout_Shop", pagedRevenue);
        }

        public ActionResult _getUnread()
        {
            var supID = int.Parse(Session["supID"].ToString());
            var count = db.AdminSupNotification.Where(a => a.SupplierID == supID && !a.Read).Count();

            return PartialView(count);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
