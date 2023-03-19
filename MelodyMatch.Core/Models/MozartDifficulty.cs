namespace MelodyMatch.Core.Models
{
    public class MozartDifficulty : Difficulty
    {
        public MozartDifficulty(int id = 3, string description = "Mozart", int maxRows = 4, int maxColumns = 3) : base(id, description, maxRows, maxColumns)
        {
        }
    }
}