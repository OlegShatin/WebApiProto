namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednumtoegetask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExamTask", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExamTask", "Number");
        }
    }
}
