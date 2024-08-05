using AutoMapper;
using Restaurant.DTOs;
using Restaurant.Models.Db;
using Restaurant.Repositories.Implements;
using Restaurant.Repositories.Interfaces;
using Restaurant.Services.Interfaces;

namespace Restaurant.Services.Implements
{
    public class ComboDetailSVC(IMapper mapper, IComboDetailRES comboDetailRES) : IComboDetailSVC
    {
        public ComboDetailDTO? Add(ComboDetailDTO comboDetailDTO)
        {
            var comboDetail = mapper.Map<ComboDetail>(comboDetailDTO);
            var addedCD = comboDetailRES.Add(comboDetail);
            return mapper.Map<ComboDetailDTO>(addedCD);
        }

        public bool Delete(int id)
        {
            return comboDetailRES.Delete(id);
        }

        public ComboDetailDTO? GetById(int id)
        {
            var existingCD = comboDetailRES.GetById(id);
            return mapper.Map<ComboDetailDTO>(existingCD);
        }

        public IEnumerable<ComboDetailDTO> GetByCombo(Guid comboId)
        {
            var comboDetails = comboDetailRES.GetByCombo(comboId);
            return mapper.Map<IEnumerable<ComboDetailDTO>>(comboDetails);
        }

        public ComboDetailDTO? Update(ComboDetailDTO ComboDetailDTO, int id)
        {
            var comboDetail = mapper.Map<ComboDetail>(ComboDetailDTO);
            var updatedCD = comboDetailRES.Add(comboDetail);
            return mapper.Map<ComboDetailDTO>(updatedCD);
        }
    }
}
