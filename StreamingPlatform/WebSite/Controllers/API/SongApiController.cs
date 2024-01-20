using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamingPlatform.Domain;
using StreamingPlatform.WebSite.Contract;

namespace StreamingPlatform.WebSite.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongApiController : ControllerBase
    {
        public readonly SongService _songService;

        public SongApiController() { }

        [HttpGet]
        public async Task<IActionResult> GetList(SongDto.SearchDto dto)
        {
            var result = _songService.GetSongList(dto.AlbumName, dto.SongName);
            
            return Ok(result);
        }
    }
}
