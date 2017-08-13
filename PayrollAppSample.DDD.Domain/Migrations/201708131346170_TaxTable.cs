namespace PayrollAppSample.DDD.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaxTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaxTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaxSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeductionPercentage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaxTable");
        }
    }
}
