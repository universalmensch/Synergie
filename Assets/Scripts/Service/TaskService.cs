using System.Collections.Generic;
using Entity;
using Repository;

namespace Service
{
    public class TaskService :  ITaskService
    {
        private readonly IRepository _repository;
        
        public TaskService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Task> GetTasks()
        {
            return _repository.GetTasks();
        }

        public void AddTask(Task task)
        {
            _repository.AddTask(task);
        }

        public void DeleteTask(int taskId)
        {
            _repository.DeleteTask(taskId);
        }
    }
}