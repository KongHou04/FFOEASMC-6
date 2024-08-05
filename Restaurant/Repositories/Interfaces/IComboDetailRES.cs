using Restaurant.Models.Db;

namespace Restaurant.Repositories.Interfaces
{
    public interface IComboDetailRES
    {
        public IEnumerable<ComboDetail> GetByCombo(Guid comboId);

        public ComboDetail? GetById(int id);

        public ComboDetail? Add(ComboDetail comboDetail);

        public ComboDetail? Update(ComboDetail comboDetail, int id);

        public bool Delete(int id);
    }
}
