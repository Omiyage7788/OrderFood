using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodProject.Models
{
    public class OrderMealContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public OrderMealContext() : base("name=OrderMeal")
        {
        }
        public DbSet<AdminAccPwd> AdminAccPwd { get; set; }
        public DbSet<Administrators> Administrators { get; set; }
        public DbSet<AdminMessage> AdminMessages { get; set; }
        public DbSet<AdminMemNotification> AdminMemNotification { get; set; }
        public DbSet<AdminSupNotification> AdminSupNotification { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<MAccPwd> MAccPwds { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<PayMethod> PayMethod { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ReceiveGroup> ReceiveGroup { get; set; }
        public DbSet<SAccPwd> SAccPwd { get; set; }
        public DbSet<SCategory> SCategory { get; set; }
        public DbSet<ShipMethod> ShipMethod { get; set; }
        public DbSet<SPhoto> SPhoto { get; set; }
        public DbSet<SupMemNotification> SupMemNotification { get; set; }
        public DbSet<SupMessage> SupMessage { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
	}
}
