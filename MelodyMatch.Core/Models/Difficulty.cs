namespace MelodyMatch.Core.Models
{
    public class Difficulty
    {
        public Difficulty(int id, string description, int maxRows, int maxColumns)
        {
            Id = id;
            Description = description;
            MaxRows = maxRows;
            MaxColumns= maxColumns;
        }

        public int Id { get; }
        public string Description { get; } = null!;

        public int MaxRows { get; }
        public int MaxColumns { get; }
    }
}