using StreamingPlatform.Domain.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.Domain.xUnitTest
{
    public class MemberRegisterUnitTest
    {
        [Fact]
        public void RegisterMember_AccountNoIsEmpty_ReturnArgumentNullException()
        {
            var service = new MemberService();
            var dto = new RegisterMemberDto
            {
                AccountNo = "",
                DisplayName = "test",
                Email = "test@gmail.com",
                GoogleOta = false,
                Name = "test",
                Pwd = "test",
            };

            Assert.Throws<ArgumentNullException>(() => service.Register(dto));
        }

        [Fact]
        public void RegisterMember_AccountNoLength_ReturnArgumentNullException()
        {
            var service = new MemberService();
            var dto = new RegisterMemberDto
            {
                AccountNo = "",
                DisplayName = "test",
                Email = "test@gmail.com",
                GoogleOta = false,
                Name = "test",
                Pwd = "test",
            };

            Assert.Throws<ArgumentNullException>(() => service.Register(dto));
        }
    }
}