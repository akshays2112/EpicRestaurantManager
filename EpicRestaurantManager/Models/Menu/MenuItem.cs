using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class MenuItem
    {
        public int ID { get; set; }
        [Required]
        public int MenuCategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
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
        [ForeignKey("MenuCategoryID")]
        public MenuCategory MenuCategory { get; set; }
        [ForeignKey("SiteID")]
        public Site Site { get; set; }
        [ForeignKey("EntryByUserID")]
        public User User { get; set; }
        public List<MenuItemIngredient> MenuItemIngredients { get; set; }
        public List<MenuItemPrice> MenuItemPrices { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public MenuItem()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}