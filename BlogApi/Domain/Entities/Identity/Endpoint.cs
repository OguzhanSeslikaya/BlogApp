using Blog.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities.Identity
{
    public class Endpoint : BaseEntity
    {
        public Endpoint()
        {
            roles = new HashSet<AppRole>();
        }

        public string code { get; set; }
        public ICollection<AppRole> roles { get; set; }
    }
}
