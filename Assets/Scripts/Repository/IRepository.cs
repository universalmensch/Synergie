using System.Collections.Generic;
using Entity;

namespace Repository
{
    /// <summary>
    ///     Interface to access DB operations.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        ///     Get all units which belong to the player.
        /// </summary>
        /// <returns> A list of the units.</returns>
        public List<Unit> GetAlliedUnits();

        /// <summary>
        ///     Get the count of the units which belong to the player.
        /// </summary>
        /// <returns> count of units.</returns>
        public int GetAlliedUnitsCount();

        /// <summary>
        ///     Get the current Tasks.
        /// </summary>
        /// <returns> a list of all Tasks which need to be performed next.</returns>
        public List<Task> GetTasks();

        /// <summary>
        ///     Add a new Unit.
        ///     Mostly enemy units don't need to be saved, so this is mainly for new allied units.
        /// </summary>
        /// <param name="unit"> to be saved in the DB.</param>
        public void AddUnit(Unit unit);

        /// <summary>
        ///     Add a task to be performed.
        /// </summary>
        /// <param name="task"> to be saved in the DB.</param>
        public void AddTask(Task task);

        /// <summary>
        ///     Remove a task after it is completed.
        /// </summary>
        /// <param name="taskId"> of the task to be removed.</param>
        public void DeleteTask(int taskId);

        public void UpdateUnit(Unit unit);

        /// <summary>
        ///     Removes a unit from the DB. Used for when a unit of the player dies.
        /// </summary>
        /// <param name="unit"> to delete.</param>
        public void DeleteUnit(Unit unit);

        /// <summary>
        ///     Save a new synergie resource.
        /// </summary>
        /// <param name="synergieResource"> to be saved in the DB.</param>
        public void AddSynergieResource(SynergieResource synergieResource);

        /// <summary>
        ///     Save a new synergie trigger.
        /// </summary>
        /// <param name="synergieTrigger"> to be saved in the DB.</param>
        public void AddSynergieTrigger(SynergieTrigger synergieTrigger);

        /// <summary>
        ///     Get the synergie resources of the player.
        /// </summary>
        /// <returns> a list of the synergie resources.</returns>
        public List<SynergieResource> GetSynergieResources();

        /// <summary>
        ///     Get the Synergie triggers of the player.
        /// </summary>
        /// <returns> a list of the synergie triggers.</returns>
        public List<SynergieTrigger> GetSynergieTriggers();

        /// <summary>
        ///     Get the synergies of the player.
        /// </summary>
        /// <returns> a list of the synergies.</returns>
        public List<Synergie> GetSynergies();

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
        ///     Update a synergie resource of the player.
        /// </summary>
        /// <param name="synergieResource"> to be updated.</param>
        public void UpdateSynergieResource(SynergieResource synergieResource);

        /// <summary>
        ///     Update a synergie trigger of the player.
        /// </summary>
        /// <param name="synergieTrigger"> to be updated.</param>
        public void UpdateSynergieTrigger(SynergieTrigger synergieTrigger);

        /// <summary>
        ///     Close DB connection on application quit.
        /// </summary>
        public void Dispose();

        /// <summary>
        ///     Get all possible synergie effects.
        /// </summary>
        /// <returns> a list of the effects.</returns>
        public List<SynergieEffect> GetSynergieEffects();

        /// <summary>
        ///     Update a object in the DB.
        /// </summary>
        /// <param name="obj"> to update.</param>
        public void Update(object obj);

        /// <summary>
        ///     Get the current resources of the player.
        /// </summary>
        /// <returns> the resources.</returns>
        public Resources GetResources();
    }
}