using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EpicRestaurantManager.Models
{
    public class EpicRestaurantManagerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public EpicRestaurantManagerContext() : base("name=EpicRestaurantManagerContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SettlementType>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<SettlementType>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<OrderItem>().HasRequired(c => c.MenuItem).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<OrderItem>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<OrderItem>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Order>().HasRequired(c => c.SettlementType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Order>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Order>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<InventoryLocation>().HasRequired(c => c.ProductType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<InventoryLocation>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<InventoryLocation>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<InventoryQuota>().HasRequired(c => c.ProductType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<InventoryQuota>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<InventoryQuota>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Requisition>().HasRequired(c => c.ProductType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Requisition>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Requisition>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MenuCategory>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MenuCategory>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MenuItem>().HasRequired(c => c.MenuCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MenuItem>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MenuItem>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MenuItemIngredient>().HasRequired(c => c.MenuItem).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MenuItemIngredient>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MenuItemIngredient>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MenuItemPrice>().HasRequired(c => c.MenuItem).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MenuItemPrice>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<MenuItemPrice>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Receiving>().HasRequired(c => c.VendorProductType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Receiving>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Inventory>().HasRequired(c => c.ProductType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Inventory>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Inventory>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductMeasurementType>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductMeasurementType>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductType>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductType>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductType>().HasRequired(c => c.ProductTypeCategory).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductType>().HasRequired(c => c.ProductMeasurementType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductTypeCategory>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ProductTypeCategory>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PurchaseOrder>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PurchaseOrder>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<PurchaseOrder>().HasRequired(c => c.VendorProductType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Receiving>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Receiving>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Receiving>().HasRequired(c => c.VendorProductType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Vendor>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Vendor>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<VendorContactDetail>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<VendorContactDetail>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<VendorContactDetail>().HasRequired(c => c.Vendor).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<VendorProductType>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<VendorProductType>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<VendorProductType>().HasRequired(c => c.Vendor).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<VendorProductType>().HasRequired(c => c.ProductType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<VendorProductTypePrice>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<VendorProductTypePrice>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<VendorProductTypePrice>().HasRequired(c => c.VendorProductType).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserControllerActionPermission>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserControllerActionPermission>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserControllerActionPermission>().HasRequired(c => c.UserForPermission).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserControllerActionPermission>().HasRequired(c => c.ControllerAction).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserGroup>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserGroup>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserGroupControllerActionPermission>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserGroupControllerActionPermission>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserGroupControllerActionPermission>().HasRequired(c => c.UserGroup).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserGroupControllerActionPermission>().HasRequired(c => c.ControllerAction).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserInGroup>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserInGroup>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserInGroup>().HasRequired(c => c.UserGroup).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserInGroup>().HasRequired(c => c.UserPermission).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserSite>().HasRequired(c => c.Site).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<UserSite>().HasRequired(c => c.User).WithMany().WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.ProductMeasurementType> ProductMeasurementTypes { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.ProductType> ProductTypes { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.ProductTypeCategory> ProductTypeCategories { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.Vendor> Vendors { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.VendorContactDetail> VendorContactDetails { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.VendorProductType> VendorProductTypes { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.Inventory> Inventories { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.VendorProductTypePrice> VendorProductTypePrices { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.Receiving> Received { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.PurchaseOrder> PurchaseOrders { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.ControllerAction> ControllerActions { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.UserControllerActionPermission> UserControllerActionPermissions { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.UserGroup> UserGroups { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.UserGroupControllerActionPermission> UserGroupControllerActionPermissions { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.UserInGroup> UserInGroups { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.Site> Sites { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.UserSite> UserSites { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.InventoryLocation> InventoryLocations { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.InventoryQuota> InventoryQuotas { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.MenuCategory> MenuCategories { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.MenuItem> MenuItems { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.MenuItemIngredient> MenuItemIngredients { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.SettlementType> SettlementTypes { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.Requisition> Requisitions { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.OrderItem> OrderItems { get; set; }

        public System.Data.Entity.DbSet<EpicRestaurantManager.Models.MenuItemPrice> MenuItemPrices { get; set; }
    }
}
