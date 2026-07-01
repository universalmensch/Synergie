using SQLite;

namespace Entity
{
    public class SynergieTrigger : ISelection
    {
        // TODO Condition

        public SynergieTrigger(SynergieType type, string description, string header)
        {
            Type = type;
            Description = description;
            Header = header;
            SynergieId = null;
        }

        public SynergieTrigger()
        {
        }

        [PrimaryKey] [AutoIncrement] public int Id { get; set; }

        public int? SynergieId { get; set; }

        public SynergieType Type { get; set; }

        public string Description { get; set; }

        public string Header { get; set; }

        public bool IsSelected => SynergieId != null;

        public string GetSelectionText()
        {
            return "type: " + Type + "\n" + Description;
        }

        public string GetSelectionHeadingText()
        {
            return Header;
        }
    }
}