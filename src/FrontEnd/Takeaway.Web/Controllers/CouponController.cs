using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Takeaway.Web.Models;
using Takeaway.Web.Service.IService;

namespace Takeaway.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new List<CouponDto>();
            var response = await _couponService.GetCouponsAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        [HttpGet]
        public IActionResult CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto couponDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.CreateCouponAsync(couponDto);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction("CouponIndex");
                }
                else
                {
                    ModelState.AddModelError("CouponCreate", response.Message);
                }
            }
            return View(couponDto);
        }

        [HttpGet]
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            CouponDto? list = new CouponDto();
            var response = await _couponService.GetCouponByIdAsync(couponId);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(list);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            CouponDto? list = new CouponDto();
            var response = await _couponService.DeleteCouponAsync(couponDto.CouponId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CouponIndex));
            }
            ModelState.AddModelError("CouponDelete", "删除失败");
            return View(couponDto);
        }
    }
}
