using SQLite;

namespace Entity
{
    public class SynergieResource : ISelection
    {
        public SynergieResource(SynergieType synergieType, int value, string description, string header)
        {
            SynergieType = synergieType;
            Value = value;
            Description = description;
            Header = header;
            Level = 1;
            SynergieId = null;
        }

        public SynergieResource()
        {
        }

        [PrimaryKey] [AutoIncrement] public int Id { get; set; }

        public SynergieType SynergieType { get; set; }

        public int? SynergieId { get; set; }

        public int Value { get; set; }

        public int Level { get; set; }

        public string Description { get; set; }

        public string Header { get; set; }

        public bool IsSelected => SynergieId != null;

        public int UpgradeCost => Level * 50;

        public string GetSelectionText()
        {
            return "type: " + SynergieType + "\n" + Description;
        }

        public string GetSelectionHeadingText()
        {
            return Header;
        }

        public void Upgrade()
        {
            Level++;
            Value++;
        }
    }
}