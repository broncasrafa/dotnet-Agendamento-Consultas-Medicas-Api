using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSF.AgendamentoConsultas.Infra.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoTabelasdoIdentity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "IdentityUser",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))");
        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "IdentityUser");
        }
    }
}
