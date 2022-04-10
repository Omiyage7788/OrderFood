using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodProject.Models
{
	public class AdminAccPwd
	{
        [Key, ForeignKey("Administrators")]
        [DisplayName("管理員編號")]
        public int AdminID { get; set; }

        [DisplayName("管理員帳號")]
        [Required(ErrorMessage = "管理員帳號為必填")]
        [RegularExpression("[0-9A-Za-z]{3,20}", ErrorMessage = "請填3-20碼英數字組合")]
        public string AdminAccount { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "密碼為必填")]
        [RegularExpression("[0-9A-Za-z]{3,20}", ErrorMessage = "請填3-20碼英數字組合")]
        public string AdminPassword { get; set; }


        public virtual Administrators Administrators { get; set; }
    }
}