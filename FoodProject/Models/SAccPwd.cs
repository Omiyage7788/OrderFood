using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodProject.Models
{
	public class SAccPwd
	{
        [Key, ForeignKey("Suppliers")]
        [DisplayName("店家編號")]
        public int SupplierID { get; set; }

        [DisplayName("店家帳號")]
        [Required(ErrorMessage = "店家帳號為必填")]
        [RegularExpression("[0-9A-Za-z]{3,20}", ErrorMessage = "請填3-20碼英數字組合")]
        public string SAccount { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "密碼為必填")]
        [RegularExpression("[0-9A-Za-z]{3,20}", ErrorMessage = "請填3-20碼英數字組合")]
        public string SPassword { get; set; }


        public virtual Suppliers Suppliers { get; set; }
    }
}