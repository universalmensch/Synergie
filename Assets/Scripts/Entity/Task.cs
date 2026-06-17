using SQLite;

namespace Entity
{
    public class Task
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        public TaskType TaskType { get; set; }
        
        public int TaskNumber { get; set; }

        public Task(TaskType taskType,  int taskNumber)
        {
            TaskType = taskType;
            TaskNumber = taskNumber;
        }

        public Task()
        {
            // only for ORM, use parameterized constructor instead
        }
    }
}