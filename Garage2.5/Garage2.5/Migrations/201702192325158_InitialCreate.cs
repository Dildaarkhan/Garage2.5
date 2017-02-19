namespace Garage2._5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(),
                        Email = c.String(nullable: false),
                        Telephone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistrationNumber = c.String(nullable: false),
                        Color = c.String(nullable: false, maxLength: 30),
                        Make = c.String(nullable: false, maxLength: 30),
                        Model = c.String(nullable: false, maxLength: 30),
                        Wheels = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        MemberId = c.Int(nullable: false),
                        VehicleTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.VehicleTypeId);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "MemberId", "dbo.Members");
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeId" });
            DropIndex("dbo.Vehicles", new[] { "MemberId" });
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Members");
        }
    }
}
