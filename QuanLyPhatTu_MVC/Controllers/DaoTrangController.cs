using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
    public class DaoTrangController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<PhatTu> _userManager;

        public DaoTrangController(UserManager<PhatTu> userManager, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _userManager = userManager;
        }
        [HttpPost]

        public async Task<IActionResult> Add([FromBody]DaoTrangViewModel daoTrang)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.TenTaiKhoan == userId);
                if (role != null && (role == "2" || role == "3"))
                {
                    var checkPhatTu = await _dbContext.PhatTu.AnyAsync(x => x.Id == user.Id);
                    if (!checkPhatTu)
                    {
                        return BadRequest(new { status = "Error", message = "Tru tri khong ton tai" });
                    }
                    
                    daoTrang.DaKetThuc = false;
                    var newDaoTrang = new DaoTrang()
                    {
                        DaKetThuc = false,
                        NoiDung = daoTrang.NoiDung,
                        NoiToChuc = daoTrang.NoiToChuc,
                        SoThanhVienThamGia = (int)daoTrang.SoThanhVienThamGia,
                        ThoiGianBatDau = (DateTime)daoTrang.ThoiGianBatDau,
                        NguoiTruTri = user.Id,
                        PhatTuDaoTrangs = { },
                    };
                    await _dbContext.AddAsync(newDaoTrang);
                    await _dbContext.SaveChangesAsync();
                    return Ok(new { status = "sucsses", message = "Them thanh cong" });
                }
                else
                {
                    return Unauthorized(new { status = "Error", message = "Không có quyền truy cập" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { status = "Error", message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]DaoTrangViewModel daoTrang) 
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.TenTaiKhoan == userId);
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == null || role == "1")
            {
                return Unauthorized(new { status = "Error", message = "Không có quyền truy cập" });
            }
            var checkDaoTrang = await _dbContext.DaoTrang.FirstOrDefaultAsync(x => x.DaoTrangID == id);
            if(checkDaoTrang == null)
            {
                return BadRequest(new { status = "Error", message = "Dao trang khong ton tai" });
            }
            var checkPhatTu = await _dbContext.PhatTu.AnyAsync(x => x.Id == user.Id);
            if (!checkPhatTu)
            {
                return BadRequest(new { status = "Error", message = "Tru tri khong ton tai" });
            }
            if (daoTrang.ThoiGianBatDau <= DateTime.Now)
            {
                checkDaoTrang.DaKetThuc = true;
            }
            else
            {
                checkDaoTrang.DaKetThuc = false;
            }
            checkDaoTrang.NoiDung = daoTrang.NoiDung;
            checkDaoTrang.NoiToChuc = daoTrang.NoiToChuc;
            checkDaoTrang.ThoiGianBatDau = (DateTime)daoTrang.ThoiGianBatDau;
            checkDaoTrang.NguoiTruTri = user.Id;
            if (!daoTrang.SoThanhVienThamGia.HasValue)
            {
                checkDaoTrang.SoThanhVienThamGia = checkDaoTrang.SoThanhVienThamGia;
            }
            else
            {
                checkDaoTrang.SoThanhVienThamGia = (int)daoTrang.SoThanhVienThamGia;
            }
            _dbContext.Update(checkDaoTrang);
            await _dbContext.SaveChangesAsync();
            return Ok(new { status = "sucsses", message = "Cap nhat thanh cong" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == null || role == "1")
            {
                return Unauthorized(new { status = "Error", message = "Không có quyền truy cập" });
            }
            var checkDaoTrang = await _dbContext.DaoTrang.FirstOrDefaultAsync(x => x.DaoTrangID == id);
            if (checkDaoTrang == null)
            {
                return BadRequest(new { status = "Error", message = "Dao trang khong ton tai" });
            }
            _dbContext.Remove(checkDaoTrang);
            await _dbContext.SaveChangesAsync();
            return Ok(new { status = "sucsses", message = "Xoa thanh cong" });
        }
        [HttpGet]
        public IActionResult FetchAll(
            [FromQuery] string? truTri = null,
            [FromQuery] DateTime? tuNgay = null,
            [FromQuery] DateTime? denNgay = null,
            [FromQuery] Pagination pagination = null
            )
        {
            var query = _dbContext.DaoTrang.Select(x => new DaoTrang
            {
                DaoTrangID = x.DaoTrangID,
                DaKetThuc = x.DaKetThuc,
                NoiDung = x.NoiDung,
                NoiToChuc = x.NoiToChuc,
                SoThanhVienThamGia = x.SoThanhVienThamGia,
                ThoiGianBatDau = x.ThoiGianBatDau,
                NguoiTruTri = x.NguoiTruTri,
            }).AsQueryable();
            if (!string.IsNullOrEmpty(truTri))
            {
                query = query.Where(x => x.NguoiTruTri == truTri);
            }
            if (tuNgay.HasValue)
            {
                query = query.Where(x => x.ThoiGianBatDau.Date >= tuNgay);
            }
            if (denNgay.HasValue)
            {
                query = query.Where(x => x.ThoiGianBatDau.Date <= denNgay);
            }
            var result = PageResult<DaoTrang>.ToPageResult(pagination, query).AsEnumerable();
            pagination.TotalCount = query.Count();
            var res = new PageResult<DaoTrang>(pagination, result);
            return Ok(res);
        }
    }
}
