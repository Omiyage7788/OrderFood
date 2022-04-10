using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodProject.Models
{
	public class SCategory
	{
        [Key, Column(Order = 0), ForeignKey("Categories")]
        [DisplayName("分類編號")]
        public int CategoryID { get; set; }

        [Key, Column(Order = 1), ForeignKey("Suppliers")]
        [DisplayName("店家編號")]
        public int SupplierID { get; set; }

        [DisplayName("備註")]
        [StringLength(30, ErrorMessage = "備註最多30字")]
        public string Note { get; set; }

        public virtual Categories Categories { get; set; }
        public virtual Suppliers Suppliers { get; set; }
    }
}