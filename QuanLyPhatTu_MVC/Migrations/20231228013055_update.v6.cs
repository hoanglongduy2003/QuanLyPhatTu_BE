using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhatTu_MVC.Migrations
{
    public partial class updatev6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BaiVietID",
                table: "LoaiBaiViet",
                newName: "TenLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenLoai",
                table: "LoaiBaiViet",
                newName: "BaiVietID");
        }
    }
}
