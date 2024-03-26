using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities.Identity
{
    public class AppRole : IdentityRole
    {
        public ICollection<Blog.Domain.Entities.Identity.Endpoint> endpoints { get; set; }
    }
}
