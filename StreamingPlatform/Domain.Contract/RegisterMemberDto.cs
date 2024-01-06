using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamingPlatform.Domain.Contract
{
    public class RegisterMemberDto
    {
        public string AccountNo { get; set; }

        public string Pwd { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool GoogleOta { get; set; }
    }
}
