using System.Collections.Generic;

namespace Entity
{
    public class Synergie
    {
        private List<SynergieEffect> _effects;
        private List<SynergieTrigger> _triggers;

        public Synergie()
        {
            _effects = new List<SynergieEffect>();
            _triggers = new List<SynergieTrigger>();
        }
    }
}