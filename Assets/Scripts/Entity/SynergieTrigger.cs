using SQLite;

namespace Entity
{
    public class SynergieTrigger
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public SynergieType Type
        {
            get;
            set;
        }
        
        public string Description { get; set; }
        
        public string Header { get; set; }
        
        public bool IsSelected { get; set; }
        
        // TODO Condition

        public SynergieTrigger(SynergieType type, string description, string header)
        {
            Type = type;
            Description = description;
            Header = header;
            IsSelected = false;
        }
        
        public SynergieTrigger(){}
    }
}