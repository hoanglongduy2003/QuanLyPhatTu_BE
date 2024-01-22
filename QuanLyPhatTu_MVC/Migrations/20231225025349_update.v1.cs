using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhatTu_MVC.Migrations
{
    public partial class updatev1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhatTu_DaoTrang_DaoTrangsDaoTrangID",
                table: "PhatTu");

            migrationBuilder.DropIndex(
                name: "IX_PhatTu_DaoTrangsDaoTrangID",
                table: "PhatTu");

            migrationBuilder.DropColumn(
                name: "DaoTrangsDaoTrangID",
                table: "PhatTu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DaoTrangsDaoTrangID",
                table: "PhatTu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhatTu_DaoTrangsDaoTrangID",
                table: "PhatTu",
                column: "DaoTrangsDaoTrangID");

            migrationBuilder.AddForeignKey(
                name: "FK_PhatTu_DaoTrang_DaoTrangsDaoTrangID",
                table: "PhatTu",
                column: "DaoTrangsDaoTrangID",
                principalTable: "DaoTrang",
                principalColumn: "DaoTrangID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
