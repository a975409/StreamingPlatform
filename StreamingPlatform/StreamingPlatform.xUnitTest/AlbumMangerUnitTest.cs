using StreamingPlatform.Domain.Contract;
using StreamingPlatform.xUnitTest.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.Domain.xUnitTest
{
    public class AlbumMangerUnitTest
    {
        private readonly StreamingPlatformContextFake _context = new StreamingPlatformContextFakeBuilder()
                                                                .WithAlbum()
                                                                .WithSinger()
                                                                .WithSong()
                                                                .WithRelation()
                                                                .WithMember()
                                                                .Build();

        /// <summary>
        /// 專輯名稱不得為空專輯
        /// </summary>
        [Fact]
        public void CreateAlbum_AlbumNameIsEmpty_ReturnArgumentNullException()
        {
            var service = new AlbumService(_context);

            var dto = new AlbumCreateDto
            {
                Name = "",
                Note = "MID DAY"
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.Name), () => service.CreateAlbum(dto));
        }

        /// <summary>
        /// 找不到專輯，無法編輯
        /// </summary>
        [Fact]
        public void EditAlbum_AlbumNotFound_ReturnArgumentNullException()
        {
            var service = new AlbumService(_context);

            var dto = new AlbumEditDto
            {
                Id = 5,
                Name = "name",
                Note = "note"
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.Id), () => service.EditAlbum(dto));
        }

        [Fact]
        public void EditAlbum_AlbumNameIsEmpty_ReturnArgumentNullException()
        {
            var service = new AlbumService(_context);

            var dto = new AlbumEditDto
            {
                Id = 1,
                Name = "",
                Note = "note"
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.Name), () => service.EditAlbum(dto));
        }
    }
}