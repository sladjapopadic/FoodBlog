namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjfkshdf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sastojaks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 20),
                        ReceptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sastojaks");
        }
    }
}
