using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    /* This is what kinds of goods or products the Vendor supplies */ //Incomplete
    public class VendorProductType
    {
        public int ID { get; set; }
        [Required]
        public int VendorID { get; set; }
        [Required]
        public int ProductTypeID { get; set; }
        public string Description { get; set; }
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
        [ForeignKey("VendorID")]
        public Vendor Vendor { get; set; }
        [ForeignKey("ProductTypeID")]
        public ProductType ProductType { get; set; }
        public List<PurchaseOrder> PurchaseOrders { get; set; }
        public List<Receiving> Received { get; set; }
        public List<VendorProductTypePrice> VendorProductTypePrices { get; set; }

        public VendorProductType()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}