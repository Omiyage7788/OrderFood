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
    public class MembersController : Controller
    {
        private OrderMealContext db = new OrderMealContext();

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Members members)
        {
            if (ModelState.IsValid)
            {
                db.Entry(members).State = EntityState.Modified;
                db.Entry(members.MAccPwd).State = EntityState.Modified;
                db.SaveChanges();
                //ViewBag.MemDataChanged = "會員資料已更新!";
                return RedirectToAction("Details", new { id = members.MemberID});
            }
            return View(members);
        }

        public ActionResult OrderIndex(int? id, int page = 1)
        {
            var orders = db.Orders.Where(o => o.MemberID == id && o.OrderDisplay).OrderByDescending(o => o.OrderID)
                                .Include(o => o.PayMethod).Include(o => o.ShipMethod).Include(o=>o.OrderStatus).ToList();

            if (orders.Count == 0) {
                ViewBag.NoOrder = "目前尚無訂單資訊!";
            }

            int pageSize = 10;
            int currentPage = page < 1 ? 1 : page;
            var pagedOrders = orders.ToPagedList(currentPage, pageSize);

            ViewBag.page = page;

            return View(pagedOrders);
        }

        public ActionResult VMOrderDetail(int? id, int page)
        {
            VMOrderDetails vmod = new VMOrderDetails()
            {
                orders = db.Orders.Find(id),
                orderDetails = db.OrderDetails.Where(od => od.OrderID == id).ToList()
            };

            ViewBag.page = page;

            return View(vmod);
        }

        public ActionResult VMOrderIndex(int? id)
        {
            var memberID = int.Parse(Session["memberID"].ToString());
            var query = from o in db.Orders
						join od in db.OrderDetails on o.OrderID equals od.OrderID
						join p in db.Products on od.ProductID equals p.ProductID
						join s in db.Suppliers on p.SupplierID equals s.SupplierID
						join pm in db.PayMethod on o.PayVia equals pm.PayID
						join sm in db.ShipMethod on o.ShipVia equals sm.ShipID
						join os in db.OrderStatus on o.OrderState equals os.StatusID
						where o.OrderID == id
						select new MyOrderDetail
						{
							SupplierName = s.SupplierName,
                            OrderID = od.OrderID,
							ProductID = od.ProductID,
							PName = p.PName,
							Price = od.Price,
							Discount = od.Discount,
							Quantity = od.Quantity,
							PMethod = pm.Method,
							SMethod = sm.Method,
							SubTotal = (od.Price - od.Discount) * od.Quantity
						};

            VMOrder vmo = new VMOrder()
            {
                Orders = db.Orders.Where(o=>o.MemberID == memberID && o.OrderDisplay).ToList(),
                MyOrderDetails = query.ToList()
            };

            return View(vmo);
        }

        public ActionResult OrderDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        public ActionResult OrderEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.PayVia = new SelectList(db.PayMethod, "PayID", "Method", orders.PayVia);
            ViewBag.ShipVia = new SelectList(db.ShipMethod, "ShipID", "Method", orders.ShipVia);
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderEdit(Orders orders, int page)
        {
            if (ModelState.IsValid)
            {
                if (orders.ShipVia == 1)
                {
                    orders.ShipAddress =  "自取";
                }
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("VMOrderDetail", new { id = orders.OrderID, page});
            }
            
            ViewBag.PayVia = new SelectList(db.PayMethod, "PayID", "Method", orders.PayVia);
            ViewBag.ShipVia = new SelectList(db.ShipMethod, "ShipID", "Method", orders.ShipVia);
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RateOrder(int id, int Rate, int page)
        {
            var order = db.Orders.Find(id);
            order.Rate = (short)Rate;
            order.OrderState = 6;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("VMOrderDetail", new { id = order.OrderID, page });
        }

        public ActionResult OrderDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        [HttpPost, ActionName("OrderDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult OrderDeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            if (orders.OrderState == 1)
                orders.OrderState = 4;

            orders.OrderDisplay = false;
            db.Entry(orders).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("OrderIndex", new {id = orders.MemberID });
        }

        public ActionResult NotificationIndex(int id, int page = 1)
        {
            var messageID = db.SupMemNotification.Where(m => m.MemberID == id).Select(m => m.MemberID).ToList();
            var orders = db.Orders.Where(o => o.MemberID == id && (o.OrderState == 3 || o.OrderState == 6)).Select(o => o.OrderTitle).Distinct().ToList();

            foreach (var item in orders)
            {
                var sup = db.Suppliers.Where(s => s.SupplierName == item).Select(s => s.SupplierID).FirstOrDefault();
                var supMessage = db.SupMessage.Where(s => s.SupplierID == sup).Select(s => s.MessageID).ToList();

                foreach (var mID in supMessage)
                {
                    var check = db.SupMemNotification.Find(id, mID);

                    if (check == null)
                    {
                        SupMemNotification notification = new SupMemNotification
                        {
                            MemberID = id,
                            MessageID = mID,
                            SupMemDisplay = true
                        };

                        db.SupMemNotification.Add(notification);
                    }
                }
            }

            db.SaveChanges();

            var msgID = db.SupMemNotification.Where(m => m.MemberID == id && m.SupMemDisplay).OrderByDescending(m => m.MessageID)
                                .Select(m => m.MessageID).ToList();
            List<SupMessage> showList = new List<SupMessage>();
            foreach (var item in msgID)
            {
                var msg = db.SupMessage.Where(n => n.MessageID == item).Include(n => n.Suppliers).FirstOrDefault();
                showList.Add(msg);
            }
                 
            if (showList.Count() == 0)
            {
                ViewBag.NoData = "目前無推播訊息";
            }

            int pageSize = 10;
            int currentPage = page < 1 ? 1 : page;
            var pagedMessages = showList.ToPagedList(currentPage, pageSize);

            ViewBag.page = page;

            return View(pagedMessages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NotificationRead(int[] id, int page)
        {
            var memberID = int.Parse(Session["memberID"].ToString());
            foreach (var item in id)
            {
                SupMemNotification notification = db.SupMemNotification.Find(memberID, item);
                notification.Read = true;
            }
            db.SaveChanges();

            return RedirectToAction("NotificationIndex", new { id = memberID, page });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NotificationDelete(int id, int page)
        {
            var memID = int.Parse(Session["memberID"].ToString());
            SupMemNotification notification = db.SupMemNotification.Find(memID, id);

            notification.SupMemDisplay = false;
            db.SaveChanges();

            return RedirectToAction("NotificationIndex", new { id = memID, page });
        }

        public ActionResult AdminNotificationIndex(int page = 1)
        {
            var memberID = int.Parse(Session["memberID"].ToString());
            var msgID = db.AdminMemNotification.Where(a => a.MemberID == memberID && a.AdminMemDisplay).OrderByDescending(a => a.MessageID)
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

            return View(pagedMessages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminNotificationRead(int[] id, int page)
        {
            var memberID = int.Parse(Session["memberID"].ToString());
            foreach (var item in id)
            {
                AdminMemNotification notification = db.AdminMemNotification.Find(memberID, item);
                notification.Read = true;
            }
            db.SaveChanges(); 

            return RedirectToAction("AdminNotificationIndex", new { page });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminNotificationDelete(int id, int page)
        {
            var memberID = int.Parse(Session["memberID"].ToString());
            AdminMemNotification notification = db.AdminMemNotification.Find(memberID, id);

            notification.AdminMemDisplay = false;
            db.SaveChanges();

            return RedirectToAction("AdminNotificationIndex", new { page });
        }

        public ActionResult _getUnread()
        {
            var memberID = int.Parse(Session["memberID"].ToString());
            var count = db.SupMemNotification.Where(s => s.MemberID == memberID && !s.Read).Count()
                                + db.AdminMemNotification.Where(a => a.MemberID == memberID && !a.Read).Count();

            return PartialView(count);
        }

        public ActionResult _supUnread()
        {
            var memberID = int.Parse(Session["memberID"].ToString());
            var count = db.SupMemNotification.Where(s => s.MemberID == memberID && !s.Read).Count();

            return PartialView(count);
        }

        public ActionResult _adminUnread()
        {
            var memberID = int.Parse(Session["memberID"].ToString());
            var count = db.AdminMemNotification.Where(a => a.MemberID == memberID && !a.Read).Count();

            return PartialView(count);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Members members = db.Members.Find(id);
            db.Members.Remove(members);
            db.SaveChanges();
            return RedirectToAction("Index");
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
