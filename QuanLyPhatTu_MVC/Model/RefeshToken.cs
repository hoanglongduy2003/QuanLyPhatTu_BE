using QuanLyPhatTu_MVC.Modal;
using System.Runtime.CompilerServices;

namespace QuanLyPhatTu_MVC.Model
{
    public class RefeshToken
    {
        public int RefeshTokenID { get; set; }
        public string PhatTuID { get; set; }
        public virtual PhatTu? PhatTus { get; set; }
        public string Token { get; set; }
        public DateTime ThoiGianHetHan { get; set; }
    }
}
