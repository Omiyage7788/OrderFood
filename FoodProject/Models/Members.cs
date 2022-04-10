using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Models
{
    public class Members
    {
        [Key]
        [DisplayName("會員編號")]
        public int MemberID { get; set; }

        [DisplayName("會員名稱")]
        [Required(ErrorMessage = "會員名稱為必填")]
        [StringLength(30, ErrorMessage = "會員名稱最多30字")]
        public string MName { get; set; }

        [DisplayName("手機")]
        [Required(ErrorMessage = "手機為必填")]
        [RegularExpression("[0]{1}[0-9]{9}", ErrorMessage = "請填有效手機號碼")]
        public string MCellphone { get; set; }

        [DisplayName("電子信箱")]
        [Required(ErrorMessage = "電子信箱為必填")]
        [StringLength(30, ErrorMessage = "電子信箱最多30字元")]
        public string MEmail { get; set; }

        [DisplayName("會員權限")]
        public bool MAuthority { get; set; }

        public virtual MAccPwd MAccPwd { get; set; }
        public virtual ICollection<AdminMemNotification> AdminMemNotification { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<SupMemNotification> SupMemNotification { get; set; }
    }
}