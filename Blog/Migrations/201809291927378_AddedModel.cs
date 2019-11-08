namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recepts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 100),
                        Sastojci = c.String(nullable: false),
                        Priprema = c.String(nullable: false),
                        Autor = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recepts");
        }
    }
}
