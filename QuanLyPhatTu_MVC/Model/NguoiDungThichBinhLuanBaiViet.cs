using QuanLyPhatTu_MVC.Modal;

namespace QuanLyPhatTu_MVC.Model
{
    public class NguoiDungThichBinhLuanBaiViet
    {
        public int NguoiDungThichBinhLuanBaiVietID { get; set; }
        public string PhatTuID { get; set; }
        public int BinhLuanBaiVietID { get; set; }
        public DateTime? ThoiGianLike { get; set; }
        public bool DaXoa { get; set; }
        public virtual PhatTu? PhatTus { get; set; }
        public virtual BinhLuanBaiViet? BinhLuanBaiViets { get; set; }

    }
}
