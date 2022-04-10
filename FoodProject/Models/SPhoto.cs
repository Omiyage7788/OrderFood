using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FoodProject.Models
{
	public class SPhoto
	{
        [Key]
        [DisplayName("照片編號")]
        public int PhotoID { get; set; }

        [DisplayName("店家照片")]
        public byte[] Photo { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageFormat { get; set; }

        [DisplayName("店家編號")]
        [Required(ErrorMessage = "店家編號為必填")]
        public int SupplierID { get; set; }

        public virtual Suppliers Suppliers { get; set; }
    }
}