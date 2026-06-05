using System.Collections.Generic;

namespace Service
{
    public interface IEnemyService
    {
        /**
         * returns a list of enemies for a battle.
         */
        public List<string> GetEnemies();
    }
}