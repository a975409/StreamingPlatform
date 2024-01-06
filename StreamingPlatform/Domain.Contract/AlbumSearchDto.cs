namespace StreamingPlatform.Domain.Contract
{
    /// <summary>
    /// 專輯查詢結果
    /// </summary>
    public class AlbumSearchDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 專輯名稱
        /// </summary>
        public string Name { get; set; }
    }
}