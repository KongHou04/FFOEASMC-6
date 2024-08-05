using Restaurant.DTOs;

namespace Restaurant.Services.Interfaces
{
    public interface IComboDetailSVC
    {
        public IEnumerable<ComboDetailDTO> GetByCombo(Guid comboId);

        public ComboDetailDTO? GetById(int id);

        public ComboDetailDTO? Add(ComboDetailDTO comboDetailDTO);

        public ComboDetailDTO? Update(ComboDetailDTO comboDetailDTO, int id);

        public bool Delete(int id);
    }
}
