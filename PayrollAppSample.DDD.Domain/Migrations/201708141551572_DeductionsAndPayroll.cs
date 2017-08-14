namespace PayrollAppSample.DDD.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeductionsAndPayroll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeIncludedDeductions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        EmployeeDeductionsId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeDeductions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FixedAmtDeduction = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PercentageAmtDeduction = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsFixedAmt = c.Boolean(nullable: false),
                        EmployeeIncludedDeductions_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeIncludedDeductions", t => t.EmployeeIncludedDeductions_Id)
                .Index(t => t.EmployeeIncludedDeductions_Id);
            
            CreateTable(
                "dbo.PayrollEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PayrollDate = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        GrossPay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deductions = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetPay = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Employees", "DateHired", c => c.DateTime());
            AddColumn("dbo.Employees", "EmployeeIncludedDeductionsId", c => c.Int());
            AddColumn("dbo.Employees", "PayrollEntry_Id", c => c.Int());
            CreateIndex("dbo.Employees", "PayrollEntry_Id");
            AddForeignKey("dbo.Employees", "PayrollEntry_Id", "dbo.PayrollEntries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "PayrollEntry_Id", "dbo.PayrollEntries");
            DropForeignKey("dbo.EmployeeDeductions", "EmployeeIncludedDeductions_Id", "dbo.EmployeeIncludedDeductions");
            DropForeignKey("dbo.EmployeeIncludedDeductions", "Id", "dbo.Employees");
            DropIndex("dbo.EmployeeDeductions", new[] { "EmployeeIncludedDeductions_Id" });
            DropIndex("dbo.Employees", new[] { "PayrollEntry_Id" });
            DropIndex("dbo.EmployeeIncludedDeductions", new[] { "Id" });
            DropColumn("dbo.Employees", "PayrollEntry_Id");
            DropColumn("dbo.Employees", "EmployeeIncludedDeductionsId");
            DropColumn("dbo.Employees", "DateHired");
            DropTable("dbo.PayrollEntries");
            DropTable("dbo.EmployeeDeductions");
            DropTable("dbo.EmployeeIncludedDeductions");
        }
    }
}
