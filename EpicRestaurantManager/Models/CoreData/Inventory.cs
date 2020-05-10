using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class Inventory
    {
        public int ID { get; set; }
        [Required]
        public int ProductTypeID { get; set; }
        [Required]
        public float Quantity { get; set; }
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
        [ForeignKey("ProductTypeID")]
        public ProductType ProductType { get; set; }
        [ForeignKey("SiteID")]
        public Site Site { get; set; }
        [ForeignKey("EntryByUserID")]
        public User User { get; set; }
        public List<InventoryQuota> InventoryQuotas { get; set; }
        public List<InventoryLocation> InventoryLocations { get; set; }

        public Inventory()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}