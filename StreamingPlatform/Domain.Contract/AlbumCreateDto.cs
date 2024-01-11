using System.Security.Cryptography.X509Certificates;

namespace StreamingPlatform.Domain.Contract
{
    public class AlbumCreateDto
    {
        public string Name { get;set; }
        public string Note { get;set; }
    }
}