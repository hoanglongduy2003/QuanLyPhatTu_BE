using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_MVC.Data;
using QuanLyPhatTu_MVC.Modal;
using QuanLyPhatTu_MVC.Model;
using QuanLyPhatTu_MVC.ViewModel;
using System;
using System.Security.Claims;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace QuanLyPhatTu_MVC.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DonDangKyController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<PhatTu> _userManager;
        public DonDangKyController(UserManager<PhatTu> userManager, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        private void CapNhatSoLuong(int DaoTrangID)
        {
            var HienTai = _dbContext.DaoTrang.FirstOrDefault(x => x.DaoTrangID == DaoTrangID);
            if (HienTai != null)
            {
                HienTai.SoThanhVienThamGia = _dbContext.DonDangKy.Count(x => x.DaoTrangID == HienTai.DaoTrangID && x.TrangThaiDonID == 2);
                _dbContext.Update(HienTai);
                _dbContext.SaveChanges();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(DonDangKyViewModel donDangky)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.TenTaiKhoan == userId);

            var checkPhatTu = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (checkPhatTu == null)
            {
                return BadRequest(new { status = "Error", message = "Tài khoản không tồn tại" });
            }
            if (checkPhatTu.TrangThai == false)
            {
                return BadRequest(new { status = "Error", message = "Tài khoản chưa xác minh. Vui lòng xác minh tài khoản" });
            }
            var checkDaoTrang = await _dbContext.DaoTrang.FirstOrDefaultAsync(x => x.DaoTrangID == donDangky.DaoTrangID);
            if (checkDaoTrang == null)
            {
                return BadRequest(new { status = "Error", message = "Đạo tràng không tồn tại" });
            }
            if (checkDaoTrang.DaKetThuc == true)
            {
                return BadRequest(new { status = "Error", message = "Đạo tràng đã kết thúc, không thể đăng ký" });
            }
            if (DateTime.Now.AddHours(1) > checkDaoTrang.ThoiGianBatDau)
            {
                return BadRequest(new { status = "Error", message = "Đạo tràng đang diễn ra, không thể đăng ký" });
            }
            var newDonDangKy = new DonDangKy()
            {
                NgayGuiDon = DateTime.Now,
                NgayXuLy = null,
                NguoiXuLy = null,
                TrangThaiDonID = 1,
                DaoTrangID = (int)donDangky.DaoTrangID,
                PhatTuID = user.Id,
            };
            await _dbContext.AddAsync(newDonDangKy);
            await _dbContext.SaveChangesAsync();
            return Ok(new { status = "sucsses", message = "Dang ky thanh cong, Cho xu ly" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> DuyetDon(int id,[FromBody] DonDangKyViewModel donDangKy)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.TenTaiKhoan == userId);

            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == null || role == "1")
            {
                return Unauthorized(new { status = "Error", message = "Không có quyền truy cập" });
            }
            var checkDonDK = await _dbContext.DonDangKy.FirstOrDefaultAsync(x => x.DonDangKyID == id);
            if (checkDonDK == null)
            {
                return BadRequest(new { status = "Error", message = "Don dang ky khong ton tai" });
            }
            var checkTTDon = await _dbContext.TrangThaiDon.AnyAsync(x => x.TrangThaiDonID == checkDonDK.TrangThaiDonID);
            if (!checkTTDon)
            {
                return BadRequest(new { status = "Error", message = "Trạng thái đơn không tồn tại" });
            }
            var checkNguoiXuLy = await _dbContext.PhatTu.AnyAsync(x => x.Id == user.Id);
            if (!checkNguoiXuLy)
            {
                return BadRequest(new { status = "Error", message = "Nguoi xu ly khong hop le" });
            }
            checkDonDK.TrangThaiDonID = donDangKy.TrangThaiDonID;
            checkDonDK.NgayXuLy = DateTime.Now;
            // fake nguoi xu ly
            checkDonDK.NguoiXuLy = user.Id;
            _dbContext.Update(checkDonDK);
            await _dbContext.SaveChangesAsync();
            //cap nhat so luong nguoi tham gia dao trang
            CapNhatSoLuong(checkDonDK.DaoTrangID);
            return Ok(new { status = "sucsses", message = "Duyet don thanh cong" });
        }
        [HttpGet]
        public IActionResult FetchAll(
            [FromQuery] int? trangThai = null,
            [FromQuery] int? daoTrang = null,
            [FromQuery] Pagination pagination = null
            )
        {
            var query = _dbContext.DonDangKy.Select(x => new DonDangKy
            {
                DonDangKyID = x.DonDangKyID,
                NgayGuiDon = x.NgayGuiDon,
                NgayXuLy = x.NgayXuLy,
                NguoiXuLy = x.NguoiXuLy,
                TrangThaiDonID = x.TrangThaiDonID,
                DaoTrangID = x.DaoTrangID,
                PhatTuID = x.PhatTuID,
            }).AsQueryable();
            if (trangThai.HasValue)
            {
                query = query.Where(x => x.TrangThaiDonID == trangThai);
            }
            if (daoTrang.HasValue)
            {
                query = query.Where(x => x.DaoTrangID == daoTrang);
            }
            var result = PageResult<DonDangKy>.ToPageResult(pagination, query).AsEnumerable();
            pagination.TotalCount = query.Count();
            var res = new PageResult<DonDangKy>(pagination, result);
            return Ok(res);
        }
        
    }
}
