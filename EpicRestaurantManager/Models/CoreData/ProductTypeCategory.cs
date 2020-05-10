using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    /*Could be as simple as Veg, Non Veg or more detailed like Chicken.  
    Boneless chicken would be a product type sold by a vendor in the 
    product type category of chicken for example.*/
    public class ProductTypeCategory
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
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
        public List<ProductType> ProductTypes { get; set; }

        public ProductTypeCategory()
        {
            this.TransactionDateTime = DateTime.Now;
        }
    }
}

