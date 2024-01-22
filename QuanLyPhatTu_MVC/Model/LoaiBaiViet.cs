using QuanLyPhatTu_MVC.Modal;

namespace QuanLyPhatTu_MVC.Model
{
    public class LoaiBaiViet
    {
        public int LoaiBaiVietID { get; set; }
        public string? TenLoai { get; set; }
        public virtual ICollection<BaiViet>? BaiViets { get; set; }

    }
}
