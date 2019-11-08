namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Login : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LoginErrorMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LoginErrorMessage");
        }
    }
}
