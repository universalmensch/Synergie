using System.Collections.Generic;
using SQLite;

namespace Entity
{
    public class Synergie
    {
        public Synergie()
        {
            Effects = new List<SynergieEffect>();
            Triggers = new List<SynergieTrigger>();
        }

        [PrimaryKey] [AutoIncrement] public int Id { get; set; }

        [Ignore] public List<SynergieEffect> Effects { get; set; }

        [Ignore] public List<SynergieTrigger> Triggers { get; set; }
    }
}