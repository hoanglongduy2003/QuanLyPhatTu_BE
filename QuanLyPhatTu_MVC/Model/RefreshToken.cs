namespace QuanLyPhatTu_MVC.Modal
{
    public class RefreshToken
    {
        public int RefreshTokenID { get; set; }
        public string Token { get; set; }
        public DateTime ThoiGianHetHan { get; set; }
        public string PhatTuID { get; set; }
        public virtual PhatTu? PhatTu { get; set; }
    }
}
