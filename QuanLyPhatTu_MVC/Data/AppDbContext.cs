using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_MVC.Modal;
using QuanLyPhatTu_MVC.Model;

namespace QuanLyPhatTu_MVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //this.ChangeTracker.AutoDetectChangesEnabled = true;
            //this.ChangeTracker.LazyLoadingEnabled = true;
        }
        public DbSet<PhatTu> PhatTu { get; set; }
        public DbSet<QuyenHan> QuyenHan { get; set; }
        public DbSet<XacNhanEmail> XacNhanEmail { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<DaoTrang> DaoTrang { get; set; }
        public DbSet<PhatTuDaoTrang> PhatTuDaoTrang { get; set; }
        public DbSet<DonDangKy> DonDangKy { get; set; }
        public DbSet<TrangThaiDon> TrangThaiDon { get; set; }
        public DbSet<TrangThaiBaiViet> TrangThaiBaiViet { get; set; }
        public DbSet<LoaiBaiViet> LoaiBaiViet { get; set; }
        public DbSet<BaiViet> BaiViet { get; set; }
        public DbSet<BinhLuanBaiViet> BinhLuanBaiViet { get; set; }
        public DbSet<NguoiDungThichBinhLuanBaiViet> NguoiDungThichBinhLuanBaiViet { get; set; }
        public DbSet<NguoiDungThichBaiViet> NguoiDungThichBaiViet { get; set; }
        public DbSet<RefeshToken> RefeshToken { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-F4G8HOR\\SQLEXPRESS; Database = QuanLyPhatTu; Trusted_Connection = True;Encrypt=false;TrustServerCertificate=true;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedPhatTu(builder);
        }
        private void SeedPhatTu(ModelBuilder builder)
        {
            builder.Entity<PhatTu>()
                .HasMany<RefreshToken>(i => i.RefreshTokens)
                .WithOne(i => i.PhatTu)
                .HasForeignKey(i => i.PhatTuID)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<PhatTu>()
                .HasMany<XacNhanEmail>(i => i.XacNhanEmails)
                .WithOne(i => i.PhatTu)
                .HasForeignKey(i => i.PhatTuID)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<PhatTu>()
                .HasOne<QuyenHan>(i => i.QuyenHan)
                .WithMany(i => i.PhatTus)
                .HasForeignKey(i => i.QuyenHanID)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<PhatTu>()
                .HasOne<Chua>(i => i.Chua)
                .WithMany(i => i.PhatTus)
                .HasForeignKey(i => i.ChuaID)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<PhatTu>()
               .HasMany<PhatTuDaoTrang>(i => i.PhatTuDaoTrangs)
               .WithOne(i => i.PhatTus)
               .HasForeignKey(i => i.PhatTuID)
               .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<PhatTu>()
               .HasMany<DonDangKy>(i => i.DonDangKys)
               .WithOne(i => i.PhatTus)
               .HasForeignKey(i => i.PhatTuID)
               .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<PhatTu>()
                .HasMany<BaiViet>(i => i.BaiViets)
                .WithOne(i => i.PhatTus)
                .HasForeignKey(i => i.PhatTuID)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<PhatTu>()
                .HasMany<BinhLuanBaiViet>(i => i.BinhLuanBaiViets)
                .WithOne(i => i.PhatTus)
                .HasForeignKey(i => i.PhatTuID)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<PhatTu>()
                .HasMany<NguoiDungThichBinhLuanBaiViet>(i => i.NguoiDungThichBinhLuanBaiViets)
                .WithOne(i => i.PhatTus)
                .HasForeignKey(i => i.PhatTuID)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<PhatTu>()
                .HasMany<NguoiDungThichBaiViet>(i => i.NguoiDungThichBaiViets)
                .WithOne(i => i.PhatTus)
                .HasForeignKey(i => i.PhatTuID)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder.Entity<PhatTu>()
                .HasMany<RefeshToken>(i => i.RefeshTokens)
                .WithOne(i => i.PhatTus)
                .HasForeignKey(i => i.PhatTuID)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
