using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhatTu_MVC.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chua",
                columns: table => new
                {
                    ChuaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTruTri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chua", x => x.ChuaID);
                });

            migrationBuilder.CreateTable(
                name: "DaoTrang",
                columns: table => new
                {
                    DaoTrangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaKetThuc = table.Column<bool>(type: "bit", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiToChuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoThanhVienThamGia = table.Column<int>(type: "int", nullable: false),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTruTri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaoTrang", x => x.DaoTrangID);
                });

            migrationBuilder.CreateTable(
                name: "QuyenHan",
                columns: table => new
                {
                    QuyenHanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuyenHan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyenHan", x => x.QuyenHanID);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiDon",
                columns: table => new
                {
                    TrangThaiDonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiDon", x => x.TrangThaiDonID);
                });

            migrationBuilder.CreateTable(
                name: "PhatTu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhatTuID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnhChup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaHoanTuc = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayHoanTuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhapDanh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    QuyenHanID = table.Column<int>(type: "int", nullable: false),
                    ChuaID = table.Column<int>(type: "int", nullable: false),
                    DaoTrangsDaoTrangID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhatTu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhatTu_Chua_ChuaID",
                        column: x => x.ChuaID,
                        principalTable: "Chua",
                        principalColumn: "ChuaID");
                    table.ForeignKey(
                        name: "FK_PhatTu_DaoTrang_DaoTrangsDaoTrangID",
                        column: x => x.DaoTrangsDaoTrangID,
                        principalTable: "DaoTrang",
                        principalColumn: "DaoTrangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhatTu_QuyenHan_QuyenHanID",
                        column: x => x.QuyenHanID,
                        principalTable: "QuyenHan",
                        principalColumn: "QuyenHanID");
                });

            migrationBuilder.CreateTable(
                name: "DonDangKy",
                columns: table => new
                {
                    DonDangKyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayGuiDon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayXuLy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiXuLy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThaiDonID = table.Column<int>(type: "int", nullable: false),
                    DaoTrangID = table.Column<int>(type: "int", nullable: false),
                    PhatTuID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonDangKy", x => x.DonDangKyID);
                    table.ForeignKey(
                        name: "FK_DonDangKy_DaoTrang_DaoTrangID",
                        column: x => x.DaoTrangID,
                        principalTable: "DaoTrang",
                        principalColumn: "DaoTrangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonDangKy_PhatTu_PhatTuID",
                        column: x => x.PhatTuID,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DonDangKy_TrangThaiDon_TrangThaiDonID",
                        column: x => x.TrangThaiDonID,
                        principalTable: "TrangThaiDon",
                        principalColumn: "TrangThaiDonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhatTuDaoTrang",
                columns: table => new
                {
                    PhatTuDaoTrangID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaThamGia = table.Column<bool>(type: "bit", nullable: false),
                    LiDoKhongThamGia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaoTrangID = table.Column<int>(type: "int", nullable: false),
                    PhatTuID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhatTuDaoTrang", x => x.PhatTuDaoTrangID);
                    table.ForeignKey(
                        name: "FK_PhatTuDaoTrang_DaoTrang_DaoTrangID",
                        column: x => x.DaoTrangID,
                        principalTable: "DaoTrang",
                        principalColumn: "DaoTrangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhatTuDaoTrang_PhatTu_PhatTuID",
                        column: x => x.PhatTuID,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    RefreshTokenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianHetHan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhatTuID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.RefreshTokenID);
                    table.ForeignKey(
                        name: "FK_RefreshToken_PhatTu_PhatTuID",
                        column: x => x.PhatTuID,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "XacNhanEmail",
                columns: table => new
                {
                    XacNhanEmailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhatTuID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThoiGianHetHan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaXacNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaXacNhan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XacNhanEmail", x => x.XacNhanEmailID);
                    table.ForeignKey(
                        name: "FK_XacNhanEmail_PhatTu_PhatTuID",
                        column: x => x.PhatTuID,
                        principalTable: "PhatTu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonDangKy_DaoTrangID",
                table: "DonDangKy",
                column: "DaoTrangID");

            migrationBuilder.CreateIndex(
                name: "IX_DonDangKy_PhatTuID",
                table: "DonDangKy",
                column: "PhatTuID");

            migrationBuilder.CreateIndex(
                name: "IX_DonDangKy_TrangThaiDonID",
                table: "DonDangKy",
                column: "TrangThaiDonID");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTu_ChuaID",
                table: "PhatTu",
                column: "ChuaID");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTu_DaoTrangsDaoTrangID",
                table: "PhatTu",
                column: "DaoTrangsDaoTrangID");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTu_QuyenHanID",
                table: "PhatTu",
                column: "QuyenHanID");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTuDaoTrang_DaoTrangID",
                table: "PhatTuDaoTrang",
                column: "DaoTrangID");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTuDaoTrang_PhatTuID",
                table: "PhatTuDaoTrang",
                column: "PhatTuID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_PhatTuID",
                table: "RefreshToken",
                column: "PhatTuID");

            migrationBuilder.CreateIndex(
                name: "IX_XacNhanEmail_PhatTuID",
                table: "XacNhanEmail",
                column: "PhatTuID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonDangKy");

            migrationBuilder.DropTable(
                name: "PhatTuDaoTrang");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "XacNhanEmail");

            migrationBuilder.DropTable(
                name: "TrangThaiDon");

            migrationBuilder.DropTable(
                name: "PhatTu");

            migrationBuilder.DropTable(
                name: "Chua");

            migrationBuilder.DropTable(
                name: "DaoTrang");

            migrationBuilder.DropTable(
                name: "QuyenHan");
        }
    }
}
