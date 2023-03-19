using MelodyMatch.Core.Models;
using MelodyMatch.Core.Services;
using Microsoft.AspNetCore.Components;

namespace MelodyMatch.Pages
{
    public partial class Play : IComponent
    {
        [Inject]
        public ISongService SongService { get; set; } = null!;
        
        public GameConfig GameConfig { get; set; } = null!;

        public List<Song> SongList { get; set; } = null!;
        public List<Difficulty> DifficultyList { get; set; } = null!;

        public SongGrid SongGrid { get; set; } = null!;

        protected override void OnInitialized()
        {
            InitialiseGameConfig();
            LoadSongList();
        }

        private void InitialiseGameConfig()
        {
            GameConfig = new()
            {
                SelectedSongId = -1,
                SelectedDifficultyId = -1
            };
        }

        public void LoadSongList()
        {
            SongList = SongService.GetSongs();
            DifficultyList = SongService.GetDifficulties();
        }
    }
}
