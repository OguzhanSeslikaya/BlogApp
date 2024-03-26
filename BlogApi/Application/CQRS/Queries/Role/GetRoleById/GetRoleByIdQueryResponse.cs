using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Queries.Role.GetRoleById
{
    public class GetRoleByIdQueryResponse
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
