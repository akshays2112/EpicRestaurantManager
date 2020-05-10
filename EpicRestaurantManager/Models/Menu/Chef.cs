using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class Chef
    {
        public int ID { get; set; }
        [Required]
        public int OrderItemID { get; set; }
        public bool BeingPrepared { get; set; }
        public DateTime StartedPreparing { get; set; }
        public bool ReadyForPickup { get; set; }
        public DateTime FinishedPreparing { get; set; }
        public bool Delivered { get; set; }
        public DateTime DeliveredToGuests { get; set; }
        [Required]
        public int EntryByUserID { get; set; }
        [Required]
        public int SiteID { get; set; }
        public DateTime TransactionDateTime { get; set; }
        [NotMapped]
        public int UILoginUserID { get; set; }
        [NotMapped]
        public string UILoginPassword { get; set; }
        [ForeignKey("SiteID")]
        public Site Site { get; set; }
        [ForeignKey("EntryByUserID")]
        public User User { get; set; }
        [ForeignKey("OrderItemID")]
        public OrderItem OrderItem { get; set; }
    }
}