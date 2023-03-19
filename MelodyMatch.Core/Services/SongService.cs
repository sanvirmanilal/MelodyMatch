using MelodyMatch.Core.Models;

namespace MelodyMatch.Core.Services
{
    public class SongService : ISongService
    {
        private const string Unsolved = "Unsolved";
        private const string Solved = "Solved";
        private int[,] _songTiles;
        private string[,] _tilesStatus;
        private int _attempts = 0;

        private int _firstSelection { get; set; } = 0;
        public int[,] SongTiles { get => _songTiles; }
        private Tile _firstTileChosen { get; set; }

        private int _totalNumberOfAttempts { get; set; } = 0;

        private static readonly List<Song> _songs = new()
        {
            new Song(0, "Turkish", "songs/Turkish.mp3"),
            new Song(1, "ElRock", "songs/ElRock.mp3"),
            new Song(2, "Jazzy", "songs/Jazzy.mp3")
        };

        private static readonly List<Difficulty> _difficulties = new()
        {
            new EasyDifficulty(),
            new MediumDifficulty(),
            new MozartDifficulty()
        };

        public List<Difficulty> GetDifficulties()
        {
            return _difficulties;
        }

        public List<Song> GetSongs()
        {
            return _songs;
        }

        public void GenerateSongTiles(int selectedDifficultyId)
        {
            ResetGame();
            
            var difficulty = GetDifficulty(selectedDifficultyId);

            if (difficulty is null)
            {
                return;
            }

            int totalTiles = difficulty.MaxColumns * difficulty.MaxRows;
            _songTiles = new int[difficulty.MaxRows, difficulty.MaxColumns];
            ResetTilesStatus(difficulty);

            while (totalTiles > 0)
            {
                Random random = new();
                int row = random.Next(1, difficulty.MaxRows + 1) - 1; // Random.Next Range is non-inclusive
                int col = random.Next(1, difficulty.MaxColumns + 1) -1;
                if (_songTiles[row, col] == 0)
                {
                    _songTiles[row, col] = (int)Math.Ceiling(totalTiles / 2d);
                    totalTiles--;
                }
            }
        }

        private void ResetGame()
        {
            _totalNumberOfAttempts = 0;
            _attempts = 0;
            _firstSelection = 0;
        }

        private void ResetTilesStatus(Difficulty? difficulty)
        {
            _tilesStatus = new string[difficulty.MaxRows, difficulty.MaxColumns];
            for (int row = 0; row < difficulty.MaxRows; row++)
                for (int column = 0; column < difficulty.MaxColumns; column++)
                {
                    _tilesStatus[row, column] = Unsolved;
                }
        }

        private void SetSolvedTileStatus(int row, int column)
        {
            _tilesStatus[row, column] = Solved;
        }

        public Difficulty GetDifficulty(int selectedDifficultyId)
        {
            return _difficulties.FirstOrDefault(difficulty => difficulty.Id == selectedDifficultyId);
        }

        public Song GetSong(int selectedSongId)
        {
            return _songs.FirstOrDefault(song => song.Id == selectedSongId);
        }

        public string GetSongSnippetName(int selectedSongId, int row, int column)
        {
            return $"songs/{GetSong(selectedSongId).Description} ({_songTiles[row, column]}).mp3";
        }

        public bool CheckIfMatches(int row, int column)
        {
            var selectedTile = _songTiles[row, column];

            if (_attempts >= 2)
            {
                ResetTurn();
            }
            
            _attempts++;
            if (_attempts == 1)
            {
                SetSolvedTileStatus(row, column);
                _firstTileChosen = new Tile { Row = row, Column = column };
                _firstSelection = selectedTile;
                return false;
            }
            else if (_firstSelection != 0 && _firstSelection == selectedTile)
            {
                _totalNumberOfAttempts++; // Count every 2nd choice as a turn
                SetSolvedTileStatus(row, column);
                return true;
            }
            else 
            {
                SetUnSolvedTileStatus(_firstTileChosen.Row, _firstTileChosen.Column);
                return false;
            }
        }

        public bool DidYouWin()
        {
            bool winner = true;
            for (int row = 0; row < _tilesStatus.GetLength(0); row++)
                for (int column = 0; column < _tilesStatus.GetLength(1); column++)
                {
                    if (_tilesStatus[row, column] == Unsolved)
                    {
                        return false;
                    }
                }

            return winner;
        }

        private void SetUnSolvedTileStatus(int row, int column)
        {
            _tilesStatus[row, column] = Unsolved;
        }

        public string GetTileStatus(int row, int column)
        {
            return _tilesStatus[row, column];
        }

        public int GetTotalNumberOfAttempts()
        {
            return _totalNumberOfAttempts;
        }

        private void ResetTurn()
        {
            _attempts = 0;
            _firstSelection = 0;
        }
    }
}
