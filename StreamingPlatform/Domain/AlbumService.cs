using Microsoft.EntityFrameworkCore;
using StreamingPlatform.Domain.Contract;
using StreamingPlatform.Domain.Models;

namespace StreamingPlatform.Domain
{
    public class AlbumService
    {
        private readonly StreamingPlatformContext _context;

        public AlbumService(StreamingPlatformContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 建立專輯
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void CreateAlbum(AlbumCreateDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name), "專輯名稱必填");

            _context.Album.Add(new Album
            {
                Name = dto.Name,
                Note = dto.Note
            });

            _context.SaveChanges();
        }

        /// <summary>
        /// 編輯專輯
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void EditAlbum(AlbumEditDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name), "專輯名稱必填");

            var album = _context.Album.FirstOrDefault(m => m.Id == dto.Id);

            if (album == null)
                throw new ArgumentNullException(nameof(dto.Id), "該專輯不存在");

            album.Name = dto.Name;
            album.Note= dto.Note;

            _context.SaveChanges();
        }

        /// <summary>
        /// 刪除專輯
        /// </summary>
        /// <param name="albumId"></param>
        public void RemoveAlbum(int albumId)
        {
            var albnum = _context.Album.FirstOrDefault(m => m.Id == albumId);
            var songAndAlbumRelation = _context.SongAndAlbumRelation.Where(m => m.AlbumId == albumId).ToList();

            if (albnum != null)
                _context.Album.Remove(albnum);

            if (songAndAlbumRelation != null)
                _context.SongAndAlbumRelation.RemoveRange(songAndAlbumRelation);

            _context.SaveChanges();
        }

        /// <summary>
        /// 取得專輯列表
        /// </summary>
        /// <param name="albumName"></param>
        /// <returns></returns>
        public IEnumerable<AlbumSearchDto> GetAlbumList(string albumName)
        {
            var result = _context.Album.AsNoTracking().Select(m => m);

            if (!string.IsNullOrEmpty(albumName))
                result = result.Where(m => m.Name.Contains(albumName));

            return result.AsNoTracking().Select(m => new AlbumSearchDto
            {
                Id = m.Id,
                Name = m.Name
            });
        }
    }
}