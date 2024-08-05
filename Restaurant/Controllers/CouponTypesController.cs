using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponTypesController(ICouponTypeSVC couponTypeSVC) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<CouponTypeDTO> GetAll()
        {
            return couponTypeSVC.GetAll();
        }

        [HttpGet("{id}")]
        public CouponTypeDTO? GetById([FromRoute] int id)
        {
            return couponTypeSVC.GetById(id);
        }

        [HttpGet("availablebycoupon/{id}")]
        public CouponTypeDTO? GetAvailableByCounponId([FromRoute] Guid id)
        {
            return couponTypeSVC.GetAvailableByCouponId(id);
        }

        [HttpPost]
        public CouponTypeDTO? Add([FromBody] CouponTypeDTO CouponTypeDTO)
        {
            return couponTypeSVC.Add(CouponTypeDTO);
        }

        [HttpPut("{id}")]
        public CouponTypeDTO? Update([FromBody ]CouponTypeDTO CouponTypeDTO, int id)
        {
            return couponTypeSVC.Update(CouponTypeDTO, id);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return couponTypeSVC.Delete(id);
        }
    }
}
