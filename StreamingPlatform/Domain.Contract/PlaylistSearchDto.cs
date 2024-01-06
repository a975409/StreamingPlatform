using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.Domain.Contract
{
    public class PlaylistSearchDto
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        public string Name { get; set; }
    }
}