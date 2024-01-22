namespace QuanLyPhatTu_MVC.Modal
{
    public class Chua
    {
        public int ChuaID { get; set; }
        public string TenChua { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayThanhLap { get; set; }
        public string NguoiTruTri { get; set; }
        public virtual ICollection<PhatTu> PhatTus { get; set; }

    }
}
