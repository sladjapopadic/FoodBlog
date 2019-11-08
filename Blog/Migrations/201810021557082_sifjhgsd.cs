namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sifjhgsd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recepts", "Sastojci", c => c.String(nullable: false));
            DropTable("dbo.Sastojaks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sastojaks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Recepts", "Sastojci");
        }
    }
}
