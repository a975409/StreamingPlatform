using Microsoft.EntityFrameworkCore;
using StreamingPlatform.Domain.Contract;
using StreamingPlatform.Domain.Models;

namespace StreamingPlatform.Domain
{
    public class SingerService
    {
        private readonly StreamingPlatformContext _context;

        public SingerService(StreamingPlatformContext context)
        {
            _context = context;
        }

        public IEnumerable<SingerSearchResult> GetSingerList(string singerName)
        {
            var result = _context.Singer.AsNoTracking().Select(m => m);

            if (!string.IsNullOrEmpty(singerName))
                result = result.Where(m => m.Name.Contains(singerName));

            return result.Select(m => new SingerSearchResult
            {
                Id = m.Id,
                Name = m.Name
            });
        }
    }
}