using StreamingPlatform.Domain;
using StreamingPlatform.Domain.Models;
using StreamingPlatform.xUnitTest.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.xUnitTest
{
    public class MemberPlaylistUnitTest
    {
        private readonly StreamingPlatformContextFake _context = new StreamingPlatformContextFakeBuilder()
                                                                .WithAlbum()
                                                                .WithSinger()
                                                                .WithSong()
                                                                .WithRelation()
                                                                .WithMember()
                                                                .Build();

        //建立播放清單
        [Fact]
        public void CreatePlaylist_EmptyName_ReturnArgumentNullException()
        {
            string name = "";
            int[] songIds = { };
            int memberId = 1;

            var service = new PlaylistService(_context);

            Assert.Throws<ArgumentNullException>(() => service.CreatePlaylist(name, songIds, memberId));
        }

        [Fact]
        public void CreatePlaylist_EmptySongIds_ReturnArgumentNullException()
        {
            string name = "測試清單";
            int[] songIds = { };
            int memberId = 1;

            var service = new PlaylistService(_context);

            Assert.Throws<ArgumentNullException>(() => service.CreatePlaylist(name, songIds, memberId));
        }

        [Fact]
        public void CreatePlaylist_invalidSongIds_ReturnArgumentOutOfRangeException()
        {
            string name = "測試清單";
            int[] songIds = { 4, 5, 6 };
            int memberId = 1;

            var service = new PlaylistService(_context);

            Assert.Throws<ArgumentOutOfRangeException>(() => service.CreatePlaylist(name, songIds, memberId));
        }

        [Fact]
        public void CreatePlaylist_invalidMemberId_ReturnArgumentNullException()
        {
            string name = "測試清單";
            int[] songIds = { 1, 2, 3 };
            int memberId = -1;

            var service = new PlaylistService(_context);

            Assert.Throws<ArgumentNullException>(() => service.CreatePlaylist(name, songIds, memberId));
        }

        [Fact]
        public void CreatePlaylist_ValidParam_ReturnSuccess()
        {
            string name = "測試清單";
            int[] songIds = { 1, 2, 3 };
            int memberId = 1;

            var service = new PlaylistService(_context);

            Assert.True(service.CreatePlaylist(name, songIds, memberId));
        }

        //編輯播放清單
        [Fact]
        public void EditPlaylist_EmptyName_ReturnArgumentNullException()
        {
            string name = "";
            int[] songIds = { 1, 2, 3 };
            int playlistId = 1;

            var service = new PlaylistService(_context);

            Assert.Throws<ArgumentNullException>(() => service.EditPlaylist(name, songIds, playlistId));
        }

        [Fact]
        public void EditPlaylist_EmptySongIds_ReturnArgumentNullException()
        {
            string name = "測試清單";
            int[] songIds = { };
            int playlistId = 1;

            var service = new PlaylistService(_context);

            Assert.Throws<ArgumentNullException>(() => service.EditPlaylist(name, songIds, playlistId));
        }

        [Fact]
        public void EditPlaylist_invalidSongIds_ReturnArgumentOutOfRangeException()
        {
            string name = "測試清單";
            int[] songIds = { 4, 5, 6 };
            int playlistId = 1;

            var service = new PlaylistService(_context);

            Assert.Throws<ArgumentOutOfRangeException>(() => service.EditPlaylist(name, songIds, playlistId));
        }

        [Fact]
        public void EditPlaylist_PlaylistIdNoExist_ReturnArgumentNullException()
        {
            string name = "測試清單";
            int[] songIds = { 1, 2, 3 };
            int playlistId = -1;

            var service = new PlaylistService(_context);

            Assert.Throws<ArgumentNullException>(() => service.EditPlaylist(name, songIds, playlistId));
        }

        [Fact]
        public void EditPlaylist_ValidParam_ReturnSuccess()
        {
            string name = "測試清單";
            int[] songIds = { 1, 3 };
            int playlistId = 1;

            var service = new PlaylistService(_context);

            Assert.True(service.EditPlaylist(name, songIds, playlistId));
        }

        //刪除播放清單
        [Fact]
        public void DeletePlaylist_ValidParam_ReturnSuccess()
        {
            int playlistId = 1;

            var service = new PlaylistService(_context);

            Assert.True(service.DeletePlaylist(playlistId));
        }

        [Fact]
        public void GetPlaylist_ConditionsNotMet_RetuenNoData()
        {
            string playlistName = "Nodata";
            int membarId = -1;

            var service = new PlaylistService(_context);

            Assert.True(service.SearchPlaylist(membarId, playlistName).Any() == false);
        }

        [Fact]
        public void GetPlaylist_ConditionsMet_RetuenData()
        {
            string playlistName = "最愛歌單";
            int membarId = 1;

            var service = new PlaylistService(_context);

            Assert.True(service.SearchPlaylist(membarId, playlistName).Any());
        }

        [Fact]
        public void GetPlaylist_PlaylistNameIsEmpty_RetuenData()
        {
            string playlistName = "";
            int membarId = 1;

            var service = new PlaylistService(_context);

            Assert.True(service.SearchPlaylist(membarId, playlistName).Any());
        }
    }
}