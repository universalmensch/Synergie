using System.Collections.Generic;
using Entity;

namespace Repository
{
    public class Repository : IRepository
    {
        public List<Unit> GetUnits()
        {
            return new List<Unit>();
        }

        public void AddUnit(Unit unit)
        {
            
        }
    }
}