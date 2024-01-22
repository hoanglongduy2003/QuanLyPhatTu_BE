namespace QuanLyPhatTu_MVC.Modal
{
    public class QuyenHan
    {
        public int QuyenHanID { get; set; }
        public string TenQuyenHan { get; set; }
        public virtual ICollection<PhatTu> PhatTus { get; set; }
    }
}
