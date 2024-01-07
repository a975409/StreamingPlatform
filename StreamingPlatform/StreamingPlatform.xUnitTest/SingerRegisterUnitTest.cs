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
    /// 會員登入
    /// </summary>
    public class SingerRegisterUnitTest
    {
        private readonly StreamingPlatformContextFake _context = new StreamingPlatformContextFakeBuilder()
                                                                .WithAlbum()
                                                                .WithSinger()
                                                                .WithSong()
                                                                .WithRelation()
                                                                .WithMember()
                                                                .Build();

        /// <summary>
        /// 歌手帳號需符合以下條件：
        /// 至少有一個數字
        /// 至少有一個小寫英文字母
        /// 至少有一個大寫英文字母
        /// 字串長度在 6 ~ 30 個字母之間
        /// </summary>
        [Fact]
        public void RegisterMember_AccountNoIsVerificationPassed_ReturnArgumentException()
        {
            var service = new SingerService(_context);
            var dto = new RegisterSingerDto
            {
                AccountNo = "a975409",
                DisplayName = "test",
                Email = "test@gmail.com",
                GoogleOta = false,
                Name = "test",
                Pwd = "A5516amazarashiamazarashiamazarashi",
            };

            Assert.Throws<ArgumentException>(nameof(dto.AccountNo), () => service.Register(dto));
        }

        /// <summary>
        /// 歌手帳號不能重複
        /// </summary>
        [Fact]
        public void RegisterMember_AccountNoIsDuplicate_ReturnArgumentException()
        {
            var service = new SingerService(_context);
            var dto = new RegisterSingerDto
            {
                AccountNo = "A5516amazarashia",
                DisplayName = "test",
                Email = "test@gmail.com",
                GoogleOta = false,
                Name = "test",
                Pwd = "A5516amazarashiamazarashiamazarashi",
            };

            Assert.Throws<ArgumentException>(nameof(dto.AccountNo), () => service.Register(dto));
        }

        /// <summary>
        /// Email必填
        /// </summary>
        [Fact]
        public void RegisterMember_EmailIsEmpty_ReturnArgumentNullException()
        {
            var service = new SingerService(_context);
            var dto = new RegisterSingerDto
            {
                AccountNo = "A5516amazara888",
                DisplayName = "test",
                Email = "",
                GoogleOta = false,
                Name = "test",
                Pwd = "A5516amazarashiamazarashiamazarashi",
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.Email), () => service.Register(dto));
        }

        /// <summary>
        /// Email不能重複
        /// </summary>
        [Fact]
        public void RegisterMember_EmailIsDuplicate_ReturnArgumentException()
        {
            var service = new SingerService(_context);
            var dto = new RegisterSingerDto
            {
                AccountNo = "A5516amazara888",
                DisplayName = "test",
                Email = "amazarashi@gmail.com",
                GoogleOta = false,
                Name = "test",
                Pwd = "A5516amazarashiamazarashiamazarashi",
            };

            Assert.Throws<ArgumentException>(nameof(dto.Email), () => service.Register(dto));
        }

        /// <summary>
        /// 密碼需符合以下條件：
        /// 至少有一個數字
        /// 至少有一個小寫英文字母
        /// 至少有一個大寫英文字母
        /// 字串長度在 6 ~ 30 個字母之間
        /// </summary>
        [Fact]
        public void RegisterMember_PwdIsVerificationPassed_ReturnArgumentException()
        {
            var service = new SingerService(_context);
            var dto = new RegisterSingerDto
            {
                AccountNo = "A5516amazara888",
                DisplayName = "test",
                Email = "a975409888@gmail.com",
                GoogleOta = false,
                Name = "test",
                Pwd = "test",
            };

            Assert.Throws<ArgumentException>(nameof(dto.Pwd), () => service.Register(dto));
        }

        /// <summary>
        /// 歌手姓名必填
        /// </summary>
        [Fact]
        public void RegisterMember_NameIsEmpty_ReturnArgumentNullException()
        {
            var service = new SingerService(_context);
            var dto = new RegisterSingerDto
            {
                AccountNo = "A5516amazara888",
                DisplayName = "test",
                Email = "a975409888@gmail.com",
                GoogleOta = false,
                Name = "",
                Pwd = "A5516amazara888",
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.Name), () => service.Register(dto));
        }
    }
}
