using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class OrderItem
    {
        public int ID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int MenuItemID { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string CustomOrderDescription { get; set; }
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
        [ForeignKey("MenuItemID")]
        public MenuItem MenuItem { get; set; }
        [ForeignKey("OrderID")]
        public Order Order { get; set; }
        public List<Chef> Chefs { get; set; }

        public OrderItem()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}