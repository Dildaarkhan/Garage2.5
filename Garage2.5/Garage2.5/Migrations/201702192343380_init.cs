namespace Garage2._5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckInOuts",
                c => new
                    {
                        CheckIn = c.DateTime(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        CheckOut = c.DateTime(),
                        Price = c.Int(),
                    })
                .PrimaryKey(t => new { t.CheckIn, t.VehicleId });
            
            AddColumn("dbo.Vehicles", "CheckInOut_CheckIn", c => c.DateTime());
            AddColumn("dbo.Vehicles", "CheckInOut_VehicleId", c => c.Int());
            CreateIndex("dbo.Vehicles", new[] { "CheckInOut_CheckIn", "CheckInOut_VehicleId" });
            AddForeignKey("dbo.Vehicles", new[] { "CheckInOut_CheckIn", "CheckInOut_VehicleId" }, "dbo.CheckInOuts", new[] { "CheckIn", "VehicleId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", new[] { "CheckInOut_CheckIn", "CheckInOut_VehicleId" }, "dbo.CheckInOuts");
            DropIndex("dbo.Vehicles", new[] { "CheckInOut_CheckIn", "CheckInOut_VehicleId" });
            DropColumn("dbo.Vehicles", "CheckInOut_VehicleId");
            DropColumn("dbo.Vehicles", "CheckInOut_CheckIn");
            DropTable("dbo.CheckInOuts");
        }
    }
}
