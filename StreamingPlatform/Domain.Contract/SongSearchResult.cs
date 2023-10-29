namespace StreamingPlatform.Domain.Contract
{
    public class SongSearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SingerSearchResult> SingerSearchResults { get; set; }
        public List<AlbumSearchResult> AlbumSearchResults { get; set; }
    }
}