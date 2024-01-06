namespace StreamingPlatform.Domain.Contract
{
    /// <summary>
    /// 歌曲搜尋結果
    /// </summary>
    public class SongSearchDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 歌曲名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 演唱者(多位)
        /// </summary>
        public List<SingerSearchDto> SingerSearchResults { get; set; }

        /// <summary>
        /// 專輯(多張)
        /// </summary>
        public List<AlbumSearchDto> AlbumSearchResults { get; set; }
    }
}