using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboDetailController(IComboDetailSVC ComboDetailSVC) : ControllerBase
    {
        /// <summary>
        /// Get detail combo by order ID
        /// </summary>
        ///<returns>
        /// List the combodetail
        ///</returns>
        [HttpGet("byorder/{orderId}")]
        public IEnumerable<ComboDetailDTO> GetByCombo([FromRoute] Guid comboId)
        {
            return ComboDetailSVC.GetByCombo(comboId);
        }
        /// <summary>
        /// Get combo by ID
        /// </summary>
        /// <param name="id">Id of Combo</param>
        ///<returns>
        /// List the combodetail
        ///</returns>
        [HttpGet("{id}")]
        public ComboDetailDTO? GetById([FromRoute] int id)
        {
            return ComboDetailSVC.GetById(id);
        }
        /// <summary>
        /// Create detail combo
        /// </summary>
        /// <param name="comboDetailDTO">Combo detail to be created.</param>
        ///<returns>
        /// Combo detail created
        ///</returns>
        [HttpPost]
        public ComboDetailDTO? Add([FromBody] ComboDetailDTO comboDetailDTO)
        {
            return ComboDetailSVC.Add(comboDetailDTO);
        }
        /// <summary>
        /// Update detail combo by ID
        /// </summary>
        /// <param name="comboDetailDTO">Combo detail to be updated.</param>
        /// <param name="id">Id of combo detail.</param>
        ///<returns>
        /// Combo detail updated.
        ///</returns>
        [HttpPut("{id}")]
        public ComboDetailDTO? Update([FromBody] ComboDetailDTO comboDetailDTO, [FromRoute] int id)
        {
            return ComboDetailSVC.Update(comboDetailDTO, id);
        }
        /// <summary>
        /// Delete a detail combo by ID
        /// </summary>
        /// <param name="id">Id of combo detail</param>
        ///<returns>
        /// True or false
        ///</returns>
        [HttpDelete("{id}")]
        public bool Delete([FromRoute] int id)
        {
            return ComboDetailSVC.Delete(id);
        }
    }
}
