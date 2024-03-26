using Blog.Application.Repositories.Authorize.Endpoint;
using Blog.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Repositories.Authorize.Endpoint
{
    public class EndpointWriteRepository : WriteRepository<Blog.Domain.Entities.Identity.Endpoint>, IEndpointWriteRepository
    {
        public EndpointWriteRepository(BlogApiDbContext context) : base(context)
        {
        }
    }
}
