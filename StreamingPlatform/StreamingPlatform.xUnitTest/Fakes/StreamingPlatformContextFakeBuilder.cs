using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StreamingPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.xUnitTest.Fakes
{
    public class StreamingPlatformContextFakeBuilder : IDisposable
    {
        private readonly StreamingPlatformContextFake _context = new StreamingPlatformContextFake();
        private EntityEntry<Album> _album;
        private EntityEntry<Singer> _singer;
        private EntityEntry<SingerAndSongRelation> _singerAndSongRelation;
        private EntityEntry<Song> _song;
        private EntityEntry<SongAndAlbumRelation> _ongAndAlbumRelation;
        private EntityEntry<Member> _member;
        private EntityEntry<PlayList> _playList;

        public StreamingPlatformContextFakeBuilder WithMember()
        {
            _context.Member.Add(new Member
            {
                AccountNo = "aA975409",
                Pwd = "aA975409",
                DisplayName = "a975409",
                Email = "a975409@gmail.com",
                GoogleOta = false,
                Id = 1,
                Name = "a975409",
                PlayList = new List<PlayList> { new PlayList
                {
                    Id = 1,
                    MemberId = 1,
                    Name = "最愛歌單",
                    PlaylistItem = new List<PlaylistItem>
                     {
                         new PlaylistItem
                         {
                              Id=1,
                               SongId = 1,
                         },
                         new PlaylistItem
                         {
                              Id=2,
                               SongId = 2,
                         },
                         new PlaylistItem
                         {
                              Id=3,
                               SongId = 3,
                         }
                     }
                }}
            });

            return this;
        }

        public StreamingPlatformContextFakeBuilder WithSinger()
        {
            _context.Singer.Add(new Singer
            {
                Id = 1,
                Name = "amazarashi",
                AccountNo = "A5516amazarashia",
                DisplayName = "amazarashi",
                Email = "amazarashi@gmail.com",
                GoogleOta = false,
                Pwd = "amazarashiamazarashiamazarashi"
            });

            return this;
        }

        public StreamingPlatformContextFakeBuilder WithAlbum()
        {
            _context.Album.Add(new Album
            {
                Id = 1,
                Name = "eternal city",
                Note = ""
            });

            return this;
        }

        public StreamingPlatformContextFakeBuilder WithSong()
        {
            _context.Song.Add(new Song
            {
                Id = 1,
                Name = "inhuman empathy",
                Note = ""
            });

            _context.Song.Add(new Song
            {
                Id = 2,
                Name = "shita wo muite arukou",
                Note = ""
            });

            _context.Song.Add(new Song
            {
                Id = 3,
                Name = "antinomy",
                Note = ""
            });

            return this;
        }

        public StreamingPlatformContextFakeBuilder WithRelation()
        {
            _context.SingerAndSongRelation.Add(new SingerAndSongRelation
            {
                SingerId = 1,
                SingerName = "amazarashi",
                SongId = 1,
                Id = 1
            });

            _context.SingerAndSongRelation.Add(new SingerAndSongRelation
            {
                SingerId = 1,
                SingerName = "amazarashi",
                SongId = 2,
                Id = 2
            });

            _context.SingerAndSongRelation.Add(new SingerAndSongRelation
            {
                SingerId = 1,
                SingerName = "amazarashi",
                SongId = 3,
                Id = 3
            });

            _context.SongAndAlbumRelation.Add(new SongAndAlbumRelation
            {
                AlbumId = 1,
                AlbumName = "eternal city",
                SongId = 1,
                Id = 1
            });

            _context.SongAndAlbumRelation.Add(new SongAndAlbumRelation
            {
                AlbumId = 1,
                AlbumName = "eternal city",
                SongId = 2,
                Id = 2
            });

            _context.SongAndAlbumRelation.Add(new SongAndAlbumRelation
            {
                AlbumId = 1,
                AlbumName = "eternal city",
                SongId = 3,
                Id = 3
            });

            return this;
        }

        public StreamingPlatformContextFake Build()
        {
            _context.SaveChanges();
            return _context;
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}