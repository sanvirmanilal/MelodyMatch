namespace MelodyMatch.Core.Models
{
    public class EasyDifficulty : Difficulty
    {
        public EasyDifficulty(int id = 1, string description = "Easy", int maxRows = 2, int maxColumns = 2) : base(id, description, maxRows, maxColumns)
        {
        }
    }
}