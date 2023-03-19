namespace MelodyMatch.Core.Models
{
    public class Song
    {
        public Song(int id, string description, string fullSongPath)
        {
            Id = id;
            Description = description;
            FullSongPath = fullSongPath;
        }

        public int Id { get; }
        public string Description { get; }
        public string FullSongPath { get; }
    }
}