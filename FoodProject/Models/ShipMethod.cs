using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Models
{
    public class ShipMethod
    {
        [Key]
        [DisplayName("取餐方式代號")]
        public int ShipID { get; set; }

        [DisplayName("取餐方式")]
        [Required(ErrorMessage = "取餐方式為必填")]
        [StringLength(10, ErrorMessage = "取餐方式最多10字")]
        public string Method { get; set; }


        public virtual ICollection<Orders> Orders { get; set; }
    }
}