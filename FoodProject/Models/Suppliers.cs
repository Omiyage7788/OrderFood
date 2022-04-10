using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodProject.Models
{
	public class Suppliers
	{
        [Key]
        [DisplayName("店家編號")]
        public int SupplierID { get; set; }

        [DisplayName("店家名稱")]
        [Required(ErrorMessage = "店家名稱為必填")]
        [StringLength(30, ErrorMessage = "店家名稱最多30字")]
        public string SupplierName { get; set; }

        [DisplayName("手機")]
        [Required(ErrorMessage = "手機為必填")]
        [RegularExpression("[0]{1}[0-9]{9}", ErrorMessage = "請填有效手機號碼")]
        public string SCellphone { get; set; }

        [DisplayName("市話")]
        [RegularExpression("[0]{1}[0-9]{8,9}", ErrorMessage = "請填有效市話號碼(含區碼)")]
        public string SPhone { get; set; }

        [DisplayName("電子信箱")]
        [Required(ErrorMessage = "電子信箱為必填")]
        [StringLength(30, ErrorMessage = "電子信箱最多30字元")]
        public string SEmail { get; set; }

        [DisplayName("縣/市")]
        [Required(ErrorMessage = "縣/市為必填")]
        [StringLength(5, ErrorMessage = "最多5字")]
        public string SCity { get; set; }

        [DisplayName("區/鄉/鎮")]
        [Required(ErrorMessage = "此欄位為必填")]
        [StringLength(5, ErrorMessage = "最多5字")]
        public string SDistrict { get; set; }

        [DisplayName("地址")]
        [Required(ErrorMessage = "此欄位為必填")]
        [StringLength(30, ErrorMessage = "最多30字")]
        public string SRoad { get; set; }

        [DisplayName("營業時間")]
        public string BusinessHour { get; set; }

        [DisplayName("店家權限")]
        [Required(ErrorMessage = "店家權限為必填")]
        public bool SAuthority { get; set; }


        public virtual SAccPwd SAccPwd { get; set; }
        public virtual ICollection<AdminSupNotification> AdminSupNotification { get; set; }
        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<SCategory> SCategory { get; set; }
        public virtual ICollection<SPhoto> SPhoto { get; set; }
        public virtual ICollection<SupMessage> SupMessages { get; set; }
    }
}