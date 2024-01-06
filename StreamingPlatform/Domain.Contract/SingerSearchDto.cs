namespace StreamingPlatform.Domain.Contract
{
    /// <summary>
    /// 歌手搜尋結果
    /// </summary>
    public class SingerSearchDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 歌曲名稱
        /// </summary>
        public string Name { get; set; }
    }
}