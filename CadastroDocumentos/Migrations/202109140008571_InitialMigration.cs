namespace CadastroDocumentos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Titulo = c.String(nullable: false, unicode: false),
                        Processo = c.String(nullable: false, unicode: false),
                        Categoria = c.String(nullable: false, unicode: false),
                        ArquivoURL = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Documentoes");
        }
    }
}
