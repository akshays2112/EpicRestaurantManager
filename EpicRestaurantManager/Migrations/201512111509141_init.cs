namespace EpicRestaurantManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ControllerActions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ControllerActionName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserControllerActionPermissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ControllerActionID = c.Int(nullable: false),
                        Allow = c.Boolean(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                        ControllerAction_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ControllerActions", t => t.ControllerActionID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .ForeignKey("dbo.ControllerActions", t => t.ControllerAction_ID)
                .Index(t => t.UserID)
                .Index(t => t.ControllerActionID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID)
                .Index(t => t.ControllerAction_ID);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameOfBusiness = c.String(nullable: false),
                        HomeHeaderImage = c.String(),
                        ServiceChargePercentage = c.Single(nullable: false),
                        VATPercentage = c.Single(nullable: false),
                        ServiceTaxPercentage = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductTypeID = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        ProductType_ID = c.Int(),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductType_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.ProductTypeID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.ProductType_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.InventoryLocations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductTypeID = c.Int(nullable: false),
                        LocationDescription = c.String(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        Inventory_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Inventories", t => t.Inventory_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.ProductTypeID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.Inventory_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ProductTypeCategoryID = c.Int(nullable: false),
                        ProductMeasurementTypeID = c.Int(nullable: false),
                        Image = c.String(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        ProductMeasurementType_ID = c.Int(),
                        ProductTypeCategory_ID = c.Int(),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductMeasurementTypes", t => t.ProductMeasurementType_ID)
                .ForeignKey("dbo.ProductTypeCategories", t => t.ProductTypeCategory_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.ProductMeasurementTypes", t => t.ProductMeasurementTypeID)
                .ForeignKey("dbo.ProductTypeCategories", t => t.ProductTypeCategoryID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.ProductTypeCategoryID)
                .Index(t => t.ProductMeasurementTypeID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.ProductMeasurementType_ID)
                .Index(t => t.ProductTypeCategory_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.MenuItemIngredients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MenuItemID = c.Int(nullable: false),
                        ProductTypeID = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        MenuItem_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.MenuItems", t => t.MenuItem_ID)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.MenuItemID)
                .Index(t => t.ProductTypeID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.MenuItem_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MenuCategoryID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        MenuCategory_ID = c.Int(),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MenuCategories", t => t.MenuCategory_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.MenuCategories", t => t.MenuCategoryID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.MenuCategoryID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.MenuCategory_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.MenuCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        EmailIsVerified = c.Boolean(nullable: false),
                        PhoneNumber = c.String(),
                        PhoneNumberIsVerified = c.Boolean(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        ZipCode = c.String(),
                        Photo = c.String(),
                        JobTitle = c.String(nullable: false),
                        IsSiteAdmin = c.Boolean(nullable: false),
                        IsRootUser = c.Boolean(nullable: false),
                        Password = c.String(nullable: false),
                        CreatedByUserID = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.InventoryQuotas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductTypeID = c.Int(nullable: false),
                        MinimumStock = c.Single(nullable: false),
                        MaximumStock = c.Single(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        Inventory_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Inventories", t => t.Inventory_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.ProductTypeID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.Inventory_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.MenuItemPrices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MenuItemID = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        MenuItem_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.MenuItems", t => t.MenuItem_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.MenuItemID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.MenuItem_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        MenuItemID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CustomOrderDescription = c.String(),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        MenuItem_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.MenuItems", t => t.MenuItem_ID)
                .Index(t => t.OrderID)
                .Index(t => t.MenuItemID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.MenuItem_ID);
            
            CreateTable(
                "dbo.Chefs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderItemID = c.Int(nullable: false),
                        BeingPrepared = c.Boolean(nullable: false),
                        StartedPreparing = c.DateTime(nullable: false),
                        ReadyForPickup = c.Boolean(nullable: false),
                        FinishedPreparing = c.DateTime(nullable: false),
                        Delivered = c.Boolean(nullable: false),
                        DeliveredToGuests = c.DateTime(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OrderItems", t => t.OrderItemID, cascadeDelete: true)
                .ForeignKey("dbo.Sites", t => t.SiteID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.EntryByUserID, cascadeDelete: true)
                .Index(t => t.OrderItemID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerDescription = c.String(nullable: false),
                        Settled = c.Boolean(nullable: false),
                        SettlementTypeID = c.Int(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        SettlementType_ID = c.Int(),
                        User_ID = c.Int(),
                        MenuItem_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SettlementTypes", t => t.SettlementType_ID)
                .ForeignKey("dbo.SettlementTypes", t => t.SettlementTypeID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.MenuItems", t => t.MenuItem_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.SettlementTypeID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.SettlementType_ID)
                .Index(t => t.User_ID)
                .Index(t => t.MenuItem_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.SettlementTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.ProductMeasurementTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.ProductTypeCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VendorProductTypeID = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        OrderPlacedOnDate = c.DateTime(nullable: false),
                        ExpectedDeliveryDate = c.DateTime(nullable: false),
                        Comments = c.String(),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        VendorProductType_ID = c.Int(),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.VendorProductTypes", t => t.VendorProductType_ID)
                .ForeignKey("dbo.VendorProductTypes", t => t.VendorProductTypeID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.VendorProductTypeID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.VendorProductType_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.VendorProductTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VendorID = c.Int(nullable: false),
                        ProductTypeID = c.Int(nullable: false),
                        Description = c.String(),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        Vendor_ID = c.Int(),
                        User_ID = c.Int(),
                        ProductType_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Vendors", t => t.Vendor_ID)
                .ForeignKey("dbo.Vendors", t => t.VendorID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductType_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.VendorID)
                .Index(t => t.ProductTypeID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.Vendor_ID)
                .Index(t => t.User_ID)
                .Index(t => t.ProductType_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.Receivings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VendorProductTypeID = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        PricePaid = c.Single(nullable: false),
                        ReceivedDate = c.DateTime(nullable: false),
                        Comments = c.String(),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        VendorProductType_ID = c.Int(),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.VendorProductTypes", t => t.VendorProductTypeID)
                .ForeignKey("dbo.VendorProductTypes", t => t.VendorProductType_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.VendorProductTypeID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.VendorProductType_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        ZipCode = c.String(),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.VendorContactDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PhoneNumber = c.String(),
                        EmailAddress = c.String(),
                        Description = c.String(),
                        VendorID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        Vendor_ID = c.Int(),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Vendors", t => t.VendorID)
                .ForeignKey("dbo.Vendors", t => t.Vendor_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.VendorID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.Vendor_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.VendorProductTypePrices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VendorProductTypeID = c.Int(nullable: false),
                        PricePerMeasurementType = c.Single(nullable: false),
                        ContractStartDate = c.DateTime(nullable: false),
                        ContractEndDate = c.DateTime(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        VendorProductType_ID = c.Int(),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.VendorProductTypes", t => t.VendorProductTypeID)
                .ForeignKey("dbo.VendorProductTypes", t => t.VendorProductType_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.VendorProductTypeID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.VendorProductType_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.Requisitions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductTypeID = c.Int(nullable: false),
                        Quantity = c.Single(nullable: false),
                        Authorized = c.Boolean(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        ProductType_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductType_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.ProductTypeID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.ProductType_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.UserGroupControllerActionPermissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserGroupID = c.Int(nullable: false),
                        ControllerActionID = c.Int(nullable: false),
                        Allow = c.Boolean(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        UserGroup_ID = c.Int(),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                        ControllerAction_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ControllerActions", t => t.ControllerActionID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_ID)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .ForeignKey("dbo.ControllerActions", t => t.ControllerAction_ID)
                .Index(t => t.UserGroupID)
                .Index(t => t.ControllerActionID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.UserGroup_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID)
                .Index(t => t.ControllerAction_ID);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.UserInGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        UserGroupID = c.Int(nullable: false),
                        EntryByUserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        TransactionDateTime = c.DateTime(nullable: false),
                        UserGroup_ID = c.Int(),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.EntryByUserID)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.UserID)
                .Index(t => t.UserGroupID)
                .Index(t => t.EntryByUserID)
                .Index(t => t.SiteID)
                .Index(t => t.UserGroup_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
            CreateTable(
                "dbo.UserSites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        SiteID = c.Int(nullable: false),
                        User_ID = c.Int(),
                        Site_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sites", t => t.SiteID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Sites", t => t.Site_ID)
                .Index(t => t.UserID)
                .Index(t => t.SiteID)
                .Index(t => t.User_ID)
                .Index(t => t.Site_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGroupControllerActionPermissions", "ControllerAction_ID", "dbo.ControllerActions");
            DropForeignKey("dbo.UserControllerActionPermissions", "ControllerAction_ID", "dbo.ControllerActions");
            DropForeignKey("dbo.UserControllerActionPermissions", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserControllerActionPermissions", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.UserControllerActionPermissions", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Vendors", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.VendorProductTypes", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.VendorProductTypePrices", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.VendorContactDetails", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.UserSites", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.UserInGroups", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.UserGroups", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.UserGroupControllerActionPermissions", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.UserControllerActionPermissions", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.Requisitions", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.Receivings", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.PurchaseOrders", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.ProductTypes", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.ProductTypeCategories", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.ProductMeasurementTypes", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.Orders", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.MenuItems", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.MenuItemPrices", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.MenuItemIngredients", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.MenuCategories", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.InventoryQuotas", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.InventoryLocations", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.Inventories", "Site_ID", "dbo.Sites");
            DropForeignKey("dbo.Inventories", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.Inventories", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Inventories", "ProductTypeID", "dbo.ProductTypes");
            DropForeignKey("dbo.InventoryQuotas", "Inventory_ID", "dbo.Inventories");
            DropForeignKey("dbo.InventoryLocations", "Inventory_ID", "dbo.Inventories");
            DropForeignKey("dbo.InventoryLocations", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.InventoryLocations", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.InventoryLocations", "ProductTypeID", "dbo.ProductTypes");
            DropForeignKey("dbo.VendorProductTypes", "ProductType_ID", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductTypes", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.ProductTypes", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Requisitions", "ProductType_ID", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductTypes", "ProductTypeCategoryID", "dbo.ProductTypeCategories");
            DropForeignKey("dbo.ProductTypes", "ProductMeasurementTypeID", "dbo.ProductMeasurementTypes");
            DropForeignKey("dbo.MenuItemIngredients", "ProductTypeID", "dbo.ProductTypes");
            DropForeignKey("dbo.MenuItemIngredients", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.MenuItemIngredients", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.MenuItemIngredients", "MenuItemID", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItems", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.MenuItems", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Orders", "MenuItem_ID", "dbo.MenuItems");
            DropForeignKey("dbo.OrderItems", "MenuItem_ID", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItemPrices", "MenuItem_ID", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItemIngredients", "MenuItem_ID", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItems", "MenuCategoryID", "dbo.MenuCategories");
            DropForeignKey("dbo.MenuCategories", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.Vendors", "User_ID", "dbo.Users");
            DropForeignKey("dbo.VendorProductTypes", "User_ID", "dbo.Users");
            DropForeignKey("dbo.VendorProductTypePrices", "User_ID", "dbo.Users");
            DropForeignKey("dbo.VendorContactDetails", "User_ID", "dbo.Users");
            DropForeignKey("dbo.UserSites", "User_ID", "dbo.Users");
            DropForeignKey("dbo.UserSites", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserSites", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.UserInGroups", "User_ID", "dbo.Users");
            DropForeignKey("dbo.UserGroups", "User_ID", "dbo.Users");
            DropForeignKey("dbo.UserGroupControllerActionPermissions", "User_ID", "dbo.Users");
            DropForeignKey("dbo.UserGroupControllerActionPermissions", "UserGroupID", "dbo.UserGroups");
            DropForeignKey("dbo.UserInGroups", "UserGroup_ID", "dbo.UserGroups");
            DropForeignKey("dbo.UserInGroups", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserInGroups", "UserGroupID", "dbo.UserGroups");
            DropForeignKey("dbo.UserInGroups", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.UserInGroups", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.UserGroupControllerActionPermissions", "UserGroup_ID", "dbo.UserGroups");
            DropForeignKey("dbo.UserGroups", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.UserGroups", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.UserGroupControllerActionPermissions", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.UserGroupControllerActionPermissions", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.UserGroupControllerActionPermissions", "ControllerActionID", "dbo.ControllerActions");
            DropForeignKey("dbo.UserControllerActionPermissions", "User_ID", "dbo.Users");
            DropForeignKey("dbo.SettlementTypes", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Requisitions", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Requisitions", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.Requisitions", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Requisitions", "ProductTypeID", "dbo.ProductTypes");
            DropForeignKey("dbo.Receivings", "User_ID", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrders", "User_ID", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrders", "VendorProductTypeID", "dbo.VendorProductTypes");
            DropForeignKey("dbo.VendorProductTypePrices", "VendorProductType_ID", "dbo.VendorProductTypes");
            DropForeignKey("dbo.VendorProductTypePrices", "VendorProductTypeID", "dbo.VendorProductTypes");
            DropForeignKey("dbo.VendorProductTypePrices", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.VendorProductTypePrices", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.VendorProductTypes", "VendorID", "dbo.Vendors");
            DropForeignKey("dbo.Vendors", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.Vendors", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.VendorProductTypes", "Vendor_ID", "dbo.Vendors");
            DropForeignKey("dbo.VendorContactDetails", "Vendor_ID", "dbo.Vendors");
            DropForeignKey("dbo.VendorContactDetails", "VendorID", "dbo.Vendors");
            DropForeignKey("dbo.VendorContactDetails", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.VendorContactDetails", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.VendorProductTypes", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.VendorProductTypes", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Receivings", "VendorProductType_ID", "dbo.VendorProductTypes");
            DropForeignKey("dbo.Receivings", "VendorProductTypeID", "dbo.VendorProductTypes");
            DropForeignKey("dbo.Receivings", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.Receivings", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.PurchaseOrders", "VendorProductType_ID", "dbo.VendorProductTypes");
            DropForeignKey("dbo.VendorProductTypes", "ProductTypeID", "dbo.ProductTypes");
            DropForeignKey("dbo.PurchaseOrders", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.PurchaseOrders", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.ProductTypes", "User_ID", "dbo.Users");
            DropForeignKey("dbo.ProductTypeCategories", "User_ID", "dbo.Users");
            DropForeignKey("dbo.ProductTypeCategories", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.ProductTypeCategories", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.ProductTypes", "ProductTypeCategory_ID", "dbo.ProductTypeCategories");
            DropForeignKey("dbo.ProductMeasurementTypes", "User_ID", "dbo.Users");
            DropForeignKey("dbo.ProductMeasurementTypes", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.ProductMeasurementTypes", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.ProductTypes", "ProductMeasurementType_ID", "dbo.ProductMeasurementTypes");
            DropForeignKey("dbo.Orders", "User_ID", "dbo.Users");
            DropForeignKey("dbo.OrderItems", "User_ID", "dbo.Users");
            DropForeignKey("dbo.OrderItems", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.OrderItems", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Orders", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.Orders", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Orders", "SettlementTypeID", "dbo.SettlementTypes");
            DropForeignKey("dbo.SettlementTypes", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.SettlementTypes", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Orders", "SettlementType_ID", "dbo.SettlementTypes");
            DropForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "MenuItemID", "dbo.MenuItems");
            DropForeignKey("dbo.Chefs", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.Chefs", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.Chefs", "OrderItemID", "dbo.OrderItems");
            DropForeignKey("dbo.MenuItems", "User_ID", "dbo.Users");
            DropForeignKey("dbo.MenuItemPrices", "User_ID", "dbo.Users");
            DropForeignKey("dbo.MenuItemPrices", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.MenuItemPrices", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.MenuItemPrices", "MenuItemID", "dbo.MenuItems");
            DropForeignKey("dbo.MenuItemIngredients", "User_ID", "dbo.Users");
            DropForeignKey("dbo.MenuCategories", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Inventories", "User_ID", "dbo.Users");
            DropForeignKey("dbo.InventoryQuotas", "User_ID", "dbo.Users");
            DropForeignKey("dbo.InventoryQuotas", "EntryByUserID", "dbo.Users");
            DropForeignKey("dbo.InventoryQuotas", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.InventoryQuotas", "ProductTypeID", "dbo.ProductTypes");
            DropForeignKey("dbo.InventoryLocations", "User_ID", "dbo.Users");
            DropForeignKey("dbo.MenuCategories", "SiteID", "dbo.Sites");
            DropForeignKey("dbo.MenuItems", "MenuCategory_ID", "dbo.MenuCategories");
            DropForeignKey("dbo.Inventories", "ProductType_ID", "dbo.ProductTypes");
            DropForeignKey("dbo.UserControllerActionPermissions", "ControllerActionID", "dbo.ControllerActions");
            DropIndex("dbo.UserSites", new[] { "Site_ID" });
            DropIndex("dbo.UserSites", new[] { "User_ID" });
            DropIndex("dbo.UserSites", new[] { "SiteID" });
            DropIndex("dbo.UserSites", new[] { "UserID" });
            DropIndex("dbo.UserInGroups", new[] { "Site_ID" });
            DropIndex("dbo.UserInGroups", new[] { "User_ID" });
            DropIndex("dbo.UserInGroups", new[] { "UserGroup_ID" });
            DropIndex("dbo.UserInGroups", new[] { "SiteID" });
            DropIndex("dbo.UserInGroups", new[] { "EntryByUserID" });
            DropIndex("dbo.UserInGroups", new[] { "UserGroupID" });
            DropIndex("dbo.UserInGroups", new[] { "UserID" });
            DropIndex("dbo.UserGroups", new[] { "Site_ID" });
            DropIndex("dbo.UserGroups", new[] { "User_ID" });
            DropIndex("dbo.UserGroups", new[] { "SiteID" });
            DropIndex("dbo.UserGroups", new[] { "EntryByUserID" });
            DropIndex("dbo.UserGroupControllerActionPermissions", new[] { "ControllerAction_ID" });
            DropIndex("dbo.UserGroupControllerActionPermissions", new[] { "Site_ID" });
            DropIndex("dbo.UserGroupControllerActionPermissions", new[] { "User_ID" });
            DropIndex("dbo.UserGroupControllerActionPermissions", new[] { "UserGroup_ID" });
            DropIndex("dbo.UserGroupControllerActionPermissions", new[] { "SiteID" });
            DropIndex("dbo.UserGroupControllerActionPermissions", new[] { "EntryByUserID" });
            DropIndex("dbo.UserGroupControllerActionPermissions", new[] { "ControllerActionID" });
            DropIndex("dbo.UserGroupControllerActionPermissions", new[] { "UserGroupID" });
            DropIndex("dbo.Requisitions", new[] { "Site_ID" });
            DropIndex("dbo.Requisitions", new[] { "ProductType_ID" });
            DropIndex("dbo.Requisitions", new[] { "User_ID" });
            DropIndex("dbo.Requisitions", new[] { "SiteID" });
            DropIndex("dbo.Requisitions", new[] { "EntryByUserID" });
            DropIndex("dbo.Requisitions", new[] { "ProductTypeID" });
            DropIndex("dbo.VendorProductTypePrices", new[] { "Site_ID" });
            DropIndex("dbo.VendorProductTypePrices", new[] { "User_ID" });
            DropIndex("dbo.VendorProductTypePrices", new[] { "VendorProductType_ID" });
            DropIndex("dbo.VendorProductTypePrices", new[] { "SiteID" });
            DropIndex("dbo.VendorProductTypePrices", new[] { "EntryByUserID" });
            DropIndex("dbo.VendorProductTypePrices", new[] { "VendorProductTypeID" });
            DropIndex("dbo.VendorContactDetails", new[] { "Site_ID" });
            DropIndex("dbo.VendorContactDetails", new[] { "User_ID" });
            DropIndex("dbo.VendorContactDetails", new[] { "Vendor_ID" });
            DropIndex("dbo.VendorContactDetails", new[] { "SiteID" });
            DropIndex("dbo.VendorContactDetails", new[] { "EntryByUserID" });
            DropIndex("dbo.VendorContactDetails", new[] { "VendorID" });
            DropIndex("dbo.Vendors", new[] { "Site_ID" });
            DropIndex("dbo.Vendors", new[] { "User_ID" });
            DropIndex("dbo.Vendors", new[] { "SiteID" });
            DropIndex("dbo.Vendors", new[] { "EntryByUserID" });
            DropIndex("dbo.Receivings", new[] { "Site_ID" });
            DropIndex("dbo.Receivings", new[] { "User_ID" });
            DropIndex("dbo.Receivings", new[] { "VendorProductType_ID" });
            DropIndex("dbo.Receivings", new[] { "SiteID" });
            DropIndex("dbo.Receivings", new[] { "EntryByUserID" });
            DropIndex("dbo.Receivings", new[] { "VendorProductTypeID" });
            DropIndex("dbo.VendorProductTypes", new[] { "Site_ID" });
            DropIndex("dbo.VendorProductTypes", new[] { "ProductType_ID" });
            DropIndex("dbo.VendorProductTypes", new[] { "User_ID" });
            DropIndex("dbo.VendorProductTypes", new[] { "Vendor_ID" });
            DropIndex("dbo.VendorProductTypes", new[] { "SiteID" });
            DropIndex("dbo.VendorProductTypes", new[] { "EntryByUserID" });
            DropIndex("dbo.VendorProductTypes", new[] { "ProductTypeID" });
            DropIndex("dbo.VendorProductTypes", new[] { "VendorID" });
            DropIndex("dbo.PurchaseOrders", new[] { "Site_ID" });
            DropIndex("dbo.PurchaseOrders", new[] { "User_ID" });
            DropIndex("dbo.PurchaseOrders", new[] { "VendorProductType_ID" });
            DropIndex("dbo.PurchaseOrders", new[] { "SiteID" });
            DropIndex("dbo.PurchaseOrders", new[] { "EntryByUserID" });
            DropIndex("dbo.PurchaseOrders", new[] { "VendorProductTypeID" });
            DropIndex("dbo.ProductTypeCategories", new[] { "Site_ID" });
            DropIndex("dbo.ProductTypeCategories", new[] { "User_ID" });
            DropIndex("dbo.ProductTypeCategories", new[] { "SiteID" });
            DropIndex("dbo.ProductTypeCategories", new[] { "EntryByUserID" });
            DropIndex("dbo.ProductMeasurementTypes", new[] { "Site_ID" });
            DropIndex("dbo.ProductMeasurementTypes", new[] { "User_ID" });
            DropIndex("dbo.ProductMeasurementTypes", new[] { "SiteID" });
            DropIndex("dbo.ProductMeasurementTypes", new[] { "EntryByUserID" });
            DropIndex("dbo.SettlementTypes", new[] { "User_ID" });
            DropIndex("dbo.SettlementTypes", new[] { "SiteID" });
            DropIndex("dbo.SettlementTypes", new[] { "EntryByUserID" });
            DropIndex("dbo.Orders", new[] { "Site_ID" });
            DropIndex("dbo.Orders", new[] { "MenuItem_ID" });
            DropIndex("dbo.Orders", new[] { "User_ID" });
            DropIndex("dbo.Orders", new[] { "SettlementType_ID" });
            DropIndex("dbo.Orders", new[] { "SiteID" });
            DropIndex("dbo.Orders", new[] { "EntryByUserID" });
            DropIndex("dbo.Orders", new[] { "SettlementTypeID" });
            DropIndex("dbo.Chefs", new[] { "SiteID" });
            DropIndex("dbo.Chefs", new[] { "EntryByUserID" });
            DropIndex("dbo.Chefs", new[] { "OrderItemID" });
            DropIndex("dbo.OrderItems", new[] { "MenuItem_ID" });
            DropIndex("dbo.OrderItems", new[] { "User_ID" });
            DropIndex("dbo.OrderItems", new[] { "SiteID" });
            DropIndex("dbo.OrderItems", new[] { "EntryByUserID" });
            DropIndex("dbo.OrderItems", new[] { "MenuItemID" });
            DropIndex("dbo.OrderItems", new[] { "OrderID" });
            DropIndex("dbo.MenuItemPrices", new[] { "Site_ID" });
            DropIndex("dbo.MenuItemPrices", new[] { "MenuItem_ID" });
            DropIndex("dbo.MenuItemPrices", new[] { "User_ID" });
            DropIndex("dbo.MenuItemPrices", new[] { "SiteID" });
            DropIndex("dbo.MenuItemPrices", new[] { "EntryByUserID" });
            DropIndex("dbo.MenuItemPrices", new[] { "MenuItemID" });
            DropIndex("dbo.InventoryQuotas", new[] { "Site_ID" });
            DropIndex("dbo.InventoryQuotas", new[] { "Inventory_ID" });
            DropIndex("dbo.InventoryQuotas", new[] { "User_ID" });
            DropIndex("dbo.InventoryQuotas", new[] { "SiteID" });
            DropIndex("dbo.InventoryQuotas", new[] { "EntryByUserID" });
            DropIndex("dbo.InventoryQuotas", new[] { "ProductTypeID" });
            DropIndex("dbo.MenuCategories", new[] { "Site_ID" });
            DropIndex("dbo.MenuCategories", new[] { "User_ID" });
            DropIndex("dbo.MenuCategories", new[] { "SiteID" });
            DropIndex("dbo.MenuCategories", new[] { "EntryByUserID" });
            DropIndex("dbo.MenuItems", new[] { "Site_ID" });
            DropIndex("dbo.MenuItems", new[] { "User_ID" });
            DropIndex("dbo.MenuItems", new[] { "MenuCategory_ID" });
            DropIndex("dbo.MenuItems", new[] { "SiteID" });
            DropIndex("dbo.MenuItems", new[] { "EntryByUserID" });
            DropIndex("dbo.MenuItems", new[] { "MenuCategoryID" });
            DropIndex("dbo.MenuItemIngredients", new[] { "Site_ID" });
            DropIndex("dbo.MenuItemIngredients", new[] { "MenuItem_ID" });
            DropIndex("dbo.MenuItemIngredients", new[] { "User_ID" });
            DropIndex("dbo.MenuItemIngredients", new[] { "SiteID" });
            DropIndex("dbo.MenuItemIngredients", new[] { "EntryByUserID" });
            DropIndex("dbo.MenuItemIngredients", new[] { "ProductTypeID" });
            DropIndex("dbo.MenuItemIngredients", new[] { "MenuItemID" });
            DropIndex("dbo.ProductTypes", new[] { "Site_ID" });
            DropIndex("dbo.ProductTypes", new[] { "User_ID" });
            DropIndex("dbo.ProductTypes", new[] { "ProductTypeCategory_ID" });
            DropIndex("dbo.ProductTypes", new[] { "ProductMeasurementType_ID" });
            DropIndex("dbo.ProductTypes", new[] { "SiteID" });
            DropIndex("dbo.ProductTypes", new[] { "EntryByUserID" });
            DropIndex("dbo.ProductTypes", new[] { "ProductMeasurementTypeID" });
            DropIndex("dbo.ProductTypes", new[] { "ProductTypeCategoryID" });
            DropIndex("dbo.InventoryLocations", new[] { "Site_ID" });
            DropIndex("dbo.InventoryLocations", new[] { "Inventory_ID" });
            DropIndex("dbo.InventoryLocations", new[] { "User_ID" });
            DropIndex("dbo.InventoryLocations", new[] { "SiteID" });
            DropIndex("dbo.InventoryLocations", new[] { "EntryByUserID" });
            DropIndex("dbo.InventoryLocations", new[] { "ProductTypeID" });
            DropIndex("dbo.Inventories", new[] { "Site_ID" });
            DropIndex("dbo.Inventories", new[] { "User_ID" });
            DropIndex("dbo.Inventories", new[] { "ProductType_ID" });
            DropIndex("dbo.Inventories", new[] { "SiteID" });
            DropIndex("dbo.Inventories", new[] { "EntryByUserID" });
            DropIndex("dbo.Inventories", new[] { "ProductTypeID" });
            DropIndex("dbo.UserControllerActionPermissions", new[] { "ControllerAction_ID" });
            DropIndex("dbo.UserControllerActionPermissions", new[] { "Site_ID" });
            DropIndex("dbo.UserControllerActionPermissions", new[] { "User_ID" });
            DropIndex("dbo.UserControllerActionPermissions", new[] { "SiteID" });
            DropIndex("dbo.UserControllerActionPermissions", new[] { "EntryByUserID" });
            DropIndex("dbo.UserControllerActionPermissions", new[] { "ControllerActionID" });
            DropIndex("dbo.UserControllerActionPermissions", new[] { "UserID" });
            DropTable("dbo.UserSites");
            DropTable("dbo.UserInGroups");
            DropTable("dbo.UserGroups");
            DropTable("dbo.UserGroupControllerActionPermissions");
            DropTable("dbo.Requisitions");
            DropTable("dbo.VendorProductTypePrices");
            DropTable("dbo.VendorContactDetails");
            DropTable("dbo.Vendors");
            DropTable("dbo.Receivings");
            DropTable("dbo.VendorProductTypes");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.ProductTypeCategories");
            DropTable("dbo.ProductMeasurementTypes");
            DropTable("dbo.SettlementTypes");
            DropTable("dbo.Orders");
            DropTable("dbo.Chefs");
            DropTable("dbo.OrderItems");
            DropTable("dbo.MenuItemPrices");
            DropTable("dbo.InventoryQuotas");
            DropTable("dbo.Users");
            DropTable("dbo.MenuCategories");
            DropTable("dbo.MenuItems");
            DropTable("dbo.MenuItemIngredients");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.InventoryLocations");
            DropTable("dbo.Inventories");
            DropTable("dbo.Sites");
            DropTable("dbo.UserControllerActionPermissions");
            DropTable("dbo.ControllerActions");
        }
    }
}
