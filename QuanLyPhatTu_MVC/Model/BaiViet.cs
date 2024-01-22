using QuanLyPhatTu_MVC.Modal;

namespace QuanLyPhatTu_MVC.Model
{
    public class BaiViet
    {
        public int BaiVietID { get; set; }
        public int LoaiBaiVietID { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public string PhatTuID { get; set; }
        public string? NguoiDuyetBaiVietID { get; set; }
        public int? SoLuotThich { get; set; }
        public int? SoLuotBinhLuan { get; set; }
        public DateTime? ThoiGianDang { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }
        public DateTime? ThoiGianXoa { get; set; }
        public bool? DaXoa { get; set; }
        public int TrangThaiBaiVietID { get; set; }
        public virtual PhatTu? PhatTus { get; set; }
        public virtual LoaiBaiViet? LoaiBaiViets { get; set; }
        public virtual TrangThaiBaiViet? TrangThaiBaiViets { get; set; }
        public virtual ICollection<BinhLuanBaiViet>? BinhLuanBaiViets { get; set; }
        public virtual ICollection<NguoiDungThichBaiViet>? NguoiDungThichBaiViets { get; set; }

    }
}
