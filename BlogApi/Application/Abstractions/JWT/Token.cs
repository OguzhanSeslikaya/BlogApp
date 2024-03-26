using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Abstractions.JWT
{
    public class Token
    {
        public string accessToken { get; set; }
        public DateTime expiration { get; set; }
        public string refreshToken { get; set; }
    }
}
