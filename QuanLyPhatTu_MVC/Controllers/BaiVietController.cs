using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_MVC.Data;
using QuanLyPhatTu_MVC.Modal;
using QuanLyPhatTu_MVC.Model;
using QuanLyPhatTu_MVC.ViewModel;
using System.Net;
using System.Security.Claims;

namespace QuanLyPhatTu_MVC.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaiVietController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<PhatTu> _userManager;

        public BaiVietController(UserManager<PhatTu> userManager, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Add(BaiVietViewModel baiViet)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.TenTaiKhoan == userId);

                var checkLBV = await _dbContext.LoaiBaiViet.AnyAsync(x => x.LoaiBaiVietID == baiViet.LoaiBaiVietID);
                if (!checkLBV)
                {
                    return BadRequest(new { status = "Error", message = "Loại bài viết không tồn tại" });
                }
                var newBaiViet = new BaiViet()
                {
                    LoaiBaiVietID = baiViet.LoaiBaiVietID,
                    TieuDe = baiViet.TieuDe,
                    MoTa = baiViet.MoTa,
                    NoiDung = baiViet.NoiDung,
                    PhatTuID = user.Id,
                    NguoiDuyetBaiVietID = null,
                    SoLuotThich = 0,
                    SoLuotBinhLuan = 0,
                    ThoiGianDang = DateTime.Now,
                    ThoiGianCapNhat = null,
                    ThoiGianXoa = null,
                    DaXoa = false,
                    TrangThaiBaiVietID = 1

                };
                await _dbContext.AddAsync(newBaiViet);
                await _dbContext.SaveChangesAsync();
                return Ok(new { status = "sucsses", message = "Đăng bài viết thành công. Đang chờ duyệt" });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { status = "Error", message = ex.Message });

            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] BaiVietViewModel baiViet)
        {
            try
            {
                var checkBaiViet = await _dbContext.BaiViet.FirstOrDefaultAsync(x => x.BaiVietID == id);
                if (checkBaiViet == null)
                {
                    return BadRequest(new { status = "Error", message = "Bài viết không tồn tại" });
                }
                var checkLBV = await _dbContext.LoaiBaiViet.AnyAsync(x => x.LoaiBaiVietID == baiViet.LoaiBaiVietID);
                if (!checkLBV)
                {
                    return BadRequest(new { status = "Error", message = "Loại bài viết không tồn tại" });
                }

                checkBaiViet.LoaiBaiVietID = baiViet.LoaiBaiVietID;
                checkBaiViet.TieuDe = baiViet.TieuDe;
                checkBaiViet.MoTa = baiViet.MoTa;
                checkBaiViet.NoiDung = baiViet.NoiDung;
                checkBaiViet.PhatTuID = "b2cba825-6c54-499d-b256-4e9055fe9920";
                checkBaiViet.ThoiGianCapNhat = DateTime.Now;
                _dbContext.Update(checkBaiViet);
                await _dbContext.SaveChangesAsync();
                return Ok(new { status = "Sucsses", message = "Cập nhật thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { status = "Error", message = ex.Message });

            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var checkBaiViet = await _dbContext.BaiViet.FirstOrDefaultAsync(x => x.BaiVietID == id);
                if (checkBaiViet == null)
                {
                    return BadRequest(new { status = "Error", message = "Bài viết không tồn tại" });
                }
                checkBaiViet.ThoiGianXoa = DateTime.Now;
                checkBaiViet.DaXoa = true;
                _dbContext.Update(checkBaiViet);
                await _dbContext.SaveChangesAsync();
                return Ok(new { status = "sucsses", message = "Xóa thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { status = "Error", message = ex.Message });

            }
        }
        public class TrangThaiBaiVietDto
        {
            public int TrangThaiBaiVietID { get; set; }
        }
        [HttpPut("DuyetBaiViet/{id}")]
        public async Task<IActionResult> DuyetBaiViet(int id, [FromBody] TrangThaiBaiVietDto trangThaiBVID)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.TenTaiKhoan == userId);

                var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                if(role == null || role == "1")
                {
                    return Unauthorized(new { status = "Error", message = "Không có quyền truy cập" });
                }
                var checkBaiViet = await _dbContext.BaiViet.FirstOrDefaultAsync(x => x.BaiVietID == id);
                if (checkBaiViet == null)
                {
                    return BadRequest(new { status = "Error", message = "Bài viết không tồn tại" });
                }
                var checkTTBaiViet = await _dbContext.TrangThaiBaiViet.FirstOrDefaultAsync(x => x.TrangThaiBaiVietID == trangThaiBVID.TrangThaiBaiVietID);
                if (checkTTBaiViet == null)
                {
                    return BadRequest(new { status = "Error", message = "Trạng thái bài viết không tồn tại" });
                }
                checkBaiViet.NguoiDuyetBaiVietID = user.Id;
                checkBaiViet.TrangThaiBaiVietID = trangThaiBVID.TrangThaiBaiVietID;
                _dbContext.Update(checkBaiViet);
                await _dbContext.SaveChangesAsync();
                return Ok(new { status = "sucsses", message = "Duyệt bài thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { status = "Error", message = ex.Message });

            }
        }
        [HttpGet]
        public IActionResult FetchAll(
           [FromQuery] int? LoaiBV = null,
           [FromQuery] int? TTBaiViet = null,
           [FromQuery] Pagination pagination = null
           )
        {
            var query = _dbContext.BaiViet.Where(x => x.DaXoa == false).Select(x => new BaiViet
            {
                BaiVietID = x.BaiVietID,
                LoaiBaiVietID = x.LoaiBaiVietID,
                TieuDe = x.TieuDe,
                MoTa = x.MoTa,
                NguoiDuyetBaiVietID = x.NguoiDuyetBaiVietID,
                SoLuotThich = x.SoLuotThich,
                SoLuotBinhLuan = x.SoLuotBinhLuan,
                ThoiGianDang = x.ThoiGianDang,
                ThoiGianCapNhat = x.ThoiGianCapNhat,
                ThoiGianXoa = x.ThoiGianXoa,
                DaXoa = x.DaXoa,
                TrangThaiBaiVietID = x.TrangThaiBaiVietID,
            }).AsQueryable();
           
            if (LoaiBV.HasValue)
            {
                query = query.Where(x => x.LoaiBaiVietID == LoaiBV);
            }
            if (TTBaiViet.HasValue)
            {
                query = query.Where(x => x.TrangThaiBaiVietID == TTBaiViet);
            }
            var result = PageResult<BaiViet>.ToPageResult(pagination, query).AsEnumerable();
            pagination.TotalCount = query.Count();
            var res = new PageResult<BaiViet>(pagination, result);
            return Ok(res);
        }
    }
}
