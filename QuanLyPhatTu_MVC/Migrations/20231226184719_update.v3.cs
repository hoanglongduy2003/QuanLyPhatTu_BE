using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhatTu_MVC.Migrations
{
    public partial class updatev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonDangKy_TrangThaiDon_TrangThaiDonID",
                table: "DonDangKy");

            migrationBuilder.AlterColumn<int>(
                name: "TrangThaiDonID",
                table: "DonDangKy",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NguoiXuLy",
                table: "DonDangKy",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayXuLy",
                table: "DonDangKy",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_DonDangKy_TrangThaiDon_TrangThaiDonID",
                table: "DonDangKy",
                column: "TrangThaiDonID",
                principalTable: "TrangThaiDon",
                principalColumn: "TrangThaiDonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonDangKy_TrangThaiDon_TrangThaiDonID",
                table: "DonDangKy");

            migrationBuilder.AlterColumn<int>(
                name: "TrangThaiDonID",
                table: "DonDangKy",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NguoiXuLy",
                table: "DonDangKy",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayXuLy",
                table: "DonDangKy",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DonDangKy_TrangThaiDon_TrangThaiDonID",
                table: "DonDangKy",
                column: "TrangThaiDonID",
                principalTable: "TrangThaiDon",
                principalColumn: "TrangThaiDonID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
