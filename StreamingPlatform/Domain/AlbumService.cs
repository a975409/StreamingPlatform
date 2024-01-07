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