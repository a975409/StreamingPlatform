using StreamingPlatform.Domain.Contract;

namespace StreamingPlatform.Domain
{
    public class MemberService
    {
        public MemberService()
        {
        }

        /// <summary>
        /// 註冊帳號
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Register(RegisterMemberDto dto)
        {
            if (string.IsNullOrEmpty(dto.AccountNo))
                throw new ArgumentNullException(nameof(dto.AccountNo), "帳號不得為空");
        }
    }
}