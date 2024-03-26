using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Repositories.Authorize.Endpoint
{
    public interface IEndpointWriteRepository : IWriteRepository<Blog.Domain.Entities.Identity.Endpoint>
    {
    }
}
