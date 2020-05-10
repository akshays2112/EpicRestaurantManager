using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class ProductType
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int ProductTypeCategoryID { get; set; }
        [Required]
        public int ProductMeasurementTypeID { get; set; }
        [Required]
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
        [ForeignKey("EntryByUserID")]
        public User User { get; set; }
        [ForeignKey("SiteID")]
        public Site Site { get; set; }
        [ForeignKey("ProductTypeCategoryID")]
        public ProductTypeCategory ProductTypeCategory { get; set; }
        [ForeignKey("ProductMeasurementTypeID")]
        public ProductMeasurementType ProductMeasurementType { get; set; }
        public List<Inventory> Inventories { get; set; }
        public List<VendorProductType> VendorProductTypes { get; set; }
        public List<Requisition> Requisitions { get; set; }
        public List<MenuItemIngredient> MenuItemIngredients { get; set; }

        public ProductType()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}
