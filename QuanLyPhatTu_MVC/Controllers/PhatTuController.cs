using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuanLyPhatTu_MVC.Data;
using QuanLyPhatTu_MVC.Modal;
using QuanLyPhatTu_MVC.Model;
using QuanLyPhatTu_MVC.Services;
using QuanLyPhatTu_MVC.Services.Model;
using QuanLyPhatTu_MVC.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace QuanLyPhatTu_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhatTuController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<PhatTu> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<PhatTu> _signInManager;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhatTuController(IHttpContextAccessor httpContextAccessor, IEmailService emailService, IConfiguration config, AppDbContext dbContext, UserManager<PhatTu> userManager, RoleManager<IdentityRole> roleManager, SignInManager<PhatTu> signInManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _config = config;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost("/api/dangKy")]
        public async Task <IActionResult>  Add([FromForm] DangKyViewModel model)
        {
            var checkEmail = await _userManager.FindByEmailAsync(model.Email);
            if (checkEmail != null)
            {
                return BadRequest(new { status = "Error", message = "Emai da ton tai" });
            }
            var checkPhone = await _dbContext.PhatTu.AnyAsync(x => x.SoDienThoai == model.SoDienThoai);
            if (checkPhone)
            {
                return BadRequest(new { status = "Error", message = "So dien thoai da ton tai" });
            }

            Account account = new Account(
              "dmisudml9",
              "846712993973757",
              "DGg33uB9M5Hjy_mmlL02X1LTJ-g"
            );

            Cloudinary cloudinary = new Cloudinary(account);

            // Tạo tên duy nhất cho tập tin
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AnhChup.FileName;

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "image");

            // Tạo thư mục nếu nó không tồn tại
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            // Đường dẫn đầy đủ đến tập tin
            var filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.AnhChup.CopyToAsync(stream);
            }
            // Lưu tập tin lên Cloudinary
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(filePath),
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            // Lấy URL của ảnh từ kết quả upload
            var imageUrl = uploadResult.SecureUri.AbsoluteUri;
            var newPhatTu = new PhatTu()
            {
                PhatTuID = Guid.NewGuid().ToString(),
                UserName  = model.TenTaiKhoan,
                HoTen  = model.HoTen,
                ChuaID  = model.ChuaID,
                TenTaiKhoan = model.TenTaiKhoan,
                AnhChup = imageUrl,
                DaHoanTuc = model.NgayHoanTuc == null ? false : true,
                Email = model.Email,
                MatKhau = model.MatKhau,
                GioiTinh = model.GioiTinh,
                NgayCapNhat = DateTime.Now,
                NgayHoanTuc = model.NgayHoanTuc,
                PhapDanh = model.PhapDanh,
                SoDienThoai = model.SoDienThoai,
                TrangThai = false,
                QuyenHanID = 1
            };
            var result = await _userManager.CreateAsync(newPhatTu, model.MatKhau);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { status = "Error", message = result.Errors });
            }
            newPhatTu.MatKhau = newPhatTu.PasswordHash;
            _dbContext.Update(newPhatTu);
            await _dbContext.SaveChangesAsync();
            var token = new Random().Next(100000, 999999).ToString();
            await _dbContext.AddAsync(new XacNhanEmail()
            {
                PhatTuID = newPhatTu.Id,
                ThoiGianHetHan = newPhatTu.NgayCapNhat.Value.AddMinutes(100),
                MaXacNhan = token,
                DaXacNhan = false,
            });
            await _dbContext.SaveChangesAsync();
            var message = new Message(new string[] { model.Email! },
                "Token xac thuc", $"Ma xac thuc cua ban la: \n {token}");
            _emailService.SendEmail(message);
            _httpContextAccessor?.HttpContext?.Session.SetString("PhatTuID", newPhatTu.Id);
            return Ok(new { status = "sucsses", message = "Dang ky thanh cong" });
        }
        [HttpPut("/api/CapNhatThongTin")]
        public async Task<IActionResult> Edit([FromBody]PhatTuViewModel model)
        {
            var checkPhatTu = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.Id == model.Id);
            if(checkPhatTu == null)
            {
                return BadRequest(new { status = "Error", message = "Tài khoản không tồn tại" });
            }
            var checkEmail = await _userManager.FindByEmailAsync(model.Email);
            if (checkEmail != null)
            {
                return BadRequest(new { status = "Error", message = "Emai da ton tai" });
            }
            var checkPhone = await _dbContext.PhatTu.AnyAsync(x => x.SoDienThoai == model.SoDienThoai);
            if (checkPhone)
            {
                return BadRequest(new { status = "Error", message = "So dien thoai da ton tai" });
            }
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "image");

            // Tạo thư mục nếu nó không tồn tại
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Tạo tên duy nhất cho tập tin
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AnhChup.FileName;

            var filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.AnhChup.CopyToAsync(stream);
            }
            checkPhatTu.UserName = model.TenTaiKhoan;
            checkPhatTu.HoTen = model.HoTen;
            checkPhatTu.ChuaID = model.ChuaID;
            checkPhatTu.TenTaiKhoan = model.TenTaiKhoan;
            checkPhatTu.AnhChup = uniqueFileName;
            checkPhatTu.DaHoanTuc = model.NgayHoanTuc == null ? false : true;
            checkPhatTu.Email = model.Email;
            checkPhatTu.GioiTinh = model.GioiTinh;
            checkPhatTu.NgayCapNhat = DateTime.Now;
            checkPhatTu.NgayHoanTuc = model.NgayHoanTuc;
            checkPhatTu.PhapDanh = model.PhapDanh;
            checkPhatTu.SoDienThoai = model.SoDienThoai;
            _dbContext.Update(checkPhatTu);
            await _dbContext.SaveChangesAsync();
            return Ok(new { status = "sucsses", message = "Cập nhật thành công" });
        }
        [HttpPost("/api/dangNhap")]
        public async Task<IActionResult> SignIn([FromBody] DangNhapViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.TenTaiKhoan);

            if (user == null)
            {
                return BadRequest(new { status = "Error", message = "Tài khoản không tồn tại" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.MatKhau, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                var refreshToken = new RefreshToken
                {
                    PhatTuID = user.Id,
                    Token = token.Result.RefeshToken,
                    ThoiGianHetHan = DateTime.UtcNow.AddHours(2)
                };
                await _dbContext.AddAsync(refreshToken);
                await _dbContext.SaveChangesAsync();
                return Ok(new {  message = "Đăng nhập thành công.",Token = token.Result });
            }

            return BadRequest(new { status = "Error", message = "Tài khoản hoặc mật khẩu không đúng." });
        }
        private async Task<TokenViewModel> GenerateJwtToken(PhatTu user)
        {
            var jwtKey = _config["Jwt:Secret"];
            var quyenHanId = user.QuyenHanID;
            if (jwtKey == null)
            {
                throw new InvalidOperationException("Jwt:Secret is not configured.");
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, quyenHanId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:ValidIssuer"],
                _config["Jwt:ValidIssuer"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefeshToken();
            var dataRefreshToken = new RefeshToken
            {
                PhatTuID = user.Id,
                Token = refreshToken,
                ThoiGianHetHan = DateTime.UtcNow.AddMinutes(30)

            };
            await _dbContext.AddAsync(dataRefreshToken);
            await _dbContext.SaveChangesAsync();

            return new TokenViewModel
            {
                AccsessToken = accessToken,
                RefeshToken = refreshToken
            };
        }
        private string GenerateRefeshToken()
        {
            var rand = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(rand);
            }
            return Convert.ToBase64String(rand);
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenViewModel model)
        {
            var refreshToken = await _dbContext.RefeshToken
           .FirstOrDefaultAsync(x => x.Token == model.RefeshToken);

            if (refreshToken == null || refreshToken.ThoiGianHetHan < DateTime.UtcNow)
            {
                return Unauthorized(new { message = "Mã RefreshToken không hợp lệ hoặc đã hết hạn" });
            }
            var user = await _dbContext.PhatTu.FindAsync(refreshToken.PhatTuID);
            var newAccessToken = await GenerateJwtToken(user);

            // Trả về Access Token mới
            return Ok(new { status = "Success", message = "Tạo token mới thành công.", accessToken = newAccessToken });
        }

        [HttpPost("/api/quenMatKhau")]
        public async Task<IActionResult> QuenMatKhau(QuenMatKhauViewModel taiKhoan)
        {
            var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.Email == taiKhoan.Email);
            if (user == null)
            {
                return BadRequest(new { status = "Error", message = "Người dùng không tồn tại" });
            }
            if (!user.TrangThai)
            {
                return BadRequest(new { status = "Error", message = "Tài khoản chưa xác minh. Vui lòng xác minh tài khoản." });
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Gửi email xác nhận
            var maXacThuc = new Random().Next(100000, 999999).ToString();
            await _dbContext.AddAsync(new XacNhanEmail()
            {
                PhatTuID = user.Id,
                ThoiGianHetHan = DateTime.Now.AddMinutes(100),
                MaXacNhan = token,
                DaXacNhan = false,
            });
            await _dbContext.SaveChangesAsync();
            var message = new Message(new string[] { user.Email! },
                "Ma xac thuc", $"Ma xac thuc cua ban la: \n {token}");
            _emailService.SendEmail(message);
            _httpContextAccessor?.HttpContext?.Session.SetString("PhatTuID", user.Id);
            _httpContextAccessor?.HttpContext?.Session.SetString("resetMatKhau", token);
            _httpContextAccessor?.HttpContext?.Session.SetString("email", user.Email);
            return Ok(new { status = "Success", message = "Yêu cầu khôi phục mật khẩu đã được gửi đến email của bạn" });
            
        }
        [HttpPost("/api/resetMatKhau")]
        public async Task<IActionResult> ResetMatKhau(ResetMatKhauViewModel taiKhoan)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("resetMatKhau");
            var checkXacNhanEmail = await _dbContext.XacNhanEmail.FirstOrDefaultAsync(x => x.MaXacNhan == token);
            if (!checkXacNhanEmail.DaXacNhan)
            {
                return BadRequest(new { status = "Error", message = "Vui lòng xác nhận email" });
            }
            var email = _httpContextAccessor.HttpContext.Session.GetString("email");
            var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.Email == email);
            if (token  == null || email == null || checkXacNhanEmail == null)
            {
                return BadRequest(new { status = "Error", message = "Có lỗi trong quá trình thực hiện" });
            }
            if (user == null)
            {
                return BadRequest(new { status = "Error", message = "Người dùng không tồn tại" });
            }
            if (!user.TrangThai)
            {
                return BadRequest(new { status = "Error", message = "Vui lòng xác minh tài khoản." });
            }
            var result = await _userManager.ResetPasswordAsync(user, token, taiKhoan.MatKhauMoi);
            if (result.Succeeded)
            {
                return Ok(new { status = "Success", message = "Đổi mật khẩu thành công" });
            }
            else
            {
                return BadRequest(new { status = "Error", message = "Đổi mật khẩu không thành công", errors = result.Errors });
            }
        }

        
    }
}
