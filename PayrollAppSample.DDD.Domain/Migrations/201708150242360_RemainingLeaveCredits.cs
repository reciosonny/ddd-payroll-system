namespace PayrollAppSample.DDD.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemainingLeaveCredits : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "RemainingLeaveCredits", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "RemainingLeaveCredits");
        }
    }
}
