namespace PayrollAppSample.DDD.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimekeepingEmployee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PayrollHistories", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.PayrollHistories", new[] { "Employee_Id" });
            RenameColumn(table: "dbo.PayrollHistories", name: "Employee_Id", newName: "EmployeeId");
            CreateTable(
                "dbo.Timekeepings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Entry = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        TimeIn = c.DateTime(nullable: false),
                        TimeOut = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        IsPresent = c.Boolean(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            AddColumn("dbo.Employees", "CurrentSalary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Employees", "EmploymentStatus", c => c.String());
            AddColumn("dbo.Employees", "PayrollIncluded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "TimekeepingId", c => c.Int(nullable: false));
            AlterColumn("dbo.PayrollHistories", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.PayrollHistories", "EmployeeId");
            AddForeignKey("dbo.PayrollHistories", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PayrollHistories", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Timekeepings", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Timekeepings", new[] { "EmployeeId" });
            DropIndex("dbo.PayrollHistories", new[] { "EmployeeId" });
            AlterColumn("dbo.PayrollHistories", "EmployeeId", c => c.Int());
            DropColumn("dbo.Employees", "TimekeepingId");
            DropColumn("dbo.Employees", "PayrollIncluded");
            DropColumn("dbo.Employees", "EmploymentStatus");
            DropColumn("dbo.Employees", "CurrentSalary");
            DropTable("dbo.Timekeepings");
            RenameColumn(table: "dbo.PayrollHistories", name: "EmployeeId", newName: "Employee_Id");
            CreateIndex("dbo.PayrollHistories", "Employee_Id");
            AddForeignKey("dbo.PayrollHistories", "Employee_Id", "dbo.Employees", "Id");
        }
    }
}
