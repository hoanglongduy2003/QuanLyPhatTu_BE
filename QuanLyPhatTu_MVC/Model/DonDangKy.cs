using QuanLyPhatTu_MVC.Modal;

namespace QuanLyPhatTu_MVC.Model
{
    public class DonDangKy
    {
        public int DonDangKyID { get; set; }
        public DateTime NgayGuiDon { get; set; }
        public DateTime? NgayXuLy { get; set; }
        public string? NguoiXuLy { get; set; }
        public int? TrangThaiDonID { get; set; }
        public int DaoTrangID { get; set; }
        public string PhatTuID { get; set; }
        public virtual TrangThaiDon? TrangThaiDons { get; set; }
        public virtual DaoTrang? DaoTrangs { get; set; }
        public virtual PhatTu? PhatTus { get; set; }
    }
}
