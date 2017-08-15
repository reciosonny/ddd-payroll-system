namespace PayrollAppSample.DDD.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeaveCredits : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Timekeepings", "LeaveCreditYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Timekeepings", "LeaveCreditYear");
        }
    }
}
