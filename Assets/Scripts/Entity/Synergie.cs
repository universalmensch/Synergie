using System.Collections.Generic;
using SQLite;

namespace Entity
{
    public class Synergie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public List<SynergieEffect> Effects { get; set; }
        public List<SynergieTrigger> Triggers { get; set; }

        public Synergie()
        {
            Effects = new List<SynergieEffect>();
            Triggers = new List<SynergieTrigger>();
        }
    }
}