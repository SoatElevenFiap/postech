using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soat.Eleven.FastFood.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarImagemProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Produtos",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TokensAtendimento",
                table: "TokensAtendimento",
                column: "TokenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TokensAtendimento",
                table: "TokensAtendimento");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Produtos");
        }
    }
}
