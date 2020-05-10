using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class Receiving
    {
        public int ID { get; set; }
        [Required]
        public int VendorProductTypeID { get; set; }
        [Required]
        public float Quantity { get; set; }
        [Required]
        public float PricePaid { get; set; }
        [Required]
        public DateTime ReceivedDate { get; set; }
        public string Comments { get; set; }
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

        public Receiving()
        {
            this.ReceivedDate = DateTime.Now;
            this.TransactionDateTime = DateTime.Now;
        }
    }
}