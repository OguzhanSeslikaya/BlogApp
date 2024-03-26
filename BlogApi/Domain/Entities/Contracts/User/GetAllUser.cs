using Blog.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities.Contracts.User
{
    public class GetAllUser
    {
        public string id { get; set; }
        public string name { get; set; }
        public string? role { get; set; }
    }
}
