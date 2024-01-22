using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_MVC.Data;
using QuanLyPhatTu_MVC.Modal;
using QuanLyPhatTu_MVC.Model;
using QuanLyPhatTu_MVC.ViewModel;
using System.Security.Claims;

namespace QuanLyPhatTu_MVC.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ThichBinhLuanBaiVietController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<PhatTu> _userManager;
        public ThichBinhLuanBaiVietController(UserManager<PhatTu> userManager, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        private void CapNhatSoLuongThichBinhLuan(int id)
        {
            var HienTai = _dbContext.BinhLuanBaiViet.FirstOrDefault(x => x.BinhLuanBaiVietID == id);
            if (HienTai != null)
            {
                HienTai.SoLuotThich = _dbContext.NguoiDungThichBinhLuanBaiViet.Count(x => x.BinhLuanBaiVietID == HienTai.BinhLuanBaiVietID && !x.DaXoa);
                if (HienTai.SoLuotThich < 0) HienTai.SoLuotThich = 0;
                _dbContext.Update(HienTai);
                _dbContext.SaveChanges();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ThichBinhLuanBaiViet(ThichBLBaiVietViewModel item)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.TenTaiKhoan == userId);


            var checkThichBaiviet = await _dbContext.NguoiDungThichBinhLuanBaiViet.FirstOrDefaultAsync(x => x.PhatTuID == user.Id && x.BinhLuanBaiVietID == item.BinhLuanBaiVietID);
            if (checkThichBaiviet == null)
            {
                var checkBaiViet = await _dbContext.BinhLuanBaiViet.AnyAsync(x => x.BinhLuanBaiVietID == item.BinhLuanBaiVietID);
                if (!checkBaiViet)
                {
                    return BadRequest(new { status = "Error", message = "Bình luận bài viết không tồn tại" });
                }
                var newThichBaiViet = new NguoiDungThichBinhLuanBaiViet()
                {
                    PhatTuID = user.Id,
                    BinhLuanBaiVietID = item.BinhLuanBaiVietID,
                    ThoiGianLike = DateTime.Now,
                };
                await _dbContext.AddAsync(newThichBaiViet);
                await _dbContext.SaveChangesAsync();
                CapNhatSoLuongThichBinhLuan(item.BinhLuanBaiVietID);
                return Ok(new { status = "sucsses", message = "Thích bình luận bài viết thành công" });
            }
            else
            {
                if (checkThichBaiviet.DaXoa == false)
                {
                    checkThichBaiviet.DaXoa = true;
                    _dbContext.Update(checkThichBaiviet);
                    await _dbContext.SaveChangesAsync();
                    CapNhatSoLuongThichBinhLuan(item.BinhLuanBaiVietID);
                    return Ok(new { status = "sucsses", message = "Đã bỏ lượt thích bình luận" });
                }
                else
                {
                    checkThichBaiviet.DaXoa = false;
                    _dbContext.Update(checkThichBaiviet);
                    await _dbContext.SaveChangesAsync();
                    CapNhatSoLuongThichBinhLuan(item.BinhLuanBaiVietID);
                    return Ok(new { status = "sucsses", message = "Thích bình luận bài viết thành công" });
                }

            }

        }
        [HttpGet]
        public IActionResult FetchAll(
           [FromQuery] int? binhLuanBaiVietID = null,
           [FromQuery] string? phatTuID = null,
           [FromQuery] Pagination pagination = null
           )
        {
            var query = _dbContext.NguoiDungThichBinhLuanBaiViet.Where(x => x.DaXoa == false).Select(x => new NguoiDungThichBinhLuanBaiViet
            {
                BinhLuanBaiVietID = x.BinhLuanBaiVietID,
                PhatTuID = x.PhatTuID,
                ThoiGianLike = x.ThoiGianLike,
            }).AsQueryable();
            if (binhLuanBaiVietID.HasValue)
            {
                query = query.Where(x => x.BinhLuanBaiVietID == binhLuanBaiVietID);
            }
            if (!string.IsNullOrEmpty(phatTuID))
            {
                query = query.Where(x => x.PhatTuID == phatTuID);
            }
            var result = PageResult<NguoiDungThichBinhLuanBaiViet>.ToPageResult(pagination, query).AsEnumerable();
            pagination.TotalCount = query.Count();
            var res = new PageResult<NguoiDungThichBinhLuanBaiViet>(pagination, result);
            return Ok(res);
        }
    }
}
