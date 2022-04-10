using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Models
{
	public class Administrators
	{
		[Key]
        [DisplayName("管理員編號")]
        public int AdminID { get; set; }

        [DisplayName("管理員名稱")]
        [Required(ErrorMessage = "名稱為必填")]
        [StringLength(30, ErrorMessage = "名稱最多30字")]
        public string AdminName { get; set; }

        [DisplayName("最高權限")]
        //[Required(ErrorMessage = "管理員權限為必填")]
        public bool AdminAuthority { get; set; }


        public virtual AdminAccPwd AdminAccPwd { get; set; }
        public virtual ICollection<AdminMessage> AdminMessages { get; set; }
    }
}