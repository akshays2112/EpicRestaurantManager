namespace EpicRestaurantManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EpicRestaurantManager.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EpicRestaurantManager.Models.EpicRestaurantManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EpicRestaurantManagerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Sites.SqlQuery("set identity_insert sites on");
            context.Sites.AddOrUpdate(u => new
            {
                u.ID,
                u.NameOfBusiness,
                u.HomeHeaderImage
            }, new Site
            {
                ID = 1,
                NameOfBusiness = "Epic Restaurant Manager",
                HomeHeaderImage = ""
            }, new Site
            {
                ID = 2,
                NameOfBusiness = "Spice Nation (Vinstar)",
                HomeHeaderImage = ""
            });

            context.Sites.SqlQuery("set identity_insert users on");
            context.Users.AddOrUpdate(u => new
            {
                u.ID,
                u.Address,
                u.City,
                u.Country,
                u.EmailAddress,
                u.EmailIsVerified,
                u.IsRootUser,
                u.IsSiteAdmin,
                u.JobTitle,
                u.Name,
                u.Password,
                u.PhoneNumber,
                u.PhoneNumberIsVerified,
                u.State,
                u.UserName,
                u.ZipCode,
                u.Photo,
                u.CreatedByUserID,
                u.CreatedOn
            }, new User
            {
                ID = 1,
                Address = "1103, Tower 7, Blue Ridge Township, Phase 1 Hinjewadi",
                City = "Pune",
                Country = "India",
                EmailAddress = "akshay.srin@gmail.com",
                EmailIsVerified = true,
                IsRootUser = true,
                IsSiteAdmin = false,
                JobTitle = "Superuser",
                Name = "Akshay Srinivasan",
                Password = "xyz123",
                PhoneNumber = "+919884967112",
                PhoneNumberIsVerified = true,
                State = "Maharashtra",
                UserName = "root",
                ZipCode = "600031",
                Photo = "",
                CreatedByUserID = 1,
                CreatedOn = DateTime.Now
            }, new User
            {
                ID = 2,
                Address = "Phase 1 Hinjewadi",
                City = "Pune",
                Country = "India",
                EmailAddress = "dnyaneshwar@gmail.com",
                EmailIsVerified = true,
                IsRootUser = false,
                IsSiteAdmin = true,
                JobTitle = "Manager",
                Name = "Dnyaneshwar",
                Password = "xyz123",
                PhoneNumber = "9438594839",
                PhoneNumberIsVerified = true,
                State = "Maharashtra",
                UserName = "vinstar",
                ZipCode = "600031",
                Photo = "",
                CreatedByUserID = 1,
                CreatedOn = DateTime.Now
            }, new User
            {
                ID = 3,
                Address = "Phase 1 Hinjewadi",
                City = "Pune",
                Country = "India",
                EmailAddress = "double@gmail.com",
                EmailIsVerified = true,
                IsRootUser = false,
                IsSiteAdmin = false,
                JobTitle = "Waiter",
                Name = "Double",
                Password = "xyz123",
                PhoneNumber = "8928548545",
                PhoneNumberIsVerified = true,
                State = "Maharashtra",
                UserName = "double",
                ZipCode = "600031",
                Photo = "",
                CreatedByUserID = 2,
                CreatedOn = DateTime.Now
            }, new User
            {
                ID = 4,
                Address = "Phase 1 Hinjewadi",
                City = "Pune",
                Country = "India",
                EmailAddress = "ashish@gmail.com",
                EmailIsVerified = true,
                IsRootUser = false,
                IsSiteAdmin = false,
                JobTitle = "Purchasing Manager",
                Name = "Ashish",
                Password = "xyz123",
                PhoneNumber = "8928548545",
                PhoneNumberIsVerified = true,
                State = "Maharashtra",
                UserName = "purchasing",
                ZipCode = "600031",
                Photo = "",
                CreatedByUserID = 2,
                CreatedOn = DateTime.Now
            });

            context.Sites.SqlQuery("set identity_insert usersites on");
            context.UserSites.AddOrUpdate(u => new
            {
                u.ID,
                u.SiteID,
                u.UserID
            }, new UserSite
            {
                ID = 1,
                SiteID = 2,
                UserID = 2
            }, new UserSite
            {
                ID = 1,
                SiteID = 2,
                UserID = 3
            }, new UserSite
            {
                ID = 1,
                SiteID = 2,
                UserID = 4
            });

            context.Sites.SqlQuery("set identity_insert controlleractions on");
            context.ControllerActions.AddOrUpdate(u => new
            {
                u.ID,
                u.ControllerActionName,
                u.Description
            }, new ControllerAction
            {
                ID = 1,
                ControllerActionName = "GetProductMeasurementTypes",
                Description = "Allow read access to Product Measurement Types"
            }, new ControllerAction
            {
                ID = 2,
                ControllerActionName = "GetProductMeasurementType",
                Description = "Allow read access to Product Measurement Type"
            }, new ControllerAction
            {
                ID = 3,
                ControllerActionName = "PutProductMeasurementType",
                Description = "Allow edit access to Product Measurement Type"
            }, new ControllerAction
            {
                ID = 4,
                ControllerActionName = "PostProductMeasurementType",
                Description = "Allow add access to Product Measurement Type"
            }, new ControllerAction
            {
                ID = 5,
                ControllerActionName = "DeleteProductMeasurementType",
                Description = "Allow delete access to Product Measurement Type"
            }, new ControllerAction
            {
                ID = 6,
                ControllerActionName = "GetProductTypeCategories",
                Description = "Allow read access to Product Type Categories"
            }, new ControllerAction
            {
                ID = 7,
                ControllerActionName = "GetProductTypeCategory",
                Description = "Allow read access to Product Type Category"
            }, new ControllerAction
            {
                ID = 8,
                ControllerActionName = "PutProductTypeCategory",
                Description = "Allow edit access to Product Type Category"
            }, new ControllerAction
            {
                ID = 9,
                ControllerActionName = "PostProductTypeCategory",
                Description = "Allow add access to Product Type Category"
            }, new ControllerAction
            {
                ID = 10,
                ControllerActionName = "DeleteProductTypeCategory",
                Description = "Allow delete access to Product Type Category"
            }, new ControllerAction
            {
                ID = 11,
                ControllerActionName = "GetProductTypes",
                Description = "Allow read access to Product Types"
            }, new ControllerAction
            {
                ID = 12,
                ControllerActionName = "GetProductType",
                Description = "Allow read access to Product Type"
            }, new ControllerAction
            {
                ID = 13,
                ControllerActionName = "PutProductType",
                Description = "Allow edit access to Product Type"
            }, new ControllerAction
            {
                ID = 14,
                ControllerActionName = "PostProductType",
                Description = "Allow add access to Product Type"
            }, new ControllerAction
            {
                ID = 15,
                ControllerActionName = "DeleteProductType",
                Description = "Allow delete access to Product Type"
            }, new ControllerAction
            {
                ID = 16,
                ControllerActionName = "GetVendorContactDetails",
                Description = "Allow read access to Vendor Contact Details"
            }, new ControllerAction
            {
                ID = 17,
                ControllerActionName = "GetVendorContactDetail",
                Description = "Allow read access to Vendor Contact Detail"
            }, new ControllerAction
            {
                ID = 18,
                ControllerActionName = "PutVendorContactDetail",
                Description = "Allow edit access to Vendor Contact Detail"
            }, new ControllerAction
            {
                ID = 19,
                ControllerActionName = "PostVendorContactDetail",
                Description = "Allow add access to Vendor Contact Detail"
            }, new ControllerAction
            {
                ID = 20,
                ControllerActionName = "DeleteVendorContactDetail",
                Description = "Allow delete access to Vendor Contact Detail"
            }, new ControllerAction
            {
                ID = 21,
                ControllerActionName = "GetVendorProductTypes",
                Description = "Allow read access to Vendor Product Types"
            }, new ControllerAction
            {
                ID = 22,
                ControllerActionName = "GetVendorProductType",
                Description = "Allow read access to Vendor Product Type"
            }, new ControllerAction
            {
                ID = 23,
                ControllerActionName = "PutVendorProductType",
                Description = "Allow edit access to Vendor Product Type"
            }, new ControllerAction
            {
                ID = 24,
                ControllerActionName = "PostVendorProductType",
                Description = "Allow add access to Vendor Product Type"
            }, new ControllerAction
            {
                ID = 25,
                ControllerActionName = "DeleteVendorProductType",
                Description = "Allow delete access to Vendor Product Type"
            }, new ControllerAction
            {
                ID = 26,
                ControllerActionName = "GetUsers",
                Description = "Allow read access to Users"
            }, new ControllerAction
            {
                ID = 27,
                ControllerActionName = "GetUser",
                Description = "Allow read access to User"
            }, new ControllerAction
            {
                ID = 28,
                ControllerActionName = "PutUser",
                Description = "Allow edit access to User"
            }, new ControllerAction
            {
                ID = 29,
                ControllerActionName = "PostUser",
                Description = "Allow add access to User"
            }, new ControllerAction
            {
                ID = 30,
                ControllerActionName = "DeleteUser",
                Description = "Allow delete access to User"
            }, new ControllerAction
            {
                ID = 31,
                ControllerActionName = "GetVendors",
                Description = "Allow read access to Vendor"
            }, new ControllerAction
            {
                ID = 32,
                ControllerActionName = "GetVendor",
                Description = "Allow read access to Vendor"
            }, new ControllerAction
            {
                ID = 33,
                ControllerActionName = "PutVendor",
                Description = "Allow edit access to Vendor"
            }, new ControllerAction
            {
                ID = 34,
                ControllerActionName = "PostVendor",
                Description = "Allow add access to Vendor"
            }, new ControllerAction
            {
                ID = 35,
                ControllerActionName = "DeleteVendor",
                Description = "Allow delete access to Vendor"
            }, new ControllerAction
            {
                ID = 36,
                ControllerActionName = "GetPurchaseOrders",
                Description = "Allow read access to Purchase Orders"
            }, new ControllerAction
            {
                ID = 37,
                ControllerActionName = "GetPurchaseOrder",
                Description = "Allow read access to Purchase Orders"
            }, new ControllerAction
            {
                ID = 38,
                ControllerActionName = "PutPurchaseOrder",
                Description = "Allow edit access to Purchase Orders"
            }, new ControllerAction
            {
                ID = 39,
                ControllerActionName = "PostPurchaseOrder",
                Description = "Allow add access to Purchase Orders"
            }, new ControllerAction
            {
                ID = 40,
                ControllerActionName = "DeletePurchaseOrder",
                Description = "Allow delete access to Purchase Orders"
            }, new ControllerAction
            {
                ID = 41,
                ControllerActionName = "GetInventories",
                Description = "Allow read access to Inventory"
            }, new ControllerAction
            {
                ID = 42,
                ControllerActionName = "GetInventory",
                Description = "Allow read access to Inventory"
            }, new ControllerAction
            {
                ID = 43,
                ControllerActionName = "PutInventory",
                Description = "Allow edit access to Inventory"
            }, new ControllerAction
            {
                ID = 44,
                ControllerActionName = "PostInventory",
                Description = "Allow add access to Inventory"
            }, new ControllerAction
            {
                ID = 45,
                ControllerActionName = "DeleteInventory",
                Description = "Allow delete access to Inventory"
            }, new ControllerAction
            {
                ID = 46,
                ControllerActionName = "GetInventoryLocations",
                Description = "Allow read access to Inventory Locations"
            }, new ControllerAction
            {
                ID = 47,
                ControllerActionName = "GetInventoryLocation",
                Description = "Allow read access to Inventory Location"
            }, new ControllerAction
            {
                ID = 48,
                ControllerActionName = "PutInventoryLocation",
                Description = "Allow edit access to Inventory Location"
            }, new ControllerAction
            {
                ID = 49,
                ControllerActionName = "PostInventoryLocation",
                Description = "Allow add access to Inventory Location"
            }, new ControllerAction
            {
                ID = 50,
                ControllerActionName = "DeleteInventoryLocation",
                Description = "Allow delete access to Inventory Location"
            }, new ControllerAction
            {
                ID = 51,
                ControllerActionName = "GetInventoryQuotas",
                Description = "Allow read access to Inventory Quotas"
            }, new ControllerAction
            {
                ID = 52,
                ControllerActionName = "GetInventoryQuota",
                Description = "Allow read access to Inventory Quota"
            }, new ControllerAction
            {
                ID = 53,
                ControllerActionName = "PutInventoryQuota",
                Description = "Allow edit access to Inventory Quota"
            }, new ControllerAction
            {
                ID = 54,
                ControllerActionName = "PostInventoryQuota",
                Description = "Allow add access to Inventory Quota"
            }, new ControllerAction
            {
                ID = 55,
                ControllerActionName = "DeleteInventoryQuota",
                Description = "Allow delete access to Inventory Quota"
            }, new ControllerAction
            {
                ID = 56,
                ControllerActionName = "GetRequisitions",
                Description = "Allow read access to Requisitions"
            }, new ControllerAction
            {
                ID = 57,
                ControllerActionName = "GetRequisition",
                Description = "Allow read access to Requisition"
            }, new ControllerAction
            {
                ID = 58,
                ControllerActionName = "PutRequisition",
                Description = "Allow edit access to Requisition"
            }, new ControllerAction
            {
                ID = 59,
                ControllerActionName = "PostRequisition",
                Description = "Allow add access to Requisition"
            }, new ControllerAction
            {
                ID = 60,
                ControllerActionName = "DeleteRequisition",
                Description = "Allow delete access to Requisition"
            }, new ControllerAction
            {
                ID = 61,
                ControllerActionName = "GetSettlementTypes",
                Description = "Allow read access to Settlement Types"
            }, new ControllerAction
            {
                ID = 62,
                ControllerActionName = "GetSettlementType",
                Description = "Allow read access to Settlement Type"
            }, new ControllerAction
            {
                ID = 63,
                ControllerActionName = "PutSettlementType",
                Description = "Allow edit access to Settlement Type"
            }, new ControllerAction
            {
                ID = 64,
                ControllerActionName = "PostSettlementType",
                Description = "Allow add access to Settlement Type"
            }, new ControllerAction
            {
                ID = 65,
                ControllerActionName = "DeleteSettlementType",
                Description = "Allow delete access to Settlement Type"
            }, new ControllerAction
            {
                ID = 66,
                ControllerActionName = "GetMenuCategories",
                Description = "Allow read access to Menu Categories"
            }, new ControllerAction
            {
                ID = 67,
                ControllerActionName = "GetMenuCategory",
                Description = "Allow read access to Menu Category"
            }, new ControllerAction
            {
                ID = 68,
                ControllerActionName = "PutMenuCategory",
                Description = "Allow edit access to Menu Category"
            }, new ControllerAction
            {
                ID = 69,
                ControllerActionName = "PostMenuCategory",
                Description = "Allow add access to Menu Category"
            }, new ControllerAction
            {
                ID = 70,
                ControllerActionName = "DeleteMenuCategory",
                Description = "Allow delete access to Menu Category"
            }, new ControllerAction
            {
                ID = 71,
                ControllerActionName = "GetMenuItemIngredients",
                Description = "Allow read access to Menu Item Ingredients"
            }, new ControllerAction
            {
                ID = 72,
                ControllerActionName = "GetMenuItemIngredient",
                Description = "Allow read access to Menu Item Ingredient"
            }, new ControllerAction
            {
                ID = 73,
                ControllerActionName = "PutMenuItemIngredient",
                Description = "Allow edit access to Menu Item Ingredient"
            }, new ControllerAction
            {
                ID = 74,
                ControllerActionName = "PostMenuItemIngredient",
                Description = "Allow add access to Menu Item Ingredient"
            }, new ControllerAction
            {
                ID = 75,
                ControllerActionName = "DeleteMenuItemIngredient",
                Description = "Allow delete access to Menu Item Ingredient"
            }, new ControllerAction
            {
                ID = 76,
                ControllerActionName = "GetMenuItemPrices",
                Description = "Allow read access to Menu Item Prices"
            }, new ControllerAction
            {
                ID = 77,
                ControllerActionName = "GetMenuItemPrice",
                Description = "Allow read access to Menu Item Price"
            }, new ControllerAction
            {
                ID = 78,
                ControllerActionName = "PutMenuItemPrice",
                Description = "Allow edit access to Menu Item Price"
            }, new ControllerAction
            {
                ID = 79,
                ControllerActionName = "PostMenuItemPrice",
                Description = "Allow add access to Menu Item Price"
            }, new ControllerAction
            {
                ID = 80,
                ControllerActionName = "DeleteMenuItemPrice",
                Description = "Allow delete access to Menu Item Price"
            }, new ControllerAction
            {
                ID = 81,
                ControllerActionName = "GetMenuItems",
                Description = "Allow read access to Menu Items"
            }, new ControllerAction
            {
                ID = 82,
                ControllerActionName = "GetMenuItem",
                Description = "Allow read access to Menu Item"
            }, new ControllerAction
            {
                ID = 83,
                ControllerActionName = "PutMenuItem",
                Description = "Allow edit access to Menu Item"
            }, new ControllerAction
            {
                ID = 84,
                ControllerActionName = "PostMenuItem",
                Description = "Allow add access to Menu Item"
            }, new ControllerAction
            {
                ID = 85,
                ControllerActionName = "DeleteMenuItem",
                Description = "Allow delete access to Menu Item"
            }, new ControllerAction
            {
                ID = 86,
                ControllerActionName = "GetOrderItems",
                Description = "Allow read access to Order Items"
            }, new ControllerAction
            {
                ID = 87,
                ControllerActionName = "GetOrderItem",
                Description = "Allow read access to Order Item"
            }, new ControllerAction
            {
                ID = 88,
                ControllerActionName = "PutOrderItem",
                Description = "Allow edit access to Order Item"
            }, new ControllerAction
            {
                ID = 89,
                ControllerActionName = "PostOrderItem",
                Description = "Allow add access to Order Item"
            }, new ControllerAction
            {
                ID = 90,
                ControllerActionName = "DeleteOrderItem",
                Description = "Allow delete access to Order Item"
            }, new ControllerAction
            {
                ID = 91,
                ControllerActionName = "GetOrders",
                Description = "Allow read access to Orders"
            }, new ControllerAction
            {
                ID = 92,
                ControllerActionName = "GetOrder",
                Description = "Allow read access to Order"
            }, new ControllerAction
            {
                ID = 93,
                ControllerActionName = "PutOrder",
                Description = "Allow edit access to Order"
            }, new ControllerAction
            {
                ID = 94,
                ControllerActionName = "PostOrder",
                Description = "Allow add access to Order"
            }, new ControllerAction
            {
                ID = 95,
                ControllerActionName = "DeleteOrder",
                Description = "Allow delete access to Order"
            }, new ControllerAction
            {
                ID = 96,
                ControllerActionName = "GetReceived",
                Description = "Allow read access to Received"
            }, new ControllerAction
            {
                ID = 97,
                ControllerActionName = "GetReceiving",
                Description = "Allow read access to Receiving"
            }, new ControllerAction
            {
                ID = 98,
                ControllerActionName = "PutReceiving",
                Description = "Allow edit access to Receiving"
            }, new ControllerAction
            {
                ID = 99,
                ControllerActionName = "PostReceiving",
                Description = "Allow add access to Receiving"
            }, new ControllerAction
            {
                ID = 100,
                ControllerActionName = "DeleteReceiving",
                Description = "Allow delete access to Receiving"
            }, new ControllerAction
            {
                ID = 101,
                ControllerActionName = "GetVendorProductTypePrices",
                Description = "Allow read access to Vendor Product Type Prices"
            }, new ControllerAction
            {
                ID = 102,
                ControllerActionName = "GetVendorProductTypePrice",
                Description = "Allow read access to Vendor Product Type Price"
            }, new ControllerAction
            {
                ID = 103,
                ControllerActionName = "PutVendorProductTypePrice",
                Description = "Allow edit access to Vendor Product Type Price"
            }, new ControllerAction
            {
                ID = 104,
                ControllerActionName = "PostVendorProductTypePrice",
                Description = "Allow add access to Vendor Product Type Price"
            }, new ControllerAction
            {
                ID = 105,
                ControllerActionName = "DeleteVendorProductTypePrice",
                Description = "Allow delete access to Vendor Product Type Price"
            }, new ControllerAction
            {
                ID = 106,
                ControllerActionName = "GetControllerActions",
                Description = "Allow read access to Controller Actions"
            }, new ControllerAction
            {
                ID = 107,
                ControllerActionName = "GetUsers",
                Description = "Allow read access to Users"
            }, new ControllerAction
            {
                ID = 108,
                ControllerActionName = "GetUser",
                Description = "Allow read access to User"
            }, new ControllerAction
            {
                ID = 109,
                ControllerActionName = "PutUser",
                Description = "Allow edit access to User"
            }, new ControllerAction
            {
                ID = 110,
                ControllerActionName = "PostUser",
                Description = "Allow add access to User"
            }, new ControllerAction
            {
                ID = 111,
                ControllerActionName = "DeleteUser",
                Description = "Allow delete access to User"
            });

            context.Sites.SqlQuery("set identity_insert usercontrolleractionpermissions on");
            context.UserControllerActionPermissions.AddOrUpdate(u => new
            {
                u.ID,
                u.ControllerActionID,
                u.UserID,
                u.Allow,
                u.EntryByUserID,
                u.SiteID
            }, new UserControllerActionPermission
            {
                ID = 1,
                ControllerActionID = 1,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 2,
                ControllerActionID = 2,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 3,
                ControllerActionID = 3,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 4,
                ControllerActionID = 4,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 5,
                ControllerActionID = 5,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 6,
                ControllerActionID = 6,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 7,
                ControllerActionID = 7,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 8,
                ControllerActionID = 8,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 9,
                ControllerActionID = 9,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 10,
                ControllerActionID = 10,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 11,
                ControllerActionID = 11,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 12,
                ControllerActionID = 12,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 13,
                ControllerActionID = 13,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 14,
                ControllerActionID = 14,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 15,
                ControllerActionID = 15,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 16,
                ControllerActionID = 31,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 17,
                ControllerActionID = 32,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 18,
                ControllerActionID = 33,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 19,
                ControllerActionID = 34,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 20,
                ControllerActionID = 35,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 21,
                ControllerActionID = 16,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 22,
                ControllerActionID = 17,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 23,
                ControllerActionID = 18,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 24,
                ControllerActionID = 19,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 25,
                ControllerActionID = 20,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 26,
                ControllerActionID = 21,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 27,
                ControllerActionID = 22,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 28,
                ControllerActionID = 23,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 29,
                ControllerActionID = 24,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            }, new UserControllerActionPermission
            {
                ID = 30,
                ControllerActionID = 25,
                UserID = 4,
                Allow = true,
                EntryByUserID = 2,
                SiteID = 2
            });

            context.Sites.SqlQuery("set identity_insert producttypecategories on");
            context.ProductTypeCategories.AddOrUpdate(u => new
            {
                u.ID,
                u.Name,
                u.Description,
                u.EntryByUserID,
                u.SiteID
            }, new ProductTypeCategory
            {
                ID = 1,
                Name = "Non Veg",
                Description = "Meat, Eggs, Fish, Poultry, etc.",
                EntryByUserID = 2,
                SiteID = 2
            }, new ProductTypeCategory
            {
                ID = 2,
                Name = "Veg",
                Description = "Vegetables",
                EntryByUserID = 2,
                SiteID = 2
            }, new ProductTypeCategory
            {
                ID = 3,
                Name = "Beverages",
                Description = "Drinkable Items",
                EntryByUserID = 2,
                SiteID = 2
            }, new ProductTypeCategory
            {
                ID = 4,
                Name = "Things",
                Description = "Cutlery, Glasses, etc.",
                EntryByUserID = 2,
                SiteID = 2
            });

            context.Sites.SqlQuery("set identity_insert productmeasurementtypes on");
            context.ProductMeasurementTypes.AddOrUpdate(u => new
            {
                u.ID,
                u.Name,
                u.Description,
                u.EntryByUserID,
                u.SiteID
            }, new ProductMeasurementType
            {
                ID = 1,
                Name = "Kg.",
                Description = "Kilograms in weight",
                EntryByUserID = 2,
                SiteID = 2
            }, new ProductMeasurementType
            {
                ID = 2,
                Name = "Unit",
                Description = "An item ex. Packet, Bottle, Box of Cereal, etc.",
                EntryByUserID = 2,
                SiteID = 2
            }, new ProductMeasurementType
            {
                ID = 3,
                Name = "g.",
                Description = "Grams in weight",
                EntryByUserID = 2,
                SiteID = 2
            });

            context.Sites.SqlQuery("set identity_insert producttypes on");
            context.ProductTypes.AddOrUpdate(u => new
            {
                u.ID,
                u.Name,
                u.ProductTypeCategoryID,
                u.ProductMeasurementTypeID,
                u.Image,
                u.EntryByUserID,
                u.SiteID
            }, new ProductType
            {
                ID = 1,
                Name = "Boneless Chicken",
                ProductTypeCategoryID = 1,
                ProductMeasurementTypeID = 1,
                Image = "xyz",
                EntryByUserID = 2,
                SiteID = 2
            }, new ProductType
            {
                ID = 2,
                Name = "Chicken Legs",
                ProductTypeCategoryID = 1,
                ProductMeasurementTypeID = 1,
                Image = "xyz",
                EntryByUserID = 2,
                SiteID = 2
            }, new ProductType
            {
                ID = 3,
                Name = "Chicken Nuggets",
                ProductTypeCategoryID = 1,
                ProductMeasurementTypeID = 1,
                Image = "xyz",
                EntryByUserID = 2,
                SiteID = 2
            }, new ProductType
            {
                ID = 4,
                Name = "Whole Chicken",
                ProductTypeCategoryID = 1,
                ProductMeasurementTypeID = 1,
                Image = "xyz",
                EntryByUserID = 2,
                SiteID = 2
            }, new ProductType
            {
                ID = 5,
                Name = "Chicken Breast",
                ProductTypeCategoryID = 1,
                ProductMeasurementTypeID = 1,
                Image = "xyz",
                EntryByUserID = 2,
                SiteID = 2
            });

            context.Vendors.SqlQuery("set identity_insert vendors on");
            context.Vendors.AddOrUpdate(u => new
            {
                u.ID,
                u.Name,
                u.Address,
                u.City,
                u.State,
                u.Country,
                u.ZipCode,
                u.EntryByUserID,
                u.SiteID,
                u.TransactionDateTime
            }, new Vendor
            {
                ID = 1,
                Name = "XYZ Butchers",
                Address = "Phase 1 Hinjewadi",
                City = "Pune",
                State = "Maharashtra",
                Country = "India",
                ZipCode = "400031",
                EntryByUserID = 2,
                SiteID = 2,
                TransactionDateTime = DateTime.Now
            }, new Vendor
            {
                ID = 2,
                Name = "ABC Vegetables",
                Address = "Phase 1 Hinjewadi",
                City = "Pune",
                State = "Maharashtra",
                Country = "India",
                ZipCode = "400031",
                EntryByUserID = 2,
                SiteID = 2,
                TransactionDateTime = DateTime.Now
            });

            context.VendorContactDetails.SqlQuery("set identity_insert vendorcontactdetails on");
            context.VendorContactDetails.AddOrUpdate(u => new
            {
                u.ID,
                u.Name,
                u.PhoneNumber,
                u.EmailAddress,
                u.Description,
                u.VendorID,
                u.EntryByUserID,
                u.SiteID,
                u.TransactionDateTime
            }, new VendorContactDetail
            {
                ID = 1,
                Name = "Dilip",
                PhoneNumber = "2398724987",
                EmailAddress = "dilip@xyz.com",
                Description = "Delivery Manager",
                VendorID = 1,
                EntryByUserID = 2,
                SiteID = 2,
                TransactionDateTime = DateTime.Now
            }, new VendorContactDetail
            {
                ID = 2,
                Name = "Zubin",
                PhoneNumber = "2398724987",
                EmailAddress = "zubin@xyz.com",
                Description = "Director",
                VendorID = 1,
                EntryByUserID = 2,
                SiteID = 2,
                TransactionDateTime = DateTime.Now
            }, new VendorContactDetail
            {
                ID = 3,
                Name = "Rahul",
                PhoneNumber = "2983453249",
                EmailAddress = "rahul@abc.com",
                VendorID = 2,
                EntryByUserID = 2,
                SiteID = 2,
                TransactionDateTime = DateTime.Now
            });
        }
    }
}
