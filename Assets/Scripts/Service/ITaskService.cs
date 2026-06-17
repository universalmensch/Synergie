using System.Collections.Generic;
using Entity;
using NUnit.Framework;

namespace Service
{
    public interface ITaskService
    {
        /// <summary>
        /// Get the current Tasks.
        /// </summary>
        /// <returns> a list of all Tasks which need to be performed next.</returns>
        public List<Task> GetTasks();
        
        /// <summary>
        /// Add a task to be performed.
        /// </summary>
        /// <param name="task"> the Task to save.</param>
        public void AddTask(Task task);
        
        /// <summary>
        /// Remove a task after it is completed.
        /// </summary>
        /// <param name="taskId"> of the task to be removed.</param>
        public void DeleteTask(int  taskId);
    }
}