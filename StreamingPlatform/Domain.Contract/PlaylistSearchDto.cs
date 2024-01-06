using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.Domain.Contract
{
    /// <summary>
    /// 播放清單搜尋結果
    /// </summary>
    public class PlaylistSearchDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 會員Id
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 播放清單名稱
        /// </summary>
        public string Name { get; set; }
    }
}