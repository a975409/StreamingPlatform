﻿namespace StreamingPlatform.Domain.Contract
{
    public class SongUploadDto
    {
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