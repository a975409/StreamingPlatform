using StreamingPlatform.Domain;
using StreamingPlatform.Domain.Contract;
using StreamingPlatform.xUnitTest.Fakes;

namespace StreamingPlatform.xUnitTest
{
    public class AnonymousUnitTest
    {
        private readonly StreamingPlatformContextFake _context = new StreamingPlatformContextFakeBuilder()
                                                                .WithAlbum()
                                                                .WithSinger()
                                                                .WithSong()
                                                                .WithRelation()
                                                                .WithMember()
                                                                .Build();

        [Fact]
        public void GetAlbumList_EmptyAlbumName_ReturnData()
        {
            string albumName = "";
            AlbumService service = new AlbumService(_context);
            IEnumerable<AlbumSearchResult> albumResults = service.GetAlbumList(albumName);

            Assert.True(albumResults.Any());
        }

        [Fact]
        public void GetAlbumList_AlbumName_ReturnData()
        {
            string albumName = "eternal city";
            AlbumService service = new AlbumService(_context);
            IEnumerable<AlbumSearchResult> albumResults = service.GetAlbumList(albumName);

            Assert.True(albumResults.Any());
        }

        [Fact]
        public void GetAlbumList_AlbumName_ReturnNoData()
        {
            string albumName = "asfafs";
            AlbumService service = new AlbumService(_context);
            IEnumerable<AlbumSearchResult> albumResults = service.GetAlbumList(albumName);

            Assert.True(albumResults.Any() == false);
        }

        [Fact]
        public void GetSongList_EmptyName_ReturnData()
        {
            string albumName = "";
            string songName = "";

            SongService service = new SongService(_context);
            IEnumerable<SongSearchResult> songResults = service.GetSongList(albumName: albumName, songName);

            Assert.True(songResults.Any());
        }

        [Fact]
        public void GetSongList_Name_ReturnData()
        {
            string albumName = "eternal city";
            string songName = "inhuman empathy";

            SongService service = new SongService(_context);
            IEnumerable<SongSearchResult> songResults = service.GetSongList(albumName: albumName, songName);

            Assert.True(songResults.Any());
        }

        [Fact]
        public void GetSongList_AlbumName_ReturnData()
        {
            string albumName = "eternal city";
            string songName = "";

            SongService service = new SongService(_context);
            IEnumerable<SongSearchResult> songResults = service.GetSongList(albumName: albumName, songName);

            Assert.True(songResults.Any());
        }

        [Fact]
        public void GetSongList_SongName_ReturnData()
        {
            string albumName = "";
            string songName = "inhuman empathy";

            SongService service = new SongService(_context);
            IEnumerable<SongSearchResult> songResults = service.GetSongList(albumName: albumName, songName);

            Assert.True(songResults.Any());
        }

        [Fact]
        public void GetSongList_Name_ReturnNoData()
        {
            string albumName = "asfasf";
            string songName = "safsaa";

            SongService service = new SongService(_context);
            IEnumerable<SongSearchResult> songResults = service.GetSongList(albumName: albumName, songName);

            Assert.True(songResults.Any() == false);
        }

        [Fact]
        public void GetSingerList_EmptySingerName_ReturnData()
        {
            string singerName = "";

            SingerService service = new SingerService(_context);
            IEnumerable<SingerSearchResult> singerResults = service.GetSingerList(singerName);

            Assert.True(singerResults.Any());
        }

        [Fact]
        public void GetSingerList_SingerName_ReturnData()
        {
            string singerName = "amazarashi";

            SingerService service = new SingerService(_context);
            IEnumerable<SingerSearchResult> singerResults = service.GetSingerList(singerName);

            Assert.True(singerResults.Any());
        }

        [Fact]
        public void GetSingerList_SingerName_ReturnNoData()
        {
            string singerName = "gasagasg";

            SingerService service = new SingerService(_context);
            IEnumerable<SingerSearchResult> singerResults = service.GetSingerList(singerName);

            Assert.True(singerResults.Any() == false);
        }
    }
}