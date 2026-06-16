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

        public SynergieEffect(SynergieType type, int value)
        {
            Type = type;
            Value = value;
        }
        
        public SynergieEffect(){}
    }
}