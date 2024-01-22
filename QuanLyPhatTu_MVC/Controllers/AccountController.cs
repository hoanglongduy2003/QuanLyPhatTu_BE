using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhatTu_MVC.Data;
using QuanLyPhatTu_MVC.Modal;
using QuanLyPhatTu_MVC.Model;
using QuanLyPhatTu_MVC.Services;
using QuanLyPhatTu_MVC.ViewModel;
using System.Security.Claims;

namespace QuanLyPhatTu_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<PhatTu> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<PhatTu> _signInManager;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IHttpContextAccessor httpContextAccessor, IEmailService emailService, IConfiguration config, AppDbContext dbContext, UserManager<PhatTu> userManager, RoleManager<IdentityRole> roleManager, SignInManager<PhatTu> signInManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _config = config;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        [HttpPut("/api/doiMatKhau")]
        public async Task<IActionResult> DoiMatKhau(DoiMatKhauViewModel taiKhoan)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
            {
                return BadRequest(new { status = "Error", message = "Người dùng không tồn tại" });
            }
            if (!user.TrangThai)
            {
                return BadRequest(new { status = "Error", message = "Vui lòng xác minh tài khoản." });
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, taiKhoan.MatKhau);
            if (!isPasswordValid)
            {
                return BadRequest(new { status = "Error", message = "Mật khẩu cũ không đúng." });
            }
            if (taiKhoan.MatKhauMoi != taiKhoan.NhapLaiMatKhau)
            {
                return BadRequest(new { status = "Error", message = "Mật khẩu nhập lại không đúng." });
            }
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, taiKhoan.MatKhau, taiKhoan.MatKhauMoi);
            if (changePasswordResult.Succeeded)
            {
                return Ok(new { status = "Success", message = "Đổi mật khẩu thành công" });
            }
            else
            {
                return BadRequest(new { status = "Error", message = "Đổi mật khẩu không thành công", errors = changePasswordResult.Errors });
            }
        }
        [HttpGet("/api/PhatTu")]
        public IActionResult GetAll(
            [FromQuery] string? hoTen = null,
            [FromQuery] string? email = null,
            [FromQuery] string? gioiTinh = null,
            [FromQuery] Pagination pagination = null)
        {
            var query = _dbContext.PhatTu.Select(x => new PhatTu
            {
                Id = x.Id,
                PhatTuID = x.PhatTuID,
                HoTen = x.HoTen,
                TenTaiKhoan = x.TenTaiKhoan,
                MatKhau = x.MatKhau,
                AnhChup = x.AnhChup,
                DaHoanTuc = x.DaHoanTuc,
                Email = x.Email,
                GioiTinh = x.GioiTinh,
                ChuaID = x.ChuaID,
                NgayCapNhat = x.NgayCapNhat,
                NgayHoanTuc = x.NgayHoanTuc,
                PhapDanh = x.PhapDanh,
                SoDienThoai = x.SoDienThoai,
                TrangThai = x.TrangThai,
                QuyenHanID = x.QuyenHanID,
            }).AsQueryable();

            if (!string.IsNullOrEmpty(hoTen))
            {
                query = query.Where(x => x.HoTen.ToLower().Contains(hoTen));
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(x => x.Email.ToLower().Contains(email));
            }
            if (!string.IsNullOrEmpty(gioiTinh))
            {
                query = query.Where(x => x.GioiTinh.ToLower().Contains(gioiTinh));
            }
            var result = PageResult<PhatTu>.ToPageResult(pagination, query).AsEnumerable();
            pagination.TotalCount = query.Count();
            var res = new PageResult<PhatTu>(pagination, result);
            return Ok(res);
        }
    }
}
