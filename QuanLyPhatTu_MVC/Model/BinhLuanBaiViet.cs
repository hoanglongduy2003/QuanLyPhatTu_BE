using QuanLyPhatTu_MVC.Modal;

namespace QuanLyPhatTu_MVC.Model
{
    public class BinhLuanBaiViet
    {
        public int BinhLuanBaiVietID { get; set; }
        public int BaiVietID { get; set; }
        public string PhatTuID { get; set; }
        public string BinhLuan { get; set; }
        public int SoLuotThich { get; set; }
        public bool DaXoa { get; set; }
        public DateTime? ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }
        public DateTime? ThoiGianXoa { get; set; }
        public virtual PhatTu? PhatTus { get; set; }
        public virtual BaiViet? BaiViets { get; set; }
        public virtual ICollection<NguoiDungThichBinhLuanBaiViet>? NguoiDungThichBinhLuanBaiViets { get; set; }

    }
}
