namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changessss : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recepts", "Autor", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recepts", "Autor", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
