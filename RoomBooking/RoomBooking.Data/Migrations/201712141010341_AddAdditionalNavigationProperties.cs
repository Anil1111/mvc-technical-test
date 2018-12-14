namespace RoomBooking.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdditionalNavigationProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "Site_Id", "dbo.Sites");
            DropIndex("dbo.Rooms", new[] { "Site_Id" });
            RenameColumn(table: "dbo.Bookings", name: "RoomId_Id", newName: "RoomId");
            RenameColumn(table: "dbo.Rooms", name: "Site_Id", newName: "SiteId");
            RenameIndex(table: "dbo.Bookings", name: "IX_RoomId_Id", newName: "IX_RoomId");
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AlterColumn("dbo.Rooms", "SiteId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Rooms", "SiteId");
            AddForeignKey("dbo.Rooms", "SiteId", "dbo.Sites", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "SiteId", "dbo.Sites");
            DropIndex("dbo.Rooms", new[] { "SiteId" });
            AlterColumn("dbo.Rooms", "SiteId", c => c.Guid());
            DropColumn("dbo.AspNetUsers", "Name");
            RenameIndex(table: "dbo.Bookings", name: "IX_RoomId", newName: "IX_RoomId_Id");
            RenameColumn(table: "dbo.Rooms", name: "SiteId", newName: "Site_Id");
            RenameColumn(table: "dbo.Bookings", name: "RoomId", newName: "RoomId_Id");
            CreateIndex("dbo.Rooms", "Site_Id");
            AddForeignKey("dbo.Rooms", "Site_Id", "dbo.Sites", "Id");
        }
    }
}
