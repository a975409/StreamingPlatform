using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.Domain.Contract
{
    public class SongEditDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 歌曲名稱
        /// </summary>
        public string Name { get; set; }
        public string Note { get; set; }

        /// <summary>
        /// 多位歌手id
        /// </summary>
        public IEnumerable<int> SingerIds { get; set; }

        /// <summary>
        /// 多張專輯
        /// </summary>
        public IEnumerable<int> AlbumIds { get; set; }
    }
}
