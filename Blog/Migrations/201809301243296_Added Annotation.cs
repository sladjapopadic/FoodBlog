namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnnotation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Namirnicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kolicina = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sastojak_Id = c.Int(nullable: false),
                        Recept_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sastojaks", t => t.Sastojak_Id, cascadeDelete: true)
                .ForeignKey("dbo.Recepts", t => t.Recept_Id)
                .Index(t => t.Sastojak_Id)
                .Index(t => t.Recept_Id);
            
            CreateTable(
                "dbo.Sastojaks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 20),
                        ReceptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recepts", t => t.ReceptId, cascadeDelete: true)
                .Index(t => t.ReceptId);
            
            AddColumn("dbo.Recepts", "VremePripreme", c => c.String());
            AddColumn("dbo.Recepts", "BrojPorcija", c => c.Int(nullable: false));
            AlterColumn("dbo.Recepts", "Autor", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Recepts", "Sastojci");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recepts", "Sastojci", c => c.String(nullable: false));
            DropForeignKey("dbo.Namirnicas", "Recept_Id", "dbo.Recepts");
            DropForeignKey("dbo.Namirnicas", "Sastojak_Id", "dbo.Sastojaks");
            DropForeignKey("dbo.Sastojaks", "ReceptId", "dbo.Recepts");
            DropIndex("dbo.Sastojaks", new[] { "ReceptId" });
            DropIndex("dbo.Namirnicas", new[] { "Recept_Id" });
            DropIndex("dbo.Namirnicas", new[] { "Sastojak_Id" });
            AlterColumn("dbo.Recepts", "Autor", c => c.String());
            DropColumn("dbo.Recepts", "BrojPorcija");
            DropColumn("dbo.Recepts", "VremePripreme");
            DropTable("dbo.Sastojaks");
            DropTable("dbo.Namirnicas");
        }
    }
}
