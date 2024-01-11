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

        /// <summary>
        /// 上傳歌曲
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SongUpload(SongUploadDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name),"必須填寫歌曲名稱");

            var newSong = new Song
            {
                Name = dto.Name,
            };

            _context.Song.Add(newSong);

            var singerResult = _context.Singer.AsNoTracking().Where(m => dto.SingerIds.Contains(m.Id)).ToList();

            var albumResult = _context.Album.AsNoTracking().Where(m => dto.AlbumIds.Contains(m.Id)).ToList();

            using (var trna = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();

                    if (albumResult != null && albumResult.Any())
                    {
                        foreach(var album in albumResult)
                        {
                            _context.SongAndAlbumRelation.Add(new SongAndAlbumRelation
                            {
                                AlbumId = album.Id,
                                SongId = newSong.Id,
                                AlbumName = album.Name
                            });
                        }
                    }

                    if (singerResult != null && singerResult.Any())
                    {
                        foreach (var singer in singerResult)
                        {
                            _context.SingerAndSongRelation.Add(new SingerAndSongRelation
                            {
                                SingerId = singer.Id,
                                SongId = newSong.Id,
                                SingerName = singer.DisplayName
                            });
                        }
                    }

                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    trna.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 刪除歌曲
        /// </summary>
        /// <param name="songId"></param>
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

        /// <summary>
        /// 編輯歌曲
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void SongEdit(SongEditDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name), "必須填寫歌曲名稱");

            var songReuslt = _context.Song.FirstOrDefault(m => m.Id == dto.Id);

            if (songReuslt == null)
                throw new ArgumentNullException(nameof(dto.Id), "找不到歌曲");

            songReuslt.Note = dto.Note;
            songReuslt.Name = dto.Name;

            var songAndAlbumRelation = _context.SongAndAlbumRelation.Where(m => m.SongId == dto.Id).ToList();
            var singerAndSongRelation = _context.SingerAndSongRelation.Where(m => m.SongId == dto.Id).ToList();

            if (songAndAlbumRelation != null && songAndAlbumRelation.Any())
                _context.SongAndAlbumRelation.RemoveRange(songAndAlbumRelation);

            if (singerAndSongRelation != null && singerAndSongRelation.Any())
                _context.SingerAndSongRelation.RemoveRange(singerAndSongRelation);

            _context.SaveChanges();
        }
    }
}