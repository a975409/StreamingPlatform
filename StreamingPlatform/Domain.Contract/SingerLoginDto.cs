using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.Domain.Contract
{
    public class SingerLoginDto
    {
        /// <summary>
        /// 帳號
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Pwd { get; set; }
    }
}
