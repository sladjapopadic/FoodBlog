namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jczkjc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recepts", "VrstaRecepta", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recepts", "VrstaRecepta");
        }
    }
}
