using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Models
{
	public class OrderStatus
	{
        [Key]
        [DisplayName("訂單狀態代號")]
        public int StatusID { get; set; }

        [DisplayName("訂單狀態")]
        [Required(ErrorMessage = "訂單狀態為必填")]
        [StringLength(10, ErrorMessage = "訂單狀態最多10字")]
        public string Status { get; set; }


        public virtual ICollection<Orders> Orders { get; set; }
    }
}