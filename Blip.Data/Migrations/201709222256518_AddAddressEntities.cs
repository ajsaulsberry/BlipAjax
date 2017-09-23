namespace Blip.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailAddresses",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        CustomerID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Email)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.PostalAddresses",
                c => new
                    {
                        PostalAddressID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Guid(nullable: false),
                        Iso3 = c.String(maxLength: 3),
                        StreetAddress1 = c.String(maxLength: 100),
                        StreetAddress2 = c.String(maxLength: 100),
                        City = c.String(maxLength: 50),
                        RegionCode = c.String(maxLength: 3),
                        PostalCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.PostalAddressID)
                .ForeignKey("dbo.Countries", t => t.Iso3)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Regions", t => t.RegionCode)
                .Index(t => t.CustomerID)
                .Index(t => t.Iso3)
                .Index(t => t.RegionCode);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostalAddresses", "RegionCode", "dbo.Regions");
            DropForeignKey("dbo.PostalAddresses", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.PostalAddresses", "Iso3", "dbo.Countries");
            DropForeignKey("dbo.EmailAddresses", "CustomerID", "dbo.Customers");
            DropIndex("dbo.PostalAddresses", new[] { "RegionCode" });
            DropIndex("dbo.PostalAddresses", new[] { "Iso3" });
            DropIndex("dbo.PostalAddresses", new[] { "CustomerID" });
            DropIndex("dbo.EmailAddresses", new[] { "CustomerID" });
            DropTable("dbo.PostalAddresses");
            DropTable("dbo.EmailAddresses");
        }
    }
}
