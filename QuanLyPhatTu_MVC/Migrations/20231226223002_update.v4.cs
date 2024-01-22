using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhatTu_MVC.Migrations
{
    public partial class updatev4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhatTuID",
                table: "PhatTu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "LoaiBaiViet",
                columns: table => new
                {
                    LoaiBaiVietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaiVietID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiBaiViet", x => x.LoaiBaiVietID);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiBaiViet",
                columns: table => new
                {
                    TrangThaiBaiVietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiBaiViet", x => x.TrangThaiBaiVietID);
                });

            migrationBuilder.CreateTable(
                name: "BaiViet",
                columns: table => new
                {
                    BaiVietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiBaiVietID = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhatTuID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NguoiDuyetBaiVietID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuotThich = table.Column<int>(type: "int", nullable: true),
                    SoLuotBinhLuan = table.Column<int>(type: "int", nullable: true),
                    ThoiGianDang = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiGianXoa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: true),
                    TrangThaiBaiVietID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiViet", x => x.BaiVietID);
                    table.ForeignKey(
                        name: "FK_BaiViet_LoaiBaiViet_LoaiBaiVietID",
                        column: x => x.LoaiBaiVietID,
                        principalTable: "LoaiBaiViet",
                        principalColumn: "LoaiBaiVietID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaiViet_PhatTu_PhatTuID",
                        column: x => x.PhatTuID,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaiViet_TrangThaiBaiViet_TrangThaiBaiVietID",
                        column: x => x.TrangThaiBaiVietID,
                        principalTable: "TrangThaiBaiViet",
                        principalColumn: "TrangThaiBaiVietID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuanBaiViet",
                columns: table => new
                {
                    BinhLuanBaiVietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaiVietID = table.Column<int>(type: "int", nullable: false),
                    PhatTuID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BinhLuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiGianXoa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuanBaiViet", x => x.BinhLuanBaiVietID);
                    table.ForeignKey(
                        name: "FK_BinhLuanBaiViet_BaiViet_BaiVietID",
                        column: x => x.BaiVietID,
                        principalTable: "BaiViet",
                        principalColumn: "BaiVietID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BinhLuanBaiViet_PhatTu_PhatTuID",
                        column: x => x.PhatTuID,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungThichBaiViet",
                columns: table => new
                {
                    NguoiDungThichBaiVietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhatTuID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BaiVietID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungThichBaiViet", x => x.NguoiDungThichBaiVietID);
                    table.ForeignKey(
                        name: "FK_NguoiDungThichBaiViet_BaiViet_BaiVietID",
                        column: x => x.BaiVietID,
                        principalTable: "BaiViet",
                        principalColumn: "BaiVietID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NguoiDungThichBaiViet_PhatTu_PhatTuID",
                        column: x => x.PhatTuID,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungThichBinhLuanBaiViet",
                columns: table => new
                {
                    NguoiDungThichBinhLuanBaiVietID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhatTuID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BinhLuanBaiVietID = table.Column<int>(type: "int", nullable: false),
                    ThoiGianLike = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungThichBinhLuanBaiViet", x => x.NguoiDungThichBinhLuanBaiVietID);
                    table.ForeignKey(
                        name: "FK_NguoiDungThichBinhLuanBaiViet_BinhLuanBaiViet_BinhLuanBaiVietID",
                        column: x => x.BinhLuanBaiVietID,
                        principalTable: "BinhLuanBaiViet",
                        principalColumn: "BinhLuanBaiVietID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NguoiDungThichBinhLuanBaiViet_PhatTu_PhatTuID",
                        column: x => x.PhatTuID,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaiViet_LoaiBaiVietID",
                table: "BaiViet",
                column: "LoaiBaiVietID");

            migrationBuilder.CreateIndex(
                name: "IX_BaiViet_PhatTuID",
                table: "BaiViet",
                column: "PhatTuID");

            migrationBuilder.CreateIndex(
                name: "IX_BaiViet_TrangThaiBaiVietID",
                table: "BaiViet",
                column: "TrangThaiBaiVietID");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanBaiViet_BaiVietID",
                table: "BinhLuanBaiViet",
                column: "BaiVietID");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanBaiViet_PhatTuID",
                table: "BinhLuanBaiViet",
                column: "PhatTuID");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungThichBaiViet_BaiVietID",
                table: "NguoiDungThichBaiViet",
                column: "BaiVietID");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungThichBaiViet_PhatTuID",
                table: "NguoiDungThichBaiViet",
                column: "PhatTuID");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungThichBinhLuanBaiViet_BinhLuanBaiVietID",
                table: "NguoiDungThichBinhLuanBaiViet",
                column: "BinhLuanBaiVietID");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungThichBinhLuanBaiViet_PhatTuID",
                table: "NguoiDungThichBinhLuanBaiViet",
                column: "PhatTuID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NguoiDungThichBaiViet");

            migrationBuilder.DropTable(
                name: "NguoiDungThichBinhLuanBaiViet");

            migrationBuilder.DropTable(
                name: "BinhLuanBaiViet");

            migrationBuilder.DropTable(
                name: "BaiViet");

            migrationBuilder.DropTable(
                name: "LoaiBaiViet");

            migrationBuilder.DropTable(
                name: "TrangThaiBaiViet");

            migrationBuilder.AlterColumn<string>(
                name: "PhatTuID",
                table: "PhatTu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
