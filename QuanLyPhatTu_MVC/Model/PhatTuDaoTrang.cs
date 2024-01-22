using QuanLyPhatTu_MVC.Modal;

namespace QuanLyPhatTu_MVC.Model
{
    public class PhatTuDaoTrang
    {
        public int PhatTuDaoTrangID { get; set; }
        public bool DaThamGia { get; set; }
        public string LiDoKhongThamGia { get; set; }
        public int DaoTrangID { get; set; }
        public string PhatTuID { get; set; }
        public virtual PhatTu? PhatTus { get; set; }
        public virtual DaoTrang? DaoTrangs { get; set; }
    }
}
