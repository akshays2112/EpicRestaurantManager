using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class Site
    {
        public int ID { get; set; }
        [Required]
        public string NameOfBusiness { get; set; }
        public string HomeHeaderImage { get; set; }
        public float ServiceChargePercentage { get; set; }
        public float VATPercentage { get; set; }
        public float ServiceTaxPercentage { get; set; }
        [NotMapped]
        public int UILoginUserID { get; set; }
        [NotMapped]
        public string UILoginPassword { get; set; }
        public List<Inventory> Inventories { get; set; }
        public List<ProductMeasurementType> ProductMeasurementTypes { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public List<ProductTypeCategory> ProductTypeCategories { get; set; }
        public List<PurchaseOrder> PurchaseOrders { get; set; }
        public List<Receiving> Received { get; set; }
        public List<Vendor> Vendors { get; set; }
        public List<VendorContactDetail> VendorContactDetails { get; set; }
        public List<VendorProductType> VendorProductTypes { get; set; }
        public List<VendorProductTypePrice> VendorProductTypePrices { get; set; }
        public List<UserControllerActionPermission> UserControllerActionPermissions { get; set; }
        public List<UserGroup> UserGroups { get; set; }
        public List<UserGroupControllerActionPermission> UserGroupControllerActionPermissions { get; set; }
        public List<UserInGroup> UserInGrouops { get; set; }
        public List<UserSite> UserSites { get; set; }
        public List<Requisition> Requistions { get; set; }
        public List<MenuCategory> MenuCategories { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public List<MenuItemPrice> MenuItemPrices { get; set; }
        public List<MenuItemIngredient> MenuItemIngredients { get; set; }
        public List<InventoryQuota> InventoryQuotas { get; set; }
        public List<InventoryLocation> InventoryLocations { get; set; }
        public List<Order> Orders { get; set; }
    }
}