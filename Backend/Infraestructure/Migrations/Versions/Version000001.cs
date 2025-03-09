using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Migrations.Versions
{
    [Migration(DatabaseVersion.TABELA_PRODUTO)]
    public class Version000001 : Migration
    {
        public override void Up()
        {
            Create.Table("Produtos")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Nome").AsString(100).NotNullable()
                .WithColumn("Descricao").AsString().Nullable()
                .WithColumn("Preco").AsDecimal().NotNullable()
                .WithColumn("DataCadastro").AsCustom("timestamp with time zone").NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Produtos");
        }
    }
}
