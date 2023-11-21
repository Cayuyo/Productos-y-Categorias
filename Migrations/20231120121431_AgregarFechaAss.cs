using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Productos_y_Categorias.Migrations
{
    public partial class AgregarFechaAss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Creacion",
                table: "Asociaciones",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Modificacion",
                table: "Asociaciones",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha_Creacion",
                table: "Asociaciones");

            migrationBuilder.DropColumn(
                name: "Fecha_Modificacion",
                table: "Asociaciones");
        }
    }
}
