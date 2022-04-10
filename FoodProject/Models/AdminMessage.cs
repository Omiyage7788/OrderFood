using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodProject.Models
{
	public class AdminMessage
	{
        [Key]
        [DisplayName("推播編號")]
        public int MessageID { get; set; }

        [ForeignKey("Administrators")]
        [DisplayName("管理員編號")]
        [Required(ErrorMessage = "管理員編號為必填")]
        public int AdminID { get; set; }

        [DisplayName("推播訊息")]
        [Required(ErrorMessage = "推播訊息為必填")]
        public string AMessage { get; set; }

        [ForeignKey("ReceiveGroup")]
        [DisplayName("推播對象")]
        [Required(ErrorMessage = "推播對象為必填")]
        public int MessageRecipient { get; set; }

        [DisplayName("發布時間")]
        [Required(ErrorMessage = "發布時間為必填")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayName("結束時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }


        public virtual Administrators Administrators { get; set; }
        public virtual ReceiveGroup ReceiveGroup { get; set; }
        public virtual ICollection<AdminMemNotification> AdminMemNotifications { get; set; }
        public virtual ICollection<AdminSupNotification> AdminSupNotifications { get; set; }
    }
}