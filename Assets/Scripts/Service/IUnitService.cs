using System.Collections.Generic;
using Entity;

namespace Service
{
    public interface IUnitService
    {
        public List<Unit> GetAlliedUnits();

        public int GetAlliedUnitsCount();

        public List<Unit> GetEnemyUnits();

        public void Add(Unit unit);

        public void Remove(Unit unit);
    }
}