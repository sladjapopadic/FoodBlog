namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsfs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Namirnicas", "Sastojak_Id", "dbo.Sastojaks");
            DropForeignKey("dbo.Namirnicas", "Recept_Id", "dbo.Recepts");
            DropIndex("dbo.Namirnicas", new[] { "Sastojak_Id" });
            DropIndex("dbo.Namirnicas", new[] { "Recept_Id" });
            DropTable("dbo.Namirnicas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Namirnicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kolicina = c.String(nullable: false),
                        Sastojak_Id = c.Int(nullable: false),
                        Recept_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Namirnicas", "Recept_Id");
            CreateIndex("dbo.Namirnicas", "Sastojak_Id");
            AddForeignKey("dbo.Namirnicas", "Recept_Id", "dbo.Recepts", "Id");
            AddForeignKey("dbo.Namirnicas", "Sastojak_Id", "dbo.Sastojaks", "Id", cascadeDelete: true);
        }
    }
}
