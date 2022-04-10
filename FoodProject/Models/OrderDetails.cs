using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodProject.Models
{
	public class OrderDetails
	{
        [Key, Column(Order = 0), ForeignKey("Orders")]
        [DisplayName("訂單編號")]
        [Required(ErrorMessage = "訂單編號為必填")]
        public int OrderID { get; set; }

        [Key, Column(Order = 1), ForeignKey("Products")]
        [DisplayName("商品編號")]
        [Required(ErrorMessage = "商品編號為必填")]
        public int ProductID { get; set; }

        [DisplayName("單價")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "單價為必填")]
        public decimal Price { get; set; }

        [DisplayName("折扣")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "折扣為必填")]
        public decimal Discount { get; set; }

        [DisplayName("數量")]
        [Required(ErrorMessage = "數量為必填")]
        public int Quantity { get; set; }

        [DisplayName("小計")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> Total { get; set; }


        public virtual Orders Orders { get; set; }
        public virtual Products Products { get; set; }
    }
}