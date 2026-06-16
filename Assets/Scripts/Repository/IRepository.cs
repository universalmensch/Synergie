using System.Collections.Generic;
using Entity;

namespace Repository
{
    /// <summary>
    /// Interface to access DB operations.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Get all units which belong to the player.
        /// </summary>
        /// <returns> A list of the units.</returns>
        public List<Unit> GetAlliedUnits();
        
        /// <summary>
        /// Get the count of the units which belong to the player.
        /// </summary>
        /// <returns> count of units.</returns>
        public int GetAlliedUnitsCount();
        
        /// <summary>
        /// Add a new Unit.
        /// Mostly enemy units don't need to be saved, so this is mainly for new allied units.
        /// </summary>
        /// <param name="unit"> to be saved in the DB.</param>
        public void AddUnit(Unit unit);
        
        public void UpdateUnit(Unit unit);
        
        public void DeleteUnit(Unit unit);
        
        public SynergieEffect GetSynergieEffect();
        
        public void AddSynergieEffect(SynergieEffect effect);
    }
}