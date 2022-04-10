using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodProject.Models
{
	public class MAccPwd
	{
        [Key, ForeignKey("Members")]
        [DisplayName("會員編號")]
        public int MemberID { get; set; }

        [DisplayName("會員帳號")]
        [Required(ErrorMessage = "帳號為必填")]
        [RegularExpression("[0-9A-Za-z]{3,20}", ErrorMessage = "請填3-20碼英數字組合")]
        public string MAccount { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "密碼為必填")]
        [RegularExpression("[0-9A-Za-z]{3,20}", ErrorMessage = "請填3-20碼英數字組合")]
        public string MPassword { get; set; }


        public virtual Members Members { get; set; }
    }
}