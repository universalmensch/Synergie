using System.Collections.Generic;
using Entity;

namespace Service
{
    public interface ISynergieService
    {
        /// <summary>
        /// Get the current synergies of the player.
        /// </summary>
        /// <returns> a list of the synergies.</returns>
        public List<Synergie> GetSynergies();
        
        /// <summary>
        /// Get the currently not selected synergie effects of the player.
        /// </summary>
        /// <returns> a list of the synergie effects.</returns>
        public List<SynergieEffect> GetSynergieEffects();
    }
}