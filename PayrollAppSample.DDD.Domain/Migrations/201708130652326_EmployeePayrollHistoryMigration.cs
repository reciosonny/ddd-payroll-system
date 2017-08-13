namespace PayrollAppSample.DDD.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeePayrollHistoryMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PayrollHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NetIncome = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossIncome = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalDeductions = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PayrollDate = c.DateTime(nullable: false),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .Index(t => t.Employee_Id);
            
            AddColumn("dbo.Employees", "PayrollHistoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Positions", "BaseSalary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Positions", "IncreasePercentage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PayrollHistories", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.PayrollHistories", new[] { "Employee_Id" });
            DropColumn("dbo.Positions", "IncreasePercentage");
            DropColumn("dbo.Positions", "BaseSalary");
            DropColumn("dbo.Employees", "PayrollHistoryId");
            DropTable("dbo.PayrollHistories");
        }
    }
}
