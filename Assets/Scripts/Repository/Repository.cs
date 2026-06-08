using System.Collections.Generic;
using Entity;
using Service;

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