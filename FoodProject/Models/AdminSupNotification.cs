using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodProject.Models
{
	public class AdminSupNotification
	{
        [Key, Column(Order = 0), ForeignKey("Suppliers")]
        [DisplayName("店家編號")]
        [Required(ErrorMessage = "店家編號為必填")]
        public int SupplierID { get; set; }

        [Key, Column(Order = 1), ForeignKey("AdminMessage")]
        [DisplayName("推播編號")]
        [Required(ErrorMessage = "推播編號為必填")]
        public int MessageID { get; set; }

        [DisplayName("顯示推播")]
        [Required(ErrorMessage = "此欄為必填")]
        public bool AdminSupDisplay { get; set; }

        [DisplayName("已讀")]
        [Required(ErrorMessage = "此欄為必填")]
        public bool Read { get; set; }

        public virtual AdminMessage AdminMessage { get; set; }
        public virtual Suppliers Suppliers { get; set; }
    }
}