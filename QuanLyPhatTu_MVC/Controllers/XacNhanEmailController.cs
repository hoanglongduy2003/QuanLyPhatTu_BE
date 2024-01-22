using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_MVC.Data;
using QuanLyPhatTu_MVC.Modal;
using QuanLyPhatTu_MVC.ViewModel;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace QuanLyPhatTu_MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XacNhanEmailController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public XacNhanEmailController(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> XacNhanEmail(XacNhanEmailViewModel maXacNhan)
        {
            var phatTuID = _httpContextAccessor.HttpContext.Session.GetString("PhatTuID");
            var checkXacNhan = await _dbContext.XacNhanEmail.OrderByDescending(x => x.XacNhanEmailID).FirstOrDefaultAsync(x => x.PhatTuID == phatTuID && !x.DaXacNhan);
            var checkPhatTu = await _dbContext.PhatTu.FirstOrDefaultAsync(x => x.Id == phatTuID);
            if(checkPhatTu == null)
            {
                return BadRequest(new { status = "Error", message = "Loi trong qua trinh xac nhan" });
            }
            if (checkXacNhan == null)
            {
                return BadRequest(new { status = "Error", message = "Chua co ma xac nhan" });
            }
            if (checkXacNhan.ThoiGianHetHan <= DateTime.Now)
            {
                return BadRequest(new { status = "Error", message = "Ma xac nhan da het han" });
            }
            if (checkXacNhan.MaXacNhan != maXacNhan.MaXacNhan)
            {
                return BadRequest(new { status = "Error", message = "Ma xac nhan khong dung" });
            }
            checkXacNhan.DaXacNhan = true;
            _dbContext.Update(checkXacNhan);
            await _dbContext.SaveChangesAsync();
            checkPhatTu.TrangThai = true;
            _dbContext.Update(checkPhatTu);
            await _dbContext.SaveChangesAsync();
            return Ok(new { status = "sucsses", message = "Xac nhan tai khoan thanh cong" });
        }
    }
}
