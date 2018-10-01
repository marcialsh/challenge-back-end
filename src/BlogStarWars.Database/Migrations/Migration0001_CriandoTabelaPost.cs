using FluentMigrator;

namespace BlogStarWars.Database.Migrations
{
    [Migration(1)]
    public class Migration0001_CriandoTabelaPost : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Create
                .Table("Post")
                    .WithColumn("Id")
                        .AsInt64()
                        .PrimaryKey()
                        .NotNullable()
                        .Unique()
                    .WithColumn("Titulo")
                        .AsString(100)
                        .NotNullable()
                    .WithColumn("Descricao")
                        .AsString(350)
                        .NotNullable()
                    .WithColumn("Conteudo")
                        .AsString(50000)
                        .NotNullable()
                    .WithColumn("QuantidadeLikes")
                        .AsInt16()
                        .NotNullable()
                    .WithColumn("QuantidadeViews")
                        .AsInt16()
                        .NotNullable()
                    .WithColumn("EstaDeletado")
                        .AsBoolean()
                        .Nullable();
            
            Create
                .Table("Auditoria")
                    .WithColumn("Id")
                        .AsInt64()
                        .PrimaryKey()
                        .NotNullable()
                    .WithColumn("Acao")
                        .AsString(50)
                        .NotNullable()
                    .WithColumn("Data")
                        .AsDateTime()
                        .NotNullable()
                    .WithColumn("Ip")
                        .AsString(32)
                        .NotNullable();
        }
    }
}