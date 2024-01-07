using Microsoft.EntityFrameworkCore;
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
    public class MemberLoginUnitTest
    {
        private readonly StreamingPlatformContextFake _context = new StreamingPlatformContextFakeBuilder()
                                                                .WithAlbum()
                                                                .WithSinger()
                                                                .WithSong()
                                                                .WithRelation()
                                                                .WithMember()
                                                                .Build();

        /// <summary>
        /// 帳號必填
        /// </summary>
        [Fact]
        public void MemberLogin_AccountNoIsEmpty_ReturnArgumentNullException()
        {
            var service = new MemberService(_context);
            var dto = new MemberLoginDto
            {
                AccountNo = "",
                Pwd = "test",
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.AccountNo), () => service.Login(dto));
        }

        /// <summary>
        /// 密碼必填
        /// </summary>
        [Fact]
        public void MemberLogin_PwdIsEmpty_ReturnArgumentNullException()
        {
            var service = new MemberService(_context);
            var dto = new MemberLoginDto
            {
                AccountNo = "test",
                Pwd = "",
            };

            Assert.Throws<ArgumentNullException>(nameof(dto.Pwd), () => service.Login(dto));
        }

        /// <summary>
        /// 登入驗證
        /// </summary>
        [Fact]
        public void MemberLogin_Verification_ReturnArgumentOutOfRangeException()
        {
            var service = new MemberService(_context);
            var dto = new MemberLoginDto
            {
                AccountNo = "test",
                Pwd = "test",
            };

            Assert.Throws<ArgumentOutOfRangeException>("登入失敗", () => service.Login(dto));
        }
    }
}
