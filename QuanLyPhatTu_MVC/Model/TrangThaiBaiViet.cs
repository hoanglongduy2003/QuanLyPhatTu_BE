namespace QuanLyPhatTu_MVC.Model
{
    public class TrangThaiBaiViet
    {
        public int TrangThaiBaiVietID { get; set; }
        public string TenTrangThai { get; set; }
        public virtual ICollection<BaiViet>? BaiViets { get; set; }

    }
}
