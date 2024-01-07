using Microsoft.EntityFrameworkCore;
using StreamingPlatform.Domain.Contract;
using StreamingPlatform.Domain.Models;
using System.Text.RegularExpressions;
using StreamingPlatform.Framework;

namespace StreamingPlatform.Domain
{
    public class SingerService
    {
        private readonly StreamingPlatformContext _context;

        public SingerService(StreamingPlatformContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 取得歌手列表
        /// </summary>
        /// <param name="singerName"></param>
        /// <returns></returns>
        public IEnumerable<SingerSearchDto> GetSingerList(string singerName)
        {
            var result = _context.Singer.AsNoTracking().Select(m => m);

            if (!string.IsNullOrEmpty(singerName))
                result = result.Where(m => m.Name.Contains(singerName));

            return result.Select(m => new SingerSearchDto
            {
                Id = m.Id,
                Name = m.Name
            });
        }

        public void Login(SingerLoginDto dto)
        {
            if (string.IsNullOrEmpty(dto.AccountNo))
                throw new ArgumentNullException(nameof(dto.AccountNo), "帳號不得為空");

            if (string.IsNullOrEmpty(dto.Pwd))
                throw new ArgumentNullException(nameof(dto.Pwd), "密碼不得為空");

            var member = _context.Singer.AsNoTracking().FirstOrDefault(m => m.AccountNo == dto.AccountNo && m.Pwd == dto.Pwd);

            if (member == null)
                throw new ArgumentOutOfRangeException("登入失敗");

            //進行GoogleOta二階驗證
            if (member.GoogleOta)
            {

            }

            //新增登入紀錄
        }

        public void LogOut(string accountNo)
        {
            //新增登出紀錄
        }

        public void Register(RegisterSingerDto dto)
        {
            var accountNoRegex = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,30}$");

            if (accountNoRegex.IsMatch(dto.AccountNo) == false)
                throw new ArgumentException("輸入的帳號不符合規範", nameof(dto.AccountNo));

            bool accountNoIsexist = _context.Singer.AsNoTracking().Any(m => m.AccountNo == dto.AccountNo);

            if (accountNoIsexist)
                throw new ArgumentException("該帳號已存在", nameof(dto.AccountNo));

            if (string.IsNullOrEmpty(dto.Email))
                throw new ArgumentNullException(nameof(dto.Email), "Email不得為空");

            bool emailexist = _context.Singer.AsNoTracking().Any(m => m.Email == dto.Email);

            if (emailexist)
                throw new ArgumentException("該Email已存在", nameof(dto.Email));

            var pwdRegex = new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,30}$");

            if (pwdRegex.IsMatch(dto.Pwd) == false)
                throw new ArgumentException("輸入的密碼不符合規範", nameof(dto.Pwd));

            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentNullException(nameof(dto.Name), "會員姓名不得為空");

            //新增帳號
            _context.Singer.Add(new Singer
            {
                AccountNo = dto.AccountNo,
                DisplayName = string.IsNullOrEmpty(dto.DisplayName) ? dto.AccountNo : dto.DisplayName,
                Email = dto.Email,
                GoogleOta = dto.GoogleOta,
                Name = dto.Name,
                Pwd = dto.Pwd,
                CtimeUnixTime = DateTime.Now.GetUnixTimeSecByDateTime(),
                MtimeUnixTime = DateTime.Now.GetUnixTimeSecByDateTime(),
                EmailIsVerificationPassed = false
            });

            _context.SaveChanges();

            //Email驗證
        }
    }
}