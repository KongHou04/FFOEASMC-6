using Microsoft.AspNetCore.Mvc;
using Restaurant.DTOs;
using Restaurant.Services.Interfaces;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboDetailsController(IComboDetailSVC ComboDetailSVC) : ControllerBase
    {
        [HttpGet("byorder/{orderId}")]
        public IEnumerable<ComboDetailDTO> GetByCombo([FromRoute] Guid comboId)
        {
            return ComboDetailSVC.GetByCombo(comboId);
        }

        [HttpGet("{id}")]
        public ComboDetailDTO? GetById([FromRoute] int id)
        {
            return ComboDetailSVC.GetById(id);
        }

        [HttpPost]
        public ComboDetailDTO? Add([FromBody] ComboDetailDTO comboDetailDTO)
        {
            return ComboDetailSVC.Add(comboDetailDTO);
        }

        [HttpPut("{id}")]
        public ComboDetailDTO? Update([FromBody] ComboDetailDTO comboDetailDTO, [FromRoute] int id)
        {
            return ComboDetailSVC.Update(comboDetailDTO, id);
        }

        [HttpDelete("{id}")]
        public bool Delete([FromRoute] int id)
        {
            return ComboDetailSVC.Delete(id);
        }
    }
}
