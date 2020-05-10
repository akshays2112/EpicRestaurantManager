using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class Order
    {
        public int ID { get; set; }
        [Required]
        public string CustomerDescription { get; set; }
        public bool Settled { get; set; }
        public int SettlementTypeID { get; set; }
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
        [ForeignKey("SiteID")]
        public Site Site { get; set; }
        [ForeignKey("EntryByUserID")]
        public User User { get; set; }
        [ForeignKey("SettlementTypeID")]
        public SettlementType SettlementType { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}