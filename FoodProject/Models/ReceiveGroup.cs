using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Models
{
	public class ReceiveGroup
	{
        [Key]
        [DisplayName("收件人代號")]
        public int RecipientID { get; set; }

        [DisplayName("收件人名稱")]
        [Required(ErrorMessage = "收件人名稱為必填")]
        [StringLength(10, ErrorMessage = "收件人名稱最多10字")]
        public string Recipient { get; set; }


        public virtual ICollection<AdminMessage> AdminMessages { get; set; }
    }
}