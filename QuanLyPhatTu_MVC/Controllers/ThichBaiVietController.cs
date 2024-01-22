using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_MVC.Data;
using QuanLyPhatTu_MVC.Modal;
using QuanLyPhatTu_MVC.Model;
using QuanLyPhatTu_MVC.ViewModel;
using System.Linq;
using System.Security.Claims;

namespace QuanLyPhatTu_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ThichBaiVietController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<PhatTu> _userManager;
        public ThichBaiVietController(UserManager<PhatTu> userManager, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        private void CapNhatSoLuongThich(int id)
        {
            var HienTai = _dbContext.BaiViet.FirstOrDefault(x => x.BaiVietID == id);
            if (HienTai != null)
            {
                HienTai.SoLuotThich = _dbContext.NguoiDungThichBaiViet.Count(x => x.BaiVietID == HienTai.BaiVietID && !x.DaXoa);
                if (HienTai.SoLuotThich < 0) HienTai.SoLuotThich = 0;
                _dbContext.Update(HienTai);
                _dbContext.SaveChanges();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ThichBaiViet(ThichBaiVietViewModel item)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.TenTaiKhoan == userId);
            var checkThichBaiviet = await _dbContext.NguoiDungThichBaiViet.FirstOrDefaultAsync(x => x.PhatTuID == user.Id && x.BaiVietID == item.BaiVietID);

            if(checkThichBaiviet == null)
            {
                var checkBaiViet = await _dbContext.BaiViet.FirstOrDefaultAsync(x => x.BaiVietID == item.BaiVietID);
                if (checkBaiViet == null)
                {
                    return BadRequest(new { status = "Error", message = "Bài viết không tồn tại" });
                }
                if(checkBaiViet.TrangThaiBaiVietID == 1 || checkBaiViet.TrangThaiBaiVietID == 3)
                {
                    return BadRequest(new { status = "Error", message = "Bài viết chưa duyệt hoặc đã xóa" });
                }
                var newThichBaiViet= new NguoiDungThichBaiViet()
                {
                    PhatTuID = user.Id,
                    BaiVietID = item.BaiVietID,
                    ThoiGianThich = DateTime.Now,
                };
                await _dbContext.AddAsync(newThichBaiViet);
                await _dbContext.SaveChangesAsync();
                CapNhatSoLuongThich(item.BaiVietID);
                return Ok(new { status = "sucsses", message = "Thích bài viết thành công" });
            }
            else
            {
                if(checkThichBaiviet.DaXoa == false)
                {
                    checkThichBaiviet.DaXoa = true;
                    _dbContext.Update(checkThichBaiviet);
                    await _dbContext.SaveChangesAsync();
                    CapNhatSoLuongThich(item.BaiVietID);
                    return Ok(new { status = "sucsses", message = "Đã bỏ lượt thích" });
                }
                else
                {
                    var checkBaiViet = await _dbContext.BaiViet.FirstOrDefaultAsync(x => x.BaiVietID == item.BaiVietID);
                    if (checkBaiViet == null)
                    {
                        return BadRequest(new { status = "Error", message = "Bài viết không tồn tại" });
                    }
                    if (checkBaiViet.TrangThaiBaiVietID == 1 || checkBaiViet.TrangThaiBaiVietID == 3)
                    {
                        return BadRequest(new { status = "Error", message = "Bài viết chưa duyệt hoặc đã xóa" });
                    }
                    checkThichBaiviet.DaXoa = false;
                    _dbContext.Update(checkThichBaiviet);
                    await _dbContext.SaveChangesAsync();
                    CapNhatSoLuongThich(item.BaiVietID);
                    return Ok(new { status = "sucsses", message = "Thích bài viết thành công" });
                }
            }
        }
        [HttpGet]
        public IActionResult FetchAll(
           [FromQuery] int? baiVietID = null,
           [FromQuery] string? phatTuID = null,
           [FromQuery] Pagination pagination = null
           )
        {
            var query = _dbContext.NguoiDungThichBaiViet.Where(x => x.DaXoa == false).Select(x => new NguoiDungThichBaiViet
            {
                BaiVietID = x.BaiVietID,
                PhatTuID = x.PhatTuID,
            }).AsQueryable();
            if (baiVietID.HasValue)
            {
                query = query.Where(x => x.BaiVietID == baiVietID);
            }
            if (!string.IsNullOrEmpty(phatTuID))
            {
                query = query.Where(x => x.PhatTuID == phatTuID);
            }
            var result = PageResult<NguoiDungThichBaiViet>.ToPageResult(pagination, query).AsEnumerable();
            pagination.TotalCount = query.Count();
            var res = new PageResult<NguoiDungThichBaiViet>(pagination, result);
            return Ok(res);
        }
    }
}
