using SQLite;

namespace Entity
{
    public class SynergieEffect
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public SynergieType Type
        {
            get;
            set;
        }
        
        public int Value { get; set; }
        
        public int Level { get; set; }
        
        public string Description { get; set; }
        
        public string Header { get; set; }
        
        public bool IsSelected { get; set; }

        public SynergieEffect(SynergieType type, int value,  string description, string header)
        {
            Type = type;
            Value = value;
            Description = description;
            Header = header;
            Level = 1;
            IsSelected = false;
        }
        
        public SynergieEffect(){}

        public void Upgrade()
        {
            Level++;
            Value++;
        }

        public int UpgradeCost => Level * 50;

    }
}