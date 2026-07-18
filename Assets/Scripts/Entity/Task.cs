using SQLite;

namespace Entity
{
    public class Task
    {
        public Task(TaskType taskType)
        {
            TaskType = taskType;
        }

        public Task()
        {
            // only for ORM, use parameterized constructor instead
        }

        [PrimaryKey] [AutoIncrement] public int ID { get; set; }

        public TaskType TaskType { get; set; }
    }
}