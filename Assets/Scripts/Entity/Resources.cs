using SQLite;

namespace Entity
{
    public class Resources
    {
        public Resources()
        {
            SynergiePoints = 0;
        }

        [PrimaryKey] [AutoIncrement] public int Id { get; set; }

        public int SynergiePoints { get; set; }
    }
}