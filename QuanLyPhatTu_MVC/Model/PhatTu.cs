using Microsoft.AspNetCore.Identity;
using QuanLyPhatTu_MVC.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_MVC.Modal
{
    public class PhatTu : IdentityUser
    {
        public string? PhatTuID { get; set; }
        public string HoTen { get; set; }
        public string TenTaiKhoan { get; set; }
        public string? AnhChup { get; set; }
        [NotMapped]
        public IFormFile AnhChupFile { get; set; }
        public bool DaHoanTuc { get; set; }
        public string Email { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public DateTime? NgayHoanTuc { get; set; }
        public string MatKhau { get; set; }
        public string PhapDanh { get; set; }
        public string SoDienThoai { get; set; }
        public bool TrangThai { get; set; }
        public int QuyenHanID { get; set; }
        public int ChuaID { get; set; }
        public virtual QuyenHan? QuyenHan { get; set; }
        public virtual Chua? Chua { get; set; }
        public virtual ICollection<XacNhanEmail>? XacNhanEmails { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<PhatTuDaoTrang>? PhatTuDaoTrangs { get; set; }
        public virtual ICollection<DonDangKy>? DonDangKys { get; set; }
        public virtual ICollection<BaiViet>? BaiViets { get; set; }
        public virtual ICollection<BinhLuanBaiViet>? BinhLuanBaiViets { get; set; }
        public virtual ICollection<NguoiDungThichBinhLuanBaiViet>? NguoiDungThichBinhLuanBaiViets { get; set; }
        public virtual ICollection<NguoiDungThichBaiViet>? NguoiDungThichBaiViets { get; set; }
        public virtual ICollection<RefeshToken>? RefeshTokens { get; set; }

    }
}
