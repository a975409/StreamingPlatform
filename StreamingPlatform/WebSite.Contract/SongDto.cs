using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.WebSite.Contract
{
    public static class SongDto
    {
        /// <summary>
        /// 歌曲查詢條件
        /// </summary>
        public class SearchDto
        {
            /// <summary>
            /// 歌曲名稱
            /// </summary>
            public string SongName { get; set; }

            /// <summary>
            /// 演唱者
            /// </summary>
            public string SingerName { get; set; }

            /// <summary>
            /// 專輯名稱
            /// </summary>
            public string AlbumName { get; set; }
        }
    }
}
