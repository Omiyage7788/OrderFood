using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Models
{
	public class Categories
	{
        [Key]
        [DisplayName("分類編號")]
        public int CategoryID { get; set; }

        [DisplayName("分類名稱")]
        [Required(ErrorMessage = "分類名稱為必填")]
        [StringLength(10, ErrorMessage = "分類名稱最多10字")]
        public string CategoryName { get; set; }

        public bool IsSelected { get; set; }

        public virtual ICollection<SCategory> SCategory { get; set; }
    }
}