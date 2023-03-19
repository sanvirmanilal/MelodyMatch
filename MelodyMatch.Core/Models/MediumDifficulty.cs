namespace MelodyMatch.Core.Models
{
    public class MediumDifficulty : Difficulty
    {
        public MediumDifficulty(int id = 2, string description = "Medium", int maxRows = 2, int maxColumns = 3) : base(id, description, maxRows, maxColumns)
        {
        }
    }
}