using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodProject.Models
{
	public class AdminMemNotification
	{
        [Key, Column(Order = 0), ForeignKey("Members")]
        [DisplayName("會員編號")]
        [Required(ErrorMessage = "會員編號為必填")]
        public int MemberID { get; set; }

        [Key, Column(Order = 1), ForeignKey("AdminMessage")]
        [DisplayName("推播編號")]
        [Required(ErrorMessage = "推播編號為必填")]
        public int MessageID { get; set; }

        [DisplayName("顯示推播")]
        [Required(ErrorMessage = "此欄為必填")]
        public bool AdminMemDisplay { get; set; }

        [DisplayName("已讀")]
        [Required(ErrorMessage = "此欄為必填")]
        public bool Read { get; set; }

        public virtual AdminMessage AdminMessage { get; set; }
        public virtual Members Members { get; set; }
    }
}