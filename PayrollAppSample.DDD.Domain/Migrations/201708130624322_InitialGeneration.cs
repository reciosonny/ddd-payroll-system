namespace PayrollAppSample.DDD.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialGeneration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fname = c.String(),
                        Mname = c.String(),
                        Lname = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
