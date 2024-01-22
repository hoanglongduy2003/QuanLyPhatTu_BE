using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_MVC.ViewModel
{
    public class PhatTuViewModel
    {
        public string Id { get; set; }
        public string HoTen { get; set; }
        public string TenTaiKhoan { get; set; }
        public IFormFile? AnhChup { get; set; }
        public string Email { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgayHoanTuc { get; set; }
        public string PhapDanh { get; set; }
        public string SoDienThoai { get; set; }
        public int ChuaID { get; set; }
    }
}
