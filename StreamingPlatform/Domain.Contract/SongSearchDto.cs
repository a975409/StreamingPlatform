namespace StreamingPlatform.Domain.Contract
{
    public class SongSearchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SingerSearchDto> SingerSearchResults { get; set; }
        public List<AlbumSearchDto> AlbumSearchResults { get; set; }
    }
}