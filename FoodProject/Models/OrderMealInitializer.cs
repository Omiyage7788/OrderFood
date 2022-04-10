using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;


namespace FoodProject.Models
{
	//public class OrderMealInitializer : DropCreateDatabaseAlways<OrderMealContext>
    public class OrderMealInitializer :DropCreateDatabaseIfModelChanges<OrderMealContext>
    {
        //並override覆寫Seed方法,建構Model初始值
        protected override void Seed(OrderMealContext context)
        {
            base.Seed(context);

            List<Members> members = new List<Members>
            {
                 new Members
                {
                   MName="路平",
                    MCellphone="0910111111",
                    MEmail="m1@member.com",
                    MAuthority=true
                },
               new Members {
                    MName = "奈威",
                    MCellphone="0910222222",
                    MEmail="m2@member.com",
                    MAuthority=true
                },
                new Members {
                    MName = "衛斯理",
                    MCellphone="0910333333",
                    MEmail="m3@member.com",
                    MAuthority=true
                },
                new Members {
                    MName = "格蘭傑",
                    MCellphone="0910444444",
                    MEmail="m4@member.com",
                    MAuthority=true
                }
            };

            members.ForEach(s => context.Members.Add(s));
            context.SaveChanges();


            List<MAccPwd> mAccPwds = new List<MAccPwd>
            {
                new MAccPwd
                {
                    MemberID = 1,
                    MAccount = "aaaaa",
                    MPassword = "11111"
                },
                new MAccPwd {
                   MemberID = 2,
                    MAccount = "bbbbb",
                    MPassword = "22222"
                },
                new MAccPwd {
                    MemberID = 3,
                    MAccount = "ccccc",
                    MPassword = "33333"
                },
                  new MAccPwd {
                    MemberID = 4,
                    MAccount = "ddddd",
                    MPassword = "44444"
                }
            };

            mAccPwds.ForEach(s => context.MAccPwds.Add(s));
            context.SaveChanges();


            List<Administrators> administrators = new List<Administrators>
            {
                 new Administrators
                {
                     AdminName = "最高權限",
                     AdminAuthority = true
                },
               new Administrators {
                   AdminName = "客服1",
                   AdminAuthority = false
                },
                new Administrators {
                    AdminName = "客服2",
                    AdminAuthority = false
                }
            };

            administrators.ForEach(s => context.Administrators.Add(s));
            context.SaveChanges();


            List<AdminAccPwd> adminAccPwds = new List<AdminAccPwd>
            {
                new AdminAccPwd
                {
                    AdminID = 1,
                    AdminAccount = "aaaaaa",
                    AdminPassword = "111111"
                },
                new AdminAccPwd {
                    AdminID = 2,
                    AdminAccount = "bbbbbb",
                    AdminPassword = "111111"
                },
                new AdminAccPwd {
                    AdminID = 3,
                    AdminAccount = "cccccc",
                    AdminPassword = "111111"
                }
            };

            adminAccPwds.ForEach(s => context.AdminAccPwd.Add(s));
            context.SaveChanges();


            List<ReceiveGroup> receiveGroups = new List<ReceiveGroup>
            {
                 new ReceiveGroup
                {
                     Recipient = "所有人"
                },
               new ReceiveGroup {
                   Recipient = "店家"
                },
                new ReceiveGroup {
                    Recipient = "客戶"
                }
            };

            receiveGroups.ForEach(s => context.ReceiveGroup.Add(s));
            context.SaveChanges();

            List<Categories> categories = new List<Categories>
            {
                new Categories
                {
                    CategoryName = "麵食"
                },
                new Categories {
                   CategoryName = "便當"
                },
                new Categories {
                    CategoryName = "披薩"
                },
                  new Categories {
                   CategoryName = "飲料"
                },
                  new Categories {
                   CategoryName = "中式"
                },
                  new Categories {
                   CategoryName = "美式"
                },
                  new Categories {
                   CategoryName = "日韓"
                },
                  new Categories {
                   CategoryName = "東南亞"
                }
            };

            categories.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();


            List<PayMethod> payMethods = new List<PayMethod>
            {
                new PayMethod
                {
                    Method = "現金"
                },
                new PayMethod {
                    Method = "信用卡"
                },
                new PayMethod {
                     Method = "轉帳"
                }
            };

            payMethods.ForEach(s => context.PayMethod.Add(s));
            context.SaveChanges();


            List<ShipMethod> shipMethods = new List<ShipMethod>
            {
                new ShipMethod
                {
                    Method = "自取"
                },
                new ShipMethod {
                    Method = "外送"
                }
            };

            shipMethods.ForEach(s => context.ShipMethod.Add(s));
            context.SaveChanges();


            List<OrderStatus> orderStatuses = new List<OrderStatus>
            {
                new OrderStatus
                {
                    Status = "訂單處理中"
                },
                new OrderStatus {
                    Status = "店家已接單"
                },
                new OrderStatus {
                    Status = "餐點已送達"
                },
                new OrderStatus {
                    Status = "訂單已取消"
                },
                new OrderStatus {
                    Status = "店家取消此訂單"
                },
                new OrderStatus {
                    Status = "已評價"
                }
            };

            orderStatuses.ForEach(s => context.OrderStatus.Add(s));
            context.SaveChanges();


            List<Suppliers> suppliers = new List<Suppliers>
            {
                new Suppliers
                {
                    SupplierName = "老郭燒肉飯",
                    SCellphone = "0912111111",
                    SPhone = "072801111",
                    SEmail = "s1@food.com",
                    SCity = "高雄市",
                    SDistrict = "新興區",
                    SRoad = "忠孝二路10號",
                    SAuthority = true
                },
                new Suppliers {
                    SupplierName = "品好麵館",
                    SCellphone = "0912222222",
                    SPhone = "075502222",
                    SEmail = "s2@food.com",
                    SCity = "高雄市",
                    SDistrict = "鼓山區",
                    SRoad = "明華路19號",
                    BusinessHour = "星期一  11:00–19:30\r\n星期二  11:00–19:30\r\n星期三  11:00–19:30\r\n星期四  11:00–19:30星期五  11:00–19:30\r\n星期六  10:30–20:00\r\n星期日  10:30–20:00",
                    SAuthority = true
                },
                new Suppliers {
                    SupplierName = "沐恩拉麵",
                    SCellphone = "0912333333",
                    SPhone = "075503333",
                    SEmail = "s3@food.com",
                    SCity = "高雄市",
                    SDistrict = "鼓山區",
                    SRoad = "文信路37號",
                    SAuthority = true
                },
                  new Suppliers {
                    SupplierName = "好夥伴快餐",
                    SCellphone = "0912444444",
                    SPhone = "075504444",
                    SEmail = "s4@food.com",
                    SCity = "高雄市",
                    SDistrict = "鼓山區",
                    SRoad = "裕誠路169號",
                    SAuthority = true
                },
                  new Suppliers {
                    SupplierName = "立稼披薩",
                    SCellphone = "0912555555",
                    SPhone = "075505555",
                    SEmail = "s5@food.com",
                    SCity = "高雄市",
                    SDistrict = "鼓山區",
                    SRoad = "華榮路229號",
                    SAuthority = true
                },
                  new Suppliers {
                    SupplierName = "魯蛇便當",
                    SCellphone = "0912666666",
                    SPhone = "075506666",
                    SEmail = "s6@food.com",
                    SCity = "高雄市",
                    SDistrict = "鼓山區",
                    SRoad = "慶豐街47號",
                    SAuthority = true
                },
                  new Suppliers {
                    SupplierName = "吃光食堂",
                    SCellphone = "0912777777",
                    SPhone = "075807777",
                    SEmail = "s7@food.com",
                    SCity = "高雄市",
                    SDistrict = "左營區",
                    SRoad = "左營大路61號",
                    SAuthority = true
                },
                  new Suppliers {
                    SupplierName = "兜萊簡餐",
                    SCellphone = "0912888888",
                    SPhone = "075808888",
                    SEmail = "s8@food.com",
                    SCity = "高雄市",
                    SDistrict = "左營區",
                    SRoad = "西陵街217號",
                    SAuthority = true
                },
                  new Suppliers {
                    SupplierName = "元氣牛肉麵",
                    SCellphone = "0912999999",
                    SPhone = "075809999",
                    SEmail = "s9@food.com",
                    SCity = "高雄市",
                    SDistrict = "左營區",
                    SRoad = "軍校路261號",
                    SAuthority = true
                }
            };

            suppliers.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges();

            //for debug
            //try
            //{
            //    context.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    var entityError = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
            //    var getFullMessage = string.Join("; ", entityError);
            //    var exceptionMessage = string.Concat(ex.Message, "errors are: ", getFullMessage);
            //}

            List<SAccPwd> sAccPwds = new List<SAccPwd>
            {
                new SAccPwd
                {
                     SupplierID = 1,
                     SAccount = "zzzzz",
                     SPassword = "11111"
                },
                new SAccPwd {
                    SupplierID = 2,
                    SAccount = "yyyyy",
                    SPassword = "11111"
                },
                new SAccPwd {
                    SupplierID = 3,
                    SAccount = "xxxxx",
                    SPassword = "11111"
                },
                  new SAccPwd {
                    SupplierID = 4,
                    SAccount = "wwwww",
                    SPassword = "11111"
                },
                  new SAccPwd {
                    SupplierID = 5,
                    SAccount = "vvvvv",
                    SPassword = "11111"
                },
                  new SAccPwd {
                    SupplierID = 6,
                    SAccount = "uuuuu",
                    SPassword = "11111"
                },
                  new SAccPwd {
                    SupplierID = 7,
                    SAccount = "ttttt",
                    SPassword = "11111"
                },
                  new SAccPwd {
                    SupplierID = 8,
                    SAccount = "sssss",
                    SPassword = "11111"
                },
                  new SAccPwd {
                    SupplierID = 9,
                    SAccount = "rrrrr",
                    SPassword = "11111"
                }
            };

            sAccPwds.ForEach(s => context.SAccPwd.Add(s));
            context.SaveChanges();


            List<SCategory> sCategories = new List<SCategory>
            {
                new SCategory
                {
                    CategoryID = 2,
                    SupplierID = 1
                },
                new SCategory {
                    CategoryID = 1,
                    SupplierID = 2
                },
                new SCategory {
                    CategoryID = 1,
                    SupplierID = 3
                },
                new SCategory {
                    CategoryID = 2,
                    SupplierID = 4
                },
                new SCategory {
                    CategoryID = 3,
                    SupplierID = 5
                },
                new SCategory {
                    CategoryID = 2,
                    SupplierID = 6
                },
                new SCategory {
                    CategoryID = 2,
                    SupplierID = 7
                },
                new SCategory {
                    CategoryID = 2,
                    SupplierID = 8
                },
                new SCategory {
                    CategoryID = 1,
                    SupplierID = 9
                }
            };

            sCategories.ForEach(s => context.SCategory.Add(s));
            context.SaveChanges();


            List<Products> products = new List<Products>
            {
                new Products
                {
                    PName = "燒肉飯(大)",
                    Price = 60,
                    PPhoto = getFileBytes("\\SourcePhotos\\1-1.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 5,
                    Discontinuted = false,
                    SupplierID = 1
                },
                new Products {
                   PName = "燒肉飯(小)",
                    Price = 40,
                    PPhoto = getFileBytes("\\SourcePhotos\\1-1.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 0,
                    Discontinuted = false,
                    SupplierID = 1
                },
                new Products {
                   PName = "滷肉飯(大)",
                    Price = 60,
                    PPhoto = getFileBytes("\\SourcePhotos\\1-2.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 10,
                    Discontinuted = false,
                    SupplierID = 1
                },
                new Products {
                   PName = "滷肉飯(小)",
                    Price = 40,
                    PPhoto = getFileBytes("\\SourcePhotos\\1-2.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 5,
                    Discontinuted = false,
                    SupplierID = 1
                },
                new Products {
                   PName = "滷蛋",
                    Price = 10,
                    PPhoto = getFileBytes("\\SourcePhotos\\1-3.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 0,
                    Discontinuted = false,
                    SupplierID = 1
                },
                new Products {
                   PName = "乾麵",
                    Price = 45,
                    PPhoto = getFileBytes("\\SourcePhotos\\2-1.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 0,
                    Discontinuted = false,
                    SupplierID = 2
                },
                new Products {
                   PName = "湯麵",
                    Price = 45,
                    PPhoto = getFileBytes("\\SourcePhotos\\2-2.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 0,
                    Discontinuted = false,
                    SupplierID = 2
                },
                new Products {
                   PName = "餛飩麵",
                    Price = 60,
                    PPhoto = getFileBytes("\\SourcePhotos\\2-3.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 5,
                    Discontinuted = false,
                    SupplierID = 2
                },
                new Products {
                   PName = "豬肝麵",
                    Price = 60,
                    PPhoto = getFileBytes("\\SourcePhotos\\2-4.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 5,
                    Discontinuted = false,
                    SupplierID = 2
                },
                new Products {
                   PName = "什錦麵",
                    Price = 70,
                    PPhoto = getFileBytes("\\SourcePhotos\\2-5.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 10,
                    Discontinuted = false,
                    SupplierID = 2
                },
                new Products {
                   PName = "鹽味拉麵",
                    Price = 170,
                    PPhoto = getFileBytes("\\SourcePhotos\\3-1.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 5,
                    Discontinuted = false,
                    SupplierID = 3
                },
                new Products {
                   PName = "醬油拉麵",
                    Price = 180,
                    PPhoto = getFileBytes("\\SourcePhotos\\3-2.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 5,
                    Discontinuted = false,
                    SupplierID = 3
                },
                new Products {
                   PName = "味噌拉麵",
                    Price = 180,
                    PPhoto = getFileBytes("\\SourcePhotos\\3-3.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 5,
                    Discontinuted = false,
                    SupplierID = 3
                },
                new Products {
                   PName = "雞腿飯",
                    Price = 75,
                    PPhoto = getFileBytes("\\SourcePhotos\\4-1.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 0,
                    Discontinuted = false,
                    SupplierID = 4
                },
                new Products {
                   PName = "排骨飯",
                    Price = 70,
                    PPhoto = getFileBytes("\\SourcePhotos\\4-2.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 0,
                    Discontinuted = false,
                    SupplierID = 4
                },
                new Products {
                   PName = "爌肉飯",
                    Price = 60,
                    PPhoto = getFileBytes("\\SourcePhotos\\4-3.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 5,
                    Discontinuted = false,
                    SupplierID = 4
                },
                new Products {
                   PName = "宮保雞丁飯",
                    Price = 70,
                    PPhoto = getFileBytes("\\SourcePhotos\\4-4.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 5,
                    Discontinuted = false,
                    SupplierID = 4
                },
                new Products {
                   PName = "瑪格麗特披薩",
                    Price = 170,
                    PPhoto = getFileBytes("\\SourcePhotos\\5-1.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 10,
                    Discontinuted = false,
                    SupplierID = 5
                },
                new Products {
                   PName = "夏威夷披薩",
                    Price = 160,
                    PPhoto = getFileBytes("\\SourcePhotos\\5-2.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 5,
                    Discontinuted = false,
                    SupplierID = 5
                },
                new Products {
                   PName = "海鮮披薩",
                    Price = 200,
                    PPhoto = getFileBytes("\\SourcePhotos\\5-3.jpg"),
                    ImageFormat = "image/jpeg",
                    Discount = 5,
                    Discontinuted = false,
                    SupplierID = 5
                }
            };

            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();


            List<SPhoto> sPhotos = new List<SPhoto>
            {
                new SPhoto
                {
                    Photo = getFileBytes("\\SourcePhotos\\1.jpg"),
                    ImageFormat = "image/jpeg",
                    SupplierID = 1
                },
                new SPhoto {
                    Photo = getFileBytes("\\SourcePhotos\\2.jpg"),
                    ImageFormat = "image/jpeg",
                    SupplierID = 2
                },
                new SPhoto {
                    Photo = getFileBytes("\\SourcePhotos\\3.jpg"),
                    ImageFormat = "image/jpeg",
                    SupplierID = 3
                },
                new SPhoto {
                    Photo = getFileBytes("\\SourcePhotos\\4.jpg"),
                    ImageFormat = "image/jpeg",
                    SupplierID = 4
                },
                new SPhoto {
                    Photo = getFileBytes("\\SourcePhotos\\5.jpg"),
                    ImageFormat = "image/jpeg",
                    SupplierID = 5
                },
                new SPhoto {
                    Photo = getFileBytes("\\SourcePhotos\\6.jpg"),
                    ImageFormat = "image/jpeg",
                    SupplierID = 6
                },
                new SPhoto {
                    Photo = getFileBytes("\\SourcePhotos\\7.jpg"),
                    ImageFormat = "image/jpeg",
                    SupplierID = 7
                },
                new SPhoto {
                    Photo = getFileBytes("\\SourcePhotos\\8.jpg"),
                    ImageFormat = "image/jpeg",
                    SupplierID = 8
                },
                new SPhoto {
                    Photo = getFileBytes("\\SourcePhotos\\9.jpg"),
                    ImageFormat = "image/jpeg",
                    SupplierID = 9
                }
            };

            sPhotos.ForEach(s => context.SPhoto.Add(s));
            context.SaveChanges();


            List<Orders> orders = new List<Orders>
            {
                new Orders {
                    OrderTitle = "老郭燒肉飯",
                    MemberID = 3,
                    OrderDate = DateTime.Now.AddDays(-5).AddHours(-4),
                    RequiredDate = DateTime.Now.AddDays(-5).AddHours(-2),
                    ReadyTime = DateTime.Now.AddDays(-5).AddHours(-2).AddMinutes(-20),
                    ShipAddress = "自取",
                    Note = "無",
                    ShipVia = 1,
                    PayVia = 2,
                    PayState = true,
                    OrderDisplay = true,
                    OrderState = 3
                },
                new Orders {
                    OrderTitle = "老郭燒肉飯",
                    MemberID = 3,
                    OrderDate = DateTime.Now.AddDays(-2).AddHours(-2),
                    RequiredDate = DateTime.Now.AddDays(-2).AddHours(-1),
                    ReadyTime = DateTime.Now.AddDays(-2).AddHours(-1).AddMinutes(-15),
                    ShipAddress = "自取",
                    Note = "無",
                    ShipVia = 1,
                    PayVia = 3,
                    PayState = true,
                    OrderDisplay = true,
                    OrderState = 3
                },
                new Orders {
                    OrderTitle = "老郭燒肉飯",
                    MemberID = 4,
                    OrderDate = DateTime.Now.AddDays(-2).AddHours(-3),
                    RequiredDate = DateTime.Now.AddDays(-2).AddHours(-1),
                    ReadyTime = DateTime.Now.AddDays(-2).AddHours(-1).AddMinutes(-10),
                    ShipAddress = "自取",
                    Note = "無",
                    ShipVia = 1,
                    PayVia = 2,
                    PayState = true,
                    OrderDisplay = true,
                    OrderState = 3
                },
                new Orders
                {
                    OrderTitle = "老郭燒肉飯",
                    MemberID = 1,
                    OrderDate = DateTime.Now.AddHours(-8),
                    RequiredDate = DateTime.Now.AddHours(-6),
                    ShipAddress = "高雄市鼓山區華榮路374號",
                    Note = "無",
                    ShipVia = 2,
                    PayVia = 1,
                    PayState = false,
                    OrderDisplay = true,
                    OrderState = 1
                },
                new Orders {
                    OrderTitle = "老郭燒肉飯",
                    MemberID = 2,
                    OrderDate = DateTime.Now.AddHours(-5),
                    RequiredDate = DateTime.Now.AddHours(-3),
                    ShipAddress = "自取",
                    Note = "無",
                    ShipVia = 1,
                    PayVia = 2,
                    PayState = false,
                    OrderDisplay = true,
                    OrderState = 1
                },
                new Orders {
                    OrderTitle = "品好麵館",
                    MemberID = 3,
                    OrderDate = DateTime.Now.AddDays(-1).AddHours(-7),
                    RequiredDate = DateTime.Now.AddDays(-1).AddHours(-3),
                    ReadyTime = DateTime.Now.AddDays(-1).AddHours(-3).AddMinutes(-8),
                    ShipAddress = "高雄市鼓山區民利街97號",
                    Note = "無",
                    ShipVia = 2,
                    PayVia = 2,
                    PayState = true,
                    OrderDisplay = true,
                    OrderState = 3
                },
                new Orders {
                    OrderTitle = "沐恩拉麵",
                    MemberID = 2,
                    OrderDate = DateTime.Now.AddHours(-5),
                    RequiredDate = DateTime.Now.AddHours(-3),
                    ShipAddress = "自取",
                    Note = "無",
                    ShipVia = 1,
                    PayVia = 2,
                    PayState = false,
                    OrderDisplay = true,
                    OrderState = 1
                }
            };

            orders.ForEach(s => context.Orders.Add(s));
            context.SaveChanges();


            List<OrderDetails> orderDetails = new List<OrderDetails>
            {
                new OrderDetails {
                    OrderID = 1,
                    ProductID = 2,
                    Price = 40,
                    Discount = 0,
                    Quantity = 15
                },
                new OrderDetails {
                    OrderID = 1,
                    ProductID = 3,
                    Price = 60,
                    Discount = 10,
                    Quantity = 6
                },
                new OrderDetails {
                    OrderID = 2,
                    ProductID = 1,
                    Price = 60,
                    Discount = 5,
                    Quantity = 10
                },
                new OrderDetails {
                    OrderID = 2,
                    ProductID = 5,
                    Price = 10,
                    Discount = 0,
                    Quantity = 8
                },
                new OrderDetails {
                    OrderID = 3,
                    ProductID = 1,
                    Price = 60,
                    Discount = 5,
                    Quantity = 12
                },
                new OrderDetails {
                    OrderID = 3,
                    ProductID = 3,
                    Price = 60,
                    Discount = 10,
                    Quantity = 9
                },
                new OrderDetails {
                    OrderID = 3,
                    ProductID = 4,
                    Price = 40,
                    Discount = 5,
                    Quantity = 6
                },
                new OrderDetails {
                    OrderID = 4,
                    ProductID = 3,
                    Price = 60,
                    Discount = 10,
                    Quantity = 6
                },
                new OrderDetails {
                    OrderID = 5,
                    ProductID = 1,
                    Price = 60,
                    Discount = 5,
                    Quantity = 3
                },
                new OrderDetails {
                    OrderID = 5,
                    ProductID = 5,
                    Price = 10,
                    Discount = 0,
                    Quantity = 3
                },
                new OrderDetails {
                    OrderID = 6,
                    ProductID = 6,
                    Price = 45,
                    Discount = 0,
                    Quantity = 5
                },
                new OrderDetails {
                    OrderID = 6,
                    ProductID = 9,
                    Price = 60,
                    Discount = 5,
                    Quantity = 3
                },
                new OrderDetails {
                    OrderID = 6,
                    ProductID = 10,
                    Price = 70,
                    Discount = 10,
                    Quantity = 2
                },
                new OrderDetails {
                    OrderID = 7,
                    ProductID = 11,
                    Price = 170,
                    Discount = 5,
                    Quantity = 2
                },
                new OrderDetails {
                    OrderID = 7,
                    ProductID = 12,
                    Price = 180,
                    Discount = 5,
                    Quantity = 3
                }
            };

            orderDetails.ForEach(s => context.OrderDetails.Add(s));
            context.SaveChanges();


            List<SupMessage> supMessages = new List<SupMessage>
            {
                new SupMessage {
                    SupplierID = 2,
                    SMessage = "凡到店自取外帶，即可以 7 折價訂購防疫套餐。\r\n感恩回饋，加麵不加價。\r\n恕不提供醬料及餐具。",
                    StartDate = DateTime.Now.AddDays(-14).AddHours(-3),
                    EndDate = DateTime.Now.AddDays(7).AddHours(-3)
                },
                new SupMessage {
                    SupplierID = 3,
                    SMessage = "凡到店自取外帶，即可以 65 折價訂購防疫套餐。\r\n感恩回饋，加麵不加價。",
                    StartDate = DateTime.Now.AddDays(-21).AddHours(-3),
                    EndDate = DateTime.Now.AddDays(10).AddHours(-3)
                }
            };

            supMessages.ForEach(s => context.SupMessage.Add(s));
            context.SaveChanges();
        }

        byte[] getFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);

            byte[] fileBytes;

            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }


            return fileBytes;
        }
	}
}