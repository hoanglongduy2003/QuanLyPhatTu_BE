using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhatTu_MVC.Migrations
{
    public partial class updatev9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "DaXoa",
                table: "NguoiDungThichBinhLuanBaiViet",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "DaXoa",
                table: "BinhLuanBaiViet",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "RefeshToken",
                columns: table => new
                {
                    RefeshTokenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhatTuID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianHetHan = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefeshToken", x => x.RefeshTokenID);
                    table.ForeignKey(
                        name: "FK_RefeshToken_PhatTu_PhatTuID",
                        column: x => x.PhatTuID,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefeshToken_PhatTuID",
                table: "RefeshToken",
                column: "PhatTuID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefeshToken");

            migrationBuilder.AlterColumn<bool>(
                name: "DaXoa",
                table: "NguoiDungThichBinhLuanBaiViet",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "DaXoa",
                table: "BinhLuanBaiViet",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
