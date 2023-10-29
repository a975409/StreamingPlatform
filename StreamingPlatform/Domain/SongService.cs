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

        public IEnumerable<SongSearchResult> GetSongList(string albumName, string songName)
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

            List<SongSearchResult> songSearchResults = new List<SongSearchResult>();

            foreach (var item in result)
            {
                var searchResult = songSearchResults.FirstOrDefault(m => m.Id == item.s.Id);

                if (searchResult != null)
                {
                    if (!string.IsNullOrEmpty(item.a.AlbumName))
                        searchResult.AlbumSearchResults.Add(new AlbumSearchResult
                        {
                            Id = item.a.AlbumId,
                            Name = item.a.AlbumName
                        });

                    if (!string.IsNullOrEmpty(item.p.SingerName))
                        searchResult.SingerSearchResults.Add(new SingerSearchResult
                        {
                            Id = item.p.SingerId,
                            Name = item.p.SingerName
                        });
                }
                else
                {
                    songSearchResults.Add(new SongSearchResult
                    {
                        Id = item.s.Id,
                        Name = item.s.Name,
                        AlbumSearchResults = new List<AlbumSearchResult> { new AlbumSearchResult {
                     Id= item.a.AlbumId,
                      Name=item.a.AlbumName
                    } },
                        SingerSearchResults = new List<SingerSearchResult> { new SingerSearchResult {
                      Name=item.p.SingerName,
                       Id=item.p.SingerId
                     } }
                    });
                }
            }

            return songSearchResults;
        }
    }
}