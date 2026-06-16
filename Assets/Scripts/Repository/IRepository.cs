using System.Collections.Generic;
using Entity;

namespace Repository
{
    public interface IRepository
    {
        /**
         * get current units of the player.
         */
        public List<Unit> GetUnits();
        
        /**
         * add a new unit to the player.
         */
        public void AddUnit(Unit unit);
        
        public SynergieEffect GetSynergieEffect();
        
        public void AddSynergieEffect(SynergieEffect effect);
    }
}