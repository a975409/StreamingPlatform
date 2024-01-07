namespace StreamingPlatform.Domain.Contract
{
    public class SongUploadDto
    {
        /// <summary>
        /// 歌曲名稱
        /// </summary>
        public string Name { get; set; }
        public string Note { get; set; }

        public int SingerId { get; set; }

        /// <summary>
        /// 專輯id，為null時如果AlbumName不為空，則會建立新的專輯
        /// </summary>
        public int? albumId { get; set; }

        /// <summary>
        /// 專輯名稱
        /// </summary>
        public string AlbumName { get; set; }
    }
}