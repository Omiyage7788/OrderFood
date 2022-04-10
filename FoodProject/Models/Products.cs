using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FoodProject.Models
{
	public class Products
	{
        [Key]
        [DisplayName("商品編號")]
        public int ProductID { get; set; }

        [DisplayName("商品名稱")]
        [Required(ErrorMessage = "商品名稱為必填")]
        [StringLength(30, ErrorMessage = "商品名稱最多30字")]
        public string PName { get; set; }

        [DisplayName("單價")]
        [Required(ErrorMessage = "單價為必填")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [DisplayName("商品照片")]
        public byte[] PPhoto { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageFormat { get; set; }

        [DisplayName("折扣")]
        [Required(ErrorMessage = "折扣為必填")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
        public decimal Discount { get; set; }

        [DisplayName("下架")]
        [Required(ErrorMessage = "此欄為必填")]
        public bool Discontinuted { get; set; }

        [DisplayName("店家編號")]
        [Required(ErrorMessage = "店家編號為必填")]
        public int SupplierID { get; set; }


        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual Suppliers Suppliers { get; set; }
    }
}