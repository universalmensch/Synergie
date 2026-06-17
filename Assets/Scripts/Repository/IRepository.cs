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
        /// Get the current Tasks.
        /// </summary>
        /// <returns> a list of all Tasks which need to be performed next.</returns>
        public List<Task> GetTasks();
        
        /// <summary>
        /// Add a new Unit.
        /// Mostly enemy units don't need to be saved, so this is mainly for new allied units.
        /// </summary>
        /// <param name="unit"> to be saved in the DB.</param>
        public void AddUnit(Unit unit);
        
        /// <summary>
        /// Add a task to be performed.
        /// </summary>
        /// <param name="task"> to be saved in the DB.</param>
        public void AddTask(Task task);
        
        /// <summary>
        /// Remove a task after it is completed.
        /// </summary>
        /// <param name="taskId"> of the task to be removed.</param>
        public void DeleteTask(int taskId);
        
        public void UpdateUnit(Unit unit);
        
        public void DeleteUnit(Unit unit);
        
        public SynergieEffect GetSynergieEffect();
        
        public void AddSynergieEffect(SynergieEffect effect);
    }
}