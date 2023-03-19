using FluentAssertions;
using MelodyMatch.Core.Models;
using MelodyMatch.Core.Services;
using Xunit;

namespace MelodyMatch.Tests
{
    public class SongServiceTests
    {
        private SongService _songService;

        public SongServiceTests()
        {
            _songService = new();
        }

        [Fact]
        public void GetTileIds_EasyDifficulty()
        {
            _songService.GenerateSongTiles(new EasyDifficulty().Id);
            _songService.SongTiles[0, 0].Should().NotBe(0);
        }

        [Fact]
        public void GetTileIds_DefaultDifficulty()
        {
            _songService.GenerateSongTiles(-1);
            _songService.SongTiles.Length.Should().Be(0);
        }

        [Fact]
        public void GetSongSnippetName_Easy_Jazzy()
        {
            _songService.GenerateSongTiles(new EasyDifficulty().Id);
            var songSnippetName =  _songService.GetSongSnippetName(1, 0, 1);
            songSnippetName.Should().Contain("Jazzy");
        }
    }
}