using System.Collections.Generic;
using Entity;

namespace Service
{
    public interface ISynergieService
    {
        /// <summary>
        ///     Get the current synergies of the player.
        /// </summary>
        /// <returns> a list of the synergies.</returns>
        public List<Synergie> GetSynergies();

        /// <summary>
        ///     Get the currently not selected synergie resources of the player.
        /// </summary>
        /// <returns> a list of the synergie resources.</returns>
        public List<SynergieResource> GetSynergieResources();

        /// <summary>
        ///     Get the currently not selected synergie triggers of the player.
        /// </summary>
        /// <returns> a list of the synergie triggers.</returns>
        public List<SynergieTrigger> GetSynergieTriggers();

        /// <summary>
        ///     Add a new synergie resource to the player.
        /// </summary>
        /// <param name="synergieResource"> to be added.</param>
        public void AddSynergieResource(SynergieResource synergieResource);

        /// <summary>
        ///     Update a synergie resource of the player.
        /// </summary>
        /// <param name="synergieResource"> to be updated.</param>
        public void UpdateSynergieResource(SynergieResource synergieResource);

        /// <summary>
        ///     Add a new synergie trigger to the player.
        /// </summary>
        /// <param name="synergieTrigger"></param>
        public void AddSynergieTrigger(SynergieTrigger synergieTrigger);

        /// <summary>
        ///     Update a synergie trigger of the player.
        /// </summary>
        /// <param name="synergieTrigger"> to be updated.</param>
        public void UpdateSynergieTrigger(SynergieTrigger synergieTrigger);

        /// <summary>
        ///     Update a synergie of the player.
        /// </summary>
        /// <param name="synergie"> to be updated.</param>
        public void UpdateSynergie(Synergie synergie);

        /// <summary>
        ///     Create a new synergie for the player.
        /// </summary>
        /// <param name="synergie"> to create.</param>
        public void AddSynergie(Synergie synergie);

        /// <summary>
        ///     Get the active synergie effects for current units with active resources.
        ///     Can be used both for player and AI.
        /// </summary>
        /// <param name="units"> as a list of the currently relevant units.</param>
        /// <param name="resources"> as a list of the currently active resources.</param>
        /// <returns> a list of the active synergie effects.</returns>
        public List<SynergieEffect> GetActiveSynergieEffects(List<Unit> units, List<SynergieResource> resources);
    }
}