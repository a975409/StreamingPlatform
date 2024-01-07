using Microsoft.EntityFrameworkCore;
using StreamingPlatform.Domain.Contract;
using StreamingPlatform.Domain.Models;

namespace StreamingPlatform.Domain
{
    public class SongService
    {
        private readonly StreamingPlatformContext _context;

        public SongService(StreamingPlatformContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 取得歌曲列表
        /// </summary>
        /// <param name="albumName"></param>
        /// <param name="songName"></param>
        /// <returns></returns>
        public IEnumerable<SongSearchDto> GetSongList(string albumName, string songName)
        {
            var result = from s in _context.Song
                         join p in _context.SingerAndSongRelation on s.Id equals p.SongId into ps
                         from p in ps.DefaultIfEmpty()
                         join a in _context.SongAndAlbumRelation on s.Id equals a.SongId into qs
                         from a in qs.DefaultIfEmpty()
                         select new { s, p, a };

            if (!string.IsNullOrEmpty(albumName))
                result = result.Where(m => m.a.AlbumName.Contains(albumName));

            if (!string.IsNullOrEmpty(songName))
                result = result.Where(m => m.s.Name.Contains(songName));

            result = result.AsNoTracking();

            List<SongSearchDto> songSearchResults = new List<SongSearchDto>();

            foreach (var item in result)
            {
                var searchResult = songSearchResults.FirstOrDefault(m => m.Id == item.s.Id);

                if (searchResult != null)
                {
                    if (!string.IsNullOrEmpty(item.a.AlbumName))
                        searchResult.AlbumSearchResults.Add(new AlbumSearchDto
                        {
                            Id = item.a.AlbumId,
                            Name = item.a.AlbumName
                        });

                    if (!string.IsNullOrEmpty(item.p.SingerName))
                        searchResult.SingerSearchResults.Add(new SingerSearchDto
                        {
                            Id = item.p.SingerId,
                            Name = item.p.SingerName
                        });
                }
                else
                {
                    songSearchResults.Add(new SongSearchDto
                    {
                        Id = item.s.Id,
                        Name = item.s.Name,
                        AlbumSearchResults = new List<AlbumSearchDto> { new AlbumSearchDto {
                     Id= item.a.AlbumId,
                      Name=item.a.AlbumName
                    } },
                        SingerSearchResults = new List<SingerSearchDto> { new SingerSearchDto {
                      Name=item.p.SingerName,
                       Id=item.p.SingerId
                     } }
                    });
                }
            }

            return songSearchResults;
        }

        public void SongUpload(SongUploadDto dto)
        {
            var singerResult = _context.Singer.AsNoTracking().FirstOrDefault(m => m.Id == dto.SingerId);

            if (singerResult == null)
                throw new ArgumentNullException(nameof(dto.SingerId), "此歌手不存在");

            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name),"必須填寫歌曲名稱");

            var newSong = new Song
            {
                Name = dto.Name,
            };

            _context.Song.Add(newSong);

            Album? album = null;

            if (dto.albumId != null)
            {
                album = _context.Album.AsNoTracking().FirstOrDefault(m => m.Id == dto.albumId);

                if (album == null)
                    throw new ArgumentOutOfRangeException(nameof(dto.albumId), "指定的專輯不存在");
            }
            else if(dto.albumId == null && !string.IsNullOrEmpty(dto.AlbumName))
            {
                album = new Album
                {
                    Name = dto.AlbumName
                };

                _context.Album.Add(album);
            }

            using (var trna = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();

                    if (album != null)
                    {
                        _context.SongAndAlbumRelation.Add(new SongAndAlbumRelation
                        {
                            AlbumId = album.Id,
                            SongId = newSong.Id,
                            AlbumName = album.Name
                        });
                    }

                    _context.SingerAndSongRelation.Add(new SingerAndSongRelation
                    {
                        SingerId = singerResult.Id,
                        SongId = newSong.Id,
                        SingerName = singerResult.DisplayName
                    });

                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    trna.Rollback();
                    throw;
                }
            }
        }

        public void SongRemove(int songId)
        {
            var songReuslt = _context.Song.FirstOrDefault(m => m.Id == songId);

            if (songReuslt != null)
                _context.Song.Remove(songReuslt);

            var songAndAlbumRelationList = _context.SongAndAlbumRelation.Where(m => m.SongId == songId).ToList();

            if (songAndAlbumRelationList.Any())
                _context.SongAndAlbumRelation.RemoveRange(songAndAlbumRelationList);

            var singerAndSongRelationList = _context.SingerAndSongRelation.Where(m => m.SongId == songId).ToList();

            if (singerAndSongRelationList.Any())
                _context.SingerAndSongRelation.RemoveRange(singerAndSongRelationList);

            _context.SaveChanges();
        }
    }
}