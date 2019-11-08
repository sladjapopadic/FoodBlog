namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dklsajds : DbMigration
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
            
            DropColumn("dbo.Sastojaks", "ReceptId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sastojaks", "ReceptId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Namirnicas", "Recept_Id", "dbo.Recepts");
            DropForeignKey("dbo.Namirnicas", "Sastojak_Id", "dbo.Sastojaks");
            DropIndex("dbo.Namirnicas", new[] { "Recept_Id" });
            DropIndex("dbo.Namirnicas", new[] { "Sastojak_Id" });
            DropTable("dbo.Namirnicas");
        }
    }
}
