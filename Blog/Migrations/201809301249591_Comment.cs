namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sastojaks", "ReceptId", "dbo.Recepts");
            DropForeignKey("dbo.Namirnicas", "Sastojak_Id", "dbo.Sastojaks");
            DropForeignKey("dbo.Namirnicas", "Recept_Id", "dbo.Recepts");
            DropIndex("dbo.Namirnicas", new[] { "Sastojak_Id" });
            DropIndex("dbo.Namirnicas", new[] { "Recept_Id" });
            DropIndex("dbo.Sastojaks", new[] { "ReceptId" });
            DropTable("dbo.Namirnicas");
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
                        ReceptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Namirnicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kolicina = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sastojak_Id = c.Int(nullable: false),
                        Recept_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Sastojaks", "ReceptId");
            CreateIndex("dbo.Namirnicas", "Recept_Id");
            CreateIndex("dbo.Namirnicas", "Sastojak_Id");
            AddForeignKey("dbo.Namirnicas", "Recept_Id", "dbo.Recepts", "Id");
            AddForeignKey("dbo.Namirnicas", "Sastojak_Id", "dbo.Sastojaks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sastojaks", "ReceptId", "dbo.Recepts", "Id", cascadeDelete: true);
        }
    }
}
