using MelodyMatch.Core.Models;
using MelodyMatch.Core.Services;
using Microsoft.AspNetCore.Components;

namespace MelodyMatch.Pages
{
    public partial class SongGrid : IComponent
    {
        private Popup _popup;

        [Inject]
        public ISongService SongService { get; set; }

        [Parameter]
        public int SelectedSongId { get; set; }

        [Parameter]
        public int SelectedDifficultyId { get; set; }

        public Song SelectedSong { get; set; }
        public Difficulty SelectedDifficulty { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            SongService.GenerateSongTiles(SelectedDifficultyId);
            SelectedSong = SongService.GetSong(SelectedSongId);
            SelectedDifficulty = SongService.GetDifficulty(SelectedDifficultyId);
            StateHasChanged();
        }

        public string GetTileId(int row, int column)
        {
            return $"tile{row}{column}";
        }

        public string GetSongSnippetName(int row, int column)
        {
            return SongService.GetSongSnippetName(SelectedSongId, row, column);
        }

        public bool CheckIfMatches(int row, int column)
        {
            if (SongService.CheckIfMatches(row, column))
            {
                if (SongService.DidYouWin())
                {
                    _popup.Show($"You did it in {SongService.GetTotalNumberOfAttempts()} attempts!", SelectedSong.FullSongPath, "Congratulations!");
                }
            }
            return true;
        }

        public bool GetTileStatus(int row, int column)
        {
            return SongService.GetTileStatus(row, column) == "Solved";
        }

        public string GetTileText(int row, int column)
        {
            if (SongService.GetTileStatus(row, column) == "Solved")
            {
                return "✔";
            }
            else
            {
                return "▶";
            }
        }
    }
}
