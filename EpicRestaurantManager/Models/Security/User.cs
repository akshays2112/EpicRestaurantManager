using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EpicRestaurantManager.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public bool EmailIsVerified { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberIsVerified { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Photo { get; set; }
        [Required]
        public string JobTitle { get; set; }
        public bool IsSiteAdmin { get; set; }
        public bool IsRootUser { get; set; }
        [Required]
        public string Password { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedOn { get; set; }
        [NotMapped]
        public int UILoginUserID { get; set; }
        [NotMapped]
        public string UILoginPassword { get; set; }
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
        public List<UserInGroup> UserInGroups { get; set; }
        public List<UserSite> UserSites { get; set; }
        public List<Inventory> Invetories { get; set; }
        public List<Requisition> Requistions { get; set; }
        public List<MenuCategory> MenuCategories { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public List<MenuItemPrice> MenuItemPrices { get; set; }
        public List<MenuItemIngredient> MenuItemIngredients { get; set; }
        public List<InventoryQuota> InventoryQuotas { get; set; }
        public List<InventoryLocation> InventoryLocations { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<SettlementType> SettlementTypes { get; set; }

        public User()
        {
            this.CreatedOn = DateTime.Now;
        }
    }
}
