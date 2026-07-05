using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace Entity
{
    public class SynergieEffect
    {
        public SynergieEffect()
        {
        }

        public SynergieEffect(Dictionary<SynergieType, int> requirements, Effect effect, int level)
        {
            SetRequirements(requirements);
            Effect = effect;
            Level = level;
        }

        public Effect Effect { get; set; }

        public int Level { get; set; }

        [PrimaryKey] [AutoIncrement] public int Id { get; set; }

        public string Requirements { get; set; }

        public Dictionary<SynergieType, int> GetRequirements()
        {
            return JsonConvert.DeserializeObject<Dictionary<SynergieType, int>>(Requirements);
        }

        public void SetRequirements(Dictionary<SynergieType, int> requirements)
        {
            Requirements = JsonConvert.SerializeObject(requirements);
        }
    }
}