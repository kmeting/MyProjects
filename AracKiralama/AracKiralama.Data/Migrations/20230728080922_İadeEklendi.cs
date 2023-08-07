using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AracKiralama.Data.Migrations
{
    /// <inheritdoc />
    public partial class İadeEklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musteriler_Araclar_AracId",
                table: "Musteriler");

            migrationBuilder.AddColumn<DateTime>(
                name: "İadeTarihi",
                table: "Satislar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "AracId",
                table: "Musteriler",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2023, 7, 28, 11, 9, 22, 466, DateTimeKind.Local).AddTicks(569));

            migrationBuilder.AddForeignKey(
                name: "FK_Musteriler_Araclar_AracId",
                table: "Musteriler",
                column: "AracId",
                principalTable: "Araclar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musteriler_Araclar_AracId",
                table: "Musteriler");

            migrationBuilder.DropColumn(
                name: "İadeTarihi",
                table: "Satislar");

            migrationBuilder.AlterColumn<int>(
                name: "AracId",
                table: "Musteriler",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Kullanicilar",
                keyColumn: "Id",
                keyValue: 1,
                column: "EklenmeTarihi",
                value: new DateTime(2023, 7, 28, 9, 45, 36, 294, DateTimeKind.Local).AddTicks(3513));

            migrationBuilder.AddForeignKey(
                name: "FK_Musteriler_Araclar_AracId",
                table: "Musteriler",
                column: "AracId",
                principalTable: "Araclar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
