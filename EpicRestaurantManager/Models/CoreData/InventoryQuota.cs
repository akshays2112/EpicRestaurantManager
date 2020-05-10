using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class InventoryQuota
    {
        public int ID { get; set; }
        [Required]
        public int ProductTypeID { get; set; }
        public float MinimumStock { get; set; }
        public float MaximumStock { get; set; }
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

        public InventoryQuota()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}