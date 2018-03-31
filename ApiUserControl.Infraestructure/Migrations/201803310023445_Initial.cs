namespace ApiUserControl.Infraestructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false, maxLength: 160),
                        Password = c.String(maxLength: 32, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true, name: "IX_EMAIL");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", "IX_EMAIL");
            DropTable("dbo.User");
        }
    }
}
