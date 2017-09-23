namespace Blip.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressTypes",
                c => new
                    {
                        AddressTypeID = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.AddressTypeID);
        }
        
        public override void Down()
        {
            DropTable("dbo.AddressTypes");
        }
    }
}
