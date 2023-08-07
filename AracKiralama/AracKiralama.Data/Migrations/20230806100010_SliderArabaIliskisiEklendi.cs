using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AracKiralama.Data.Migrations
{
    /// <inheritdoc />
    public partial class SliderArabaIliskisiEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SliderId",
                table: "Araclar",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2023, 8, 6, 13, 0, 10, 139, DateTimeKind.Local).AddTicks(7766));

            migrationBuilder.CreateIndex(
                name: "IX_Araclar_SliderId",
                table: "Araclar",
                column: "SliderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Araclar_Sliders_SliderId",
                table: "Araclar",
                column: "SliderId",
                principalTable: "Sliders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Araclar_Sliders_SliderId",
                table: "Araclar");

            migrationBuilder.DropIndex(
                name: "IX_Araclar_SliderId",
                table: "Araclar");

            migrationBuilder.DropColumn(
                name: "SliderId",
                table: "Araclar");

            migrationBuilder.UpdateData(
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2023, 8, 4, 11, 27, 36, 922, DateTimeKind.Local).AddTicks(6681));
        }
    }
}
