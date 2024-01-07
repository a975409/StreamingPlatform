using StreamingPlatform.Domain.Contract;
using StreamingPlatform.xUnitTest.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.Domain.xUnitTest
{
    /// <summary>
    /// 上傳歌曲
    /// </summary>
    public class SongUploadUnitTest
    {

        private readonly StreamingPlatformContextFake _context = new StreamingPlatformContextFakeBuilder()
                                                                .WithAlbum()
                                                                .WithSinger()
                                                                .WithSong()
                                                                .WithRelation()
                                                                .WithMember()
                                                                .Build();

        [Fact]
        public void SongUpload_SongNameIsEmpty_ReturnArgumentNullException()
        {
            var service = new SongService(_context);

            var dto = new SongUploadDto
            {
                albumId = 5,
                AlbumName = "spiral",
                Name = "",
                Note = "",
                SingerId = 1
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.Name), () => service.SongUpload(dto));
        }

        [Fact]
        public void SongUpload_SingerNotFound_ReturnArgumentNullException()
        {
            var service = new SongService(_context);

            var dto = new SongUploadDto
            {
                albumId = 5,
                AlbumName = "spiral",
                Name = "spiral",
                Note = "",
                SingerId = -1
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.SingerId), () => service.SongUpload(dto));
        }
    }
}