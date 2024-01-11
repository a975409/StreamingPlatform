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
        /// <summary>
        /// 歌曲名稱必填
        /// </summary>
        [Fact]
        public void SongUpload_SongNameIsEmpty_ReturnArgumentNullException()
        {
            var service = new SongService(_context);

            var dto = new SongUploadDto
            {
                Name = "",
                Note = "",
                AlbumIds = new List<int> { 1 },
                SingerIds = new List<int> { 1, }
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.Name), () => service.SongUpload(dto));
        }

        /// <summary>
        /// 編輯歌曲 - 找不到歌曲
        /// </summary>
        [Fact]
        public void SongEdit_SongNotFound_ReturnArgumentNullException()
        {
            var service = new SongService(_context);

            var dto = new SongEditDto
            {
                Id = 10,
                Name = "estsgd",
                Note = "",
                AlbumIds = new List<int> { 1 },
                SingerIds = new List<int> { 1, }
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.Id), () => service.SongEdit(dto));
        }

        /// <summary>
        /// 編輯歌曲 - 歌曲名稱為空
        /// </summary>
        [Fact]
        public void SongEdit_SongNameIsEmpty_ReturnArgumentNullException()
        {
            var service = new SongService(_context);

            var dto = new SongEditDto
            {
                Id = 1,
                Name = "",
                Note = "",
                AlbumIds = new List<int> { 1 },
                SingerIds = new List<int> { 1, }
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.Name), () => service.SongEdit(dto));
        }
    }
}