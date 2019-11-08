namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddsdsd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Namirnicas", "Kolicina", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Namirnicas", "Kolicina", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
