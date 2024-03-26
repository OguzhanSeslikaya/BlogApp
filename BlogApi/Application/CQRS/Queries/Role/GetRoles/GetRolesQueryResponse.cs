using Blog.Domain.Entities.Contracts.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Queries.Role.GetRoles
{
    public class GetRolesQueryResponse
    {
        public GetAllRole[] roles { get; set; }
    }
}
