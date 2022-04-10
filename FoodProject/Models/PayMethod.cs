using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Models
{
	public class PayMethod
	{
        [Key]
        [DisplayName("付款方式代號")]
        public int PayID { get; set; }

        [DisplayName("付款方式")]
        [Required(ErrorMessage = "付款方式為必填")]
        [StringLength(10, ErrorMessage = "付款方式最多10字")]
        public string Method { get; set; }


        public virtual ICollection<Orders> Orders { get; set; }
    }
}