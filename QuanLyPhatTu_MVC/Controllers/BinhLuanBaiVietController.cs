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
    public class BinhLuanBaiVietController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<PhatTu> _userManager;

        public BinhLuanBaiVietController(UserManager<PhatTu> userManager, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _userManager = userManager;
        }
        private void CapNhatSoLuongBinhLuan(int id)
        {
            var HienTai = _dbContext.BaiViet.FirstOrDefault(x => x.BaiVietID == id);
            if (HienTai != null)
            {
                HienTai.SoLuotBinhLuan = _dbContext.BinhLuanBaiViet.Count(x => x.BaiVietID == HienTai.BaiVietID && !x.DaXoa);
                if (HienTai.SoLuotBinhLuan < 0) HienTai.SoLuotBinhLuan = 0;
                _dbContext.Update(HienTai);
                _dbContext.SaveChanges();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(BinhLuanBaiVietViewModel baiViet)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.TenTaiKhoan == userId);

                var checkBV = await _dbContext.BaiViet.FirstOrDefaultAsync(x => x.LoaiBaiVietID == baiViet.BaiVietID);
                if (checkBV == null)
                {
                    return BadRequest(new { status = "Error", message = "Bài viết không tồn tại" });
                }
                if (checkBV.TrangThaiBaiVietID == 1 || checkBV.TrangThaiBaiVietID == 3)
                {
                    return BadRequest(new { status = "Error", message = "Bài viết chưa duyệt hoặc đã xóa" });
                }
                var newBaiViet = new BinhLuanBaiViet()
                {
                    BaiVietID = baiViet.BaiVietID,
                    PhatTuID = user.Id,
                    BinhLuan = baiViet.BinhLuan,
                    SoLuotThich = 0,
                    ThoiGianTao = DateTime.Now,
                    ThoiGianCapNhat = null,
                    ThoiGianXoa = null,
                    DaXoa = false,
                };
                await _dbContext.AddAsync(newBaiViet);
                await _dbContext.SaveChangesAsync();
                CapNhatSoLuongBinhLuan(baiViet.BaiVietID);
                return Ok(new { status = "sucsses", message = "Bình luận thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { status = "Error", message = ex.Message });

            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody]BinhLuanBaiVietViewModel baiViet)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.TenTaiKhoan == userId);

                var checkBV = await _dbContext.BinhLuanBaiViet.FirstOrDefaultAsync(x => x.BinhLuanBaiVietID == id);
                if (checkBV == null)
                {
                    return BadRequest(new { status = "Error", message = "Bài viết không tồn tại" });
                }

                checkBV.PhatTuID = user.Id;
                checkBV.BinhLuan = baiViet.BinhLuan;
                checkBV.ThoiGianCapNhat = DateTime.Now;
                _dbContext.Update(checkBV);
                await _dbContext.SaveChangesAsync();
                CapNhatSoLuongBinhLuan(baiViet.BaiVietID);
                return Ok(new { status = "sucsses", message = "Sửa bình luận thành công" });
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
                var checkBV = await _dbContext.BinhLuanBaiViet.FirstOrDefaultAsync(x => x.BinhLuanBaiVietID == id);
                if (checkBV == null)
                {
                    return BadRequest(new { status = "Error", message = "Bài viết không tồn tại" });
                }

                checkBV.ThoiGianXoa = DateTime.Now;
                checkBV.DaXoa = true;
                _dbContext.Update(checkBV);
                await _dbContext.SaveChangesAsync();
                CapNhatSoLuongBinhLuan(checkBV.BaiVietID);
                return Ok(new { status = "sucsses", message = "Xóa bình luận thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { status = "Error", message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult FetchAll(
           [FromQuery] int? baiVietID = null,
           [FromQuery] string? phatTuID = null,
           [FromQuery] Pagination pagination = null
           )
        {
            var query = _dbContext.BinhLuanBaiViet.Where(x => x.DaXoa == false).Select(x => new BinhLuanBaiViet
            {
                BinhLuanBaiVietID = x.BinhLuanBaiVietID,
                BaiVietID = x.BaiVietID,
                PhatTuID = x.PhatTuID,
                BinhLuan = x.BinhLuan,
                SoLuotThich = x.SoLuotThich,
                ThoiGianTao = x.ThoiGianTao,
            }).AsQueryable();

            if (baiVietID.HasValue)
            {
                query = query.Where(x => x.BaiVietID == baiVietID);
            }
            if (!string.IsNullOrEmpty(phatTuID))
            {
                query = query.Where(x => x.PhatTuID == phatTuID);
            }
            var result = PageResult<BinhLuanBaiViet>.ToPageResult(pagination, query).AsEnumerable();
            pagination.TotalCount = query.Count();
            var res = new PageResult<BinhLuanBaiViet>(pagination, result);
            return Ok(res);
        }
    }
}
