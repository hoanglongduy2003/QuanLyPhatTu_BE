using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhatTu_MVC.Migrations
{
    public partial class updatev7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGianThich",
                table: "NguoiDungThichBaiViet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SoLuotThich",
                table: "BinhLuanBaiViet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThoiGianThich",
                table: "NguoiDungThichBaiViet");

            migrationBuilder.DropColumn(
                name: "SoLuotThich",
                table: "BinhLuanBaiViet");
        }
    }
}
