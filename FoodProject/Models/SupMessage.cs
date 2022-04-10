using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Models
{
	public class SupMessage
	{
        [Key]
        [DisplayName("推播編號")]
        public int MessageID { get; set; }

        [DisplayName("店家編號")]
        [Required(ErrorMessage = "店家編號為必填")]
        public int SupplierID { get; set; }

        [DisplayName("推播訊息")]
        [Required(ErrorMessage = "推播訊息為必填")]
        public string SMessage { get; set; }

        [DisplayName("發布時間")]
        [Required(ErrorMessage = "發布時間為必填")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayName("結束時間")]
        [Required(ErrorMessage = "結束時間為必填")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }


        public virtual Suppliers Suppliers { get; set; }
        public virtual ICollection<SupMemNotification> SupMemNotification { get; set; }
    }
}