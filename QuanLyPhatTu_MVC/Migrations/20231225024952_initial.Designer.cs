﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuanLyPhatTu_MVC.Data;

#nullable disable

namespace QuanLyPhatTu_MVC.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231225024952_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QuanLyPhatTu_MVC.Modal.Chua", b =>
                {
                    b.Property<int>("ChuaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChuaID"), 1L, 1);

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayThanhLap")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiTruTri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenChua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChuaID");

                    b.ToTable("Chua");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Modal.PhatTu", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AnhChup")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChuaID")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DaHoanTuc")
                        .HasColumnType("bit");

                    b.Property<int>("DaoTrangsDaoTrangID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("GioiTinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayHoanTuc")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhapDanh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhatTuID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("QuyenHanID")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenTaiKhoan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChuaID");

                    b.HasIndex("DaoTrangsDaoTrangID");

                    b.HasIndex("QuyenHanID");

                    b.ToTable("PhatTu");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Modal.QuyenHan", b =>
                {
                    b.Property<int>("QuyenHanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuyenHanID"), 1L, 1);

                    b.Property<string>("TenQuyenHan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuyenHanID");

                    b.ToTable("QuyenHan");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Modal.RefreshToken", b =>
                {
                    b.Property<int>("RefreshTokenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefreshTokenID"), 1L, 1);

                    b.Property<string>("PhatTuID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ThoiGianHetHan")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RefreshTokenID");

                    b.HasIndex("PhatTuID");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Modal.XacNhanEmail", b =>
                {
                    b.Property<int>("XacNhanEmailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("XacNhanEmailID"), 1L, 1);

                    b.Property<bool>("DaXacNhan")
                        .HasColumnType("bit");

                    b.Property<string>("MaXacNhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhatTuID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ThoiGianHetHan")
                        .HasColumnType("datetime2");

                    b.HasKey("XacNhanEmailID");

                    b.HasIndex("PhatTuID");

                    b.ToTable("XacNhanEmail");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Model.DaoTrang", b =>
                {
                    b.Property<int>("DaoTrangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DaoTrangID"), 1L, 1);

                    b.Property<bool>("DaKetThuc")
                        .HasColumnType("bit");

                    b.Property<string>("NguoiTruTri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NoiToChuc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoThanhVienThamGia")
                        .HasColumnType("int");

                    b.Property<DateTime>("ThoiGianBatDau")
                        .HasColumnType("datetime2");

                    b.HasKey("DaoTrangID");

                    b.ToTable("DaoTrang");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Model.DonDangKy", b =>
                {
                    b.Property<int>("DonDangKyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DonDangKyID"), 1L, 1);

                    b.Property<int>("DaoTrangID")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayGuiDon")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayXuLy")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiXuLy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhatTuID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TrangThaiDonID")
                        .HasColumnType("int");

                    b.HasKey("DonDangKyID");

                    b.HasIndex("DaoTrangID");

                    b.HasIndex("PhatTuID");

                    b.HasIndex("TrangThaiDonID");

                    b.ToTable("DonDangKy");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Model.PhatTuDaoTrang", b =>
                {
                    b.Property<int>("PhatTuDaoTrangID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhatTuDaoTrangID"), 1L, 1);

                    b.Property<bool>("DaThamGia")
                        .HasColumnType("bit");

                    b.Property<int>("DaoTrangID")
                        .HasColumnType("int");

                    b.Property<string>("LiDoKhongThamGia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhatTuID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PhatTuDaoTrangID");

                    b.HasIndex("DaoTrangID");

                    b.HasIndex("PhatTuID");

                    b.ToTable("PhatTuDaoTrang");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Model.TrangThaiDon", b =>
                {
                    b.Property<int>("TrangThaiDonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrangThaiDonID"), 1L, 1);

                    b.Property<string>("TenTrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrangThaiDonID");

                    b.ToTable("TrangThaiDon");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Modal.PhatTu", b =>
                {
                    b.HasOne("QuanLyPhatTu_MVC.Modal.Chua", "Chua")
                        .WithMany("PhatTus")
                        .HasForeignKey("ChuaID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("QuanLyPhatTu_MVC.Model.DaoTrang", "DaoTrangs")
                        .WithMany()
                        .HasForeignKey("DaoTrangsDaoTrangID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyPhatTu_MVC.Modal.QuyenHan", "QuyenHan")
                        .WithMany("PhatTus")
                        .HasForeignKey("QuyenHanID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Chua");

                    b.Navigation("DaoTrangs");

                    b.Navigation("QuyenHan");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Modal.RefreshToken", b =>
                {
                    b.HasOne("QuanLyPhatTu_MVC.Modal.PhatTu", "PhatTu")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("PhatTuID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("PhatTu");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Modal.XacNhanEmail", b =>
                {
                    b.HasOne("QuanLyPhatTu_MVC.Modal.PhatTu", "PhatTu")
                        .WithMany("XacNhanEmails")
                        .HasForeignKey("PhatTuID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("PhatTu");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Model.DonDangKy", b =>
                {
                    b.HasOne("QuanLyPhatTu_MVC.Model.DaoTrang", "DaoTrangs")
                        .WithMany()
                        .HasForeignKey("DaoTrangID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyPhatTu_MVC.Modal.PhatTu", "PhatTus")
                        .WithMany("DonDangKys")
                        .HasForeignKey("PhatTuID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("QuanLyPhatTu_MVC.Model.TrangThaiDon", "TrangThaiDons")
                        .WithMany()
                        .HasForeignKey("TrangThaiDonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DaoTrangs");

                    b.Navigation("PhatTus");

                    b.Navigation("TrangThaiDons");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Model.PhatTuDaoTrang", b =>
                {
                    b.HasOne("QuanLyPhatTu_MVC.Model.DaoTrang", "DaoTrangs")
                        .WithMany("PhatTuDaoTrangs")
                        .HasForeignKey("DaoTrangID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuanLyPhatTu_MVC.Modal.PhatTu", "PhatTus")
                        .WithMany("PhatTuDaoTrangs")
                        .HasForeignKey("PhatTuID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("DaoTrangs");

                    b.Navigation("PhatTus");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Modal.Chua", b =>
                {
                    b.Navigation("PhatTus");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Modal.PhatTu", b =>
                {
                    b.Navigation("DonDangKys");

                    b.Navigation("PhatTuDaoTrangs");

                    b.Navigation("RefreshTokens");

                    b.Navigation("XacNhanEmails");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Modal.QuyenHan", b =>
                {
                    b.Navigation("PhatTus");
                });

            modelBuilder.Entity("QuanLyPhatTu_MVC.Model.DaoTrang", b =>
                {
                    b.Navigation("PhatTuDaoTrangs");
                });
#pragma warning restore 612, 618
        }
    }
}
