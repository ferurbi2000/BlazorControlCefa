using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorControlCefa.Migrations
{
    public partial class IsPrincialVisitPersonDetailEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrincipal",
                schema: "public",
                table: "DetalleVisitaPersona",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrincipal",
                schema: "public",
                table: "DetalleVisitaPersona");
        }
    }
}
