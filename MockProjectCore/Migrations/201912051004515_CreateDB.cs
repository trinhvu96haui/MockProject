namespace MockProjectCore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClaimInfors",
                c => new
                    {
                        ClaimInforId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        TotalNoOfHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        ClaimId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClaimInforId)
                .ForeignKey("dbo.ClaimRequests", t => t.ClaimId, cascadeDelete: true)
                .Index(t => t.ClaimId);
            
            CreateTable(
                "dbo.ClaimRequests",
                c => new
                    {
                        ClaimId = c.Int(nullable: false, identity: true),
                        Remark = c.String(),
                        TotalWorkingHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AuditTrail = c.String(),
                        StaffId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClaimId)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        StaffName = c.String(),
                        JobRank = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StaffId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StaffProjectMaps",
                c => new
                    {
                        StaffId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StaffId, t.ProjectId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectCode = c.String(),
                        ProjectName = c.String(),
                        Duration = c.DateTime(),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StaffProjectMaps", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.StaffProjectMaps", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Staffs", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ClaimRequests", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.ClaimInfors", "ClaimId", "dbo.ClaimRequests");
            DropIndex("dbo.StaffProjectMaps", new[] { "ProjectId" });
            DropIndex("dbo.StaffProjectMaps", new[] { "StaffId" });
            DropIndex("dbo.Staffs", new[] { "DepartmentId" });
            DropIndex("dbo.ClaimRequests", new[] { "StaffId" });
            DropIndex("dbo.ClaimInfors", new[] { "ClaimId" });
            DropTable("dbo.Projects");
            DropTable("dbo.StaffProjectMaps");
            DropTable("dbo.Departments");
            DropTable("dbo.Staffs");
            DropTable("dbo.ClaimRequests");
            DropTable("dbo.ClaimInfors");
        }
    }
}
