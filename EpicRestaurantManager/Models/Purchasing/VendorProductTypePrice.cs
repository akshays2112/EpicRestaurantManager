using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class VendorProductTypePrice
    {
        public int ID { get; set; }
        [Required]
        public int VendorProductTypeID { get; set; }
        [Required]
        public float PricePerMeasurementType { get; set; }
        [Required]
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        [Required]
        public int EntryByUserID { get; set; }
        [Required]
        public int SiteID { get; set; }
        [Required]
        public DateTime TransactionDateTime { get; set; }
        [NotMapped]
        public int UILoginUserID { get; set; }
        [NotMapped]
        public string UILoginPassword { get; set; }
        [ForeignKey("EntryByUserID")]
        public User User { get; set; }
        [ForeignKey("SiteID")]
        public Site Site { get; set; }
        [ForeignKey("VendorProductTypeID")]
        public VendorProductType VendorProductType { get; set; }

        public VendorProductTypePrice()
        {
            this.ContractStartDate = DateTime.Now;
            this.ContractEndDate = DateTime.Now;
            this.TransactionDateTime = DateTime.Now;
        }
    }
}