namespace QuanLyPhatTu_MVC.Model
{
    public class DaoTrang
    {
        public int DaoTrangID { get; set; }
        public bool DaKetThuc { get; set; }
        public string NoiDung { get; set; }
        public string NoiToChuc { get; set; }
        public int SoThanhVienThamGia { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public string NguoiTruTri { get; set; }
        public virtual ICollection<PhatTuDaoTrang>? PhatTuDaoTrangs { get; set; }
    }
}
