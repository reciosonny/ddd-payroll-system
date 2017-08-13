namespace PayrollAppSample.DDD.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "PositionId" });
            AlterColumn("dbo.Employees", "DepartmentId", c => c.Int());
            AlterColumn("dbo.Employees", "PositionId", c => c.Int());
            AlterColumn("dbo.Employees", "PayrollHistoryId", c => c.Int());
            AlterColumn("dbo.Employees", "TimekeepingId", c => c.Int());
            CreateIndex("dbo.Employees", "DepartmentId");
            CreateIndex("dbo.Employees", "PositionId");
            AddForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments", "Id");
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Employees", new[] { "PositionId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            AlterColumn("dbo.Employees", "TimekeepingId", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "PayrollHistoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "PositionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "PositionId");
            CreateIndex("dbo.Employees", "DepartmentId");
            AddForeignKey("dbo.Employees", "PositionId", "dbo.Positions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments", "Id", cascadeDelete: true);
        }
    }
}
