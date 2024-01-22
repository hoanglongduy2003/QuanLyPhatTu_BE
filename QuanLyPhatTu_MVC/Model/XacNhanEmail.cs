namespace QuanLyPhatTu_MVC.Modal
{
    public class XacNhanEmail
    {
        public int XacNhanEmailID { get; set; }
        public string PhatTuID { get; set; }
        public DateTime ThoiGianHetHan { get; set; }
        public string MaXacNhan { get; set; }
        public bool DaXacNhan { get; set; }
        public virtual PhatTu? PhatTu { get; set; }
    }
}
