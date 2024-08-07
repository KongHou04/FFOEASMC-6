using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponTypesController(ICouponTypeSVC couponTypeSVC) : ControllerBase
    {
        /// <summary>
        /// Get all the Coupons
        /// </summary>
        ///<returns>
        /// All the coupons
        ///</returns>
        [HttpGet]
        public IEnumerable<CouponTypeDTO> GetAll()
        {
            return couponTypeSVC.GetAll();
        }
        /// <summary>
        /// Get a coupon by ID
        /// </summary>
        /// <param name="id">ID of coupon</param>
        ///<returns>
        /// A coupon by ID
        ///</returns>
        [HttpGet("{id}")]
        public CouponTypeDTO? GetById([FromRoute] int id)
        {
            return couponTypeSVC.GetById(id);
        }
        /// <summary>
        /// Get a coupon by ID that are available
        /// </summary>
        /// <param name="id">ID of coupon</param>
        ///<returns>
        /// A coupon by ID
        ///</returns>
        [HttpGet("availablebycoupon/{id}")]
        public CouponTypeDTO? GetAvailableByCounponId([FromRoute] Guid id)
        {
            return couponTypeSVC.GetAvailableByCouponId(id);
        }
        /// <summary>
        /// Post a coupon
        /// </summary>
        /// <param name="CouponTypeDTO">A Coupon to be created.</param>
        ///<returns>
        /// A coupon created.
        ///</returns>
        [HttpPost]
        public CouponTypeDTO? Add([FromBody] CouponTypeDTO CouponTypeDTO)
        {
            return couponTypeSVC.Add(CouponTypeDTO);
        }
        /// <summary>
        /// Updated a coupon by ID
        /// </summary>
        /// <param name="CouponTypeDTO">A Coupon to be updated.</param>
        /// <param name="id">Id of coupon to be updated</param>
        ///<returns>
        /// A coupon updated.
        ///</returns>
        [HttpPut("{id}")]
        public CouponTypeDTO? Update([FromBody ]CouponTypeDTO CouponTypeDTO, int id)
        {
            return couponTypeSVC.Update(CouponTypeDTO, id);
        }
        /// <summary>
        /// Delete a coupon by ID.
        /// </summary>
        /// <param name="id">Id of coupon to be deleted.</param>
        ///<returns>
        /// A coupon deleted.
        ///</returns>
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return couponTypeSVC.Delete(id);
        }
    }
}
