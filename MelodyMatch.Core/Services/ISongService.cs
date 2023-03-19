using MelodyMatch.Core.Models;

namespace MelodyMatch.Core.Services
{
    public interface ISongService
    {
        bool CheckIfMatches(int row, int column);
        void GenerateSongTiles(int selectedDifficultyId);
        List<Difficulty> GetDifficulties();
        List<Song> GetSongs();
        string GetSongSnippetName(int selectedSongId, int row, int column);
        Song GetSong(int selectedSongId);
        Difficulty GetDifficulty(int selectedDifficultyId);
        string GetTileStatus(int row, int column);
        int GetTotalNumberOfAttempts();
        bool DidYouWin();
    }
}
