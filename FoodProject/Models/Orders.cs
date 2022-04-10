using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodProject.Models
{
	public class Orders
	{
        [Key]
        [DisplayName("訂單編號")]
        public int OrderID { get; set; }

        [DisplayName("訂單名稱")]
        [StringLength(30, ErrorMessage = "訂單名稱最多30字")]
        public string OrderTitle { get; set; }

        [DisplayName("會員編號")]
        [Required(ErrorMessage = "會員編號為必填")]
        public int MemberID { get; set; }

        [DisplayName("訂單日期")]
        [Required(ErrorMessage = "訂單日期為必填")]
        public DateTime OrderDate { get; set; }

        [DisplayName("取餐日期時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "取餐日期時間為必填")]
        public DateTime RequiredDate { get; set; }

        [DisplayName("可取餐時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ReadyTime { get; set; }

        [DisplayName("送達地址")]
        [Required(ErrorMessage = "送達地址為必填")]
        [StringLength(50, ErrorMessage = "送達地址最多50字")]
        public string ShipAddress { get; set; }

        [DisplayName("備註")]
        public string Note { get; set; }

        [ForeignKey("ShipMethod")]
        [DisplayName("取餐方式")]
        //[Required(ErrorMessage = "取餐方式為必填")]
        public int ShipVia { get; set; }

        [ForeignKey("PayMethod")]
        [DisplayName("付款方式")]
        //[Required(ErrorMessage = "付款方式為必填")]
        public int PayVia { get; set; }

        [DisplayName("團購連結")]
        public string OrderUrl { get; set; }

        [DisplayName("評價")]
        public Nullable<short> Rate { get; set; }

        [DisplayName("是否付款")]
        public Boolean PayState { get; set; }

        [DisplayName("顯示訂單")]
        public Boolean OrderDisplay { get; set; }

        [ForeignKey("OrderStatus")]
        [DisplayName("訂單狀態")]
        public int OrderState { get; set; }

        [DisplayName("店家取消訂單說明")]
        public string SupplierDecline { get; set; }


        public virtual Members Members { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual PayMethod PayMethod { get; set; }
        public virtual ShipMethod ShipMethod { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
    }
}