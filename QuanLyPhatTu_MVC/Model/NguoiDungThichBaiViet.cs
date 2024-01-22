using QuanLyPhatTu_MVC.Modal;

namespace QuanLyPhatTu_MVC.Model
{
    public class NguoiDungThichBaiViet
    {
        public int NguoiDungThichBaiVietID { get; set; }
        public string PhatTuID { get; set; }
        public int BaiVietID { get; set; }
        public DateTime ThoiGianThich { get; set; }
        public bool DaXoa { get; set; }
        public virtual PhatTu? PhatTus { get; set; }
        public virtual BaiViet? BaiViets { get; set; }
    }
}
