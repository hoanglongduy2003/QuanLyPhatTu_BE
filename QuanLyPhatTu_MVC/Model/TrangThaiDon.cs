using QuanLyPhatTu_MVC.Modal;

namespace QuanLyPhatTu_MVC.Model
{
    public class TrangThaiDon
    {
        public int TrangThaiDonID { get; set; }
        public string TenTrangThai { get; set; }
        public virtual ICollection<DonDangKy>? DonDangKys { get; set; }

    }
}
