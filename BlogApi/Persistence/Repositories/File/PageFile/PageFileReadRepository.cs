using Blog.Application.Repositories;
using Blog.Application.Repositories.File.PageFile;
using Blog.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Repositories.File.PageFile
{
    public class PageFileReadRepository : ReadRepository<Domain.Entities.File.LocalStorage.PageFile>, IPageFileReadRepository
    {
        public PageFileReadRepository(BlogApiDbContext context) : base(context)
        {
        }
    }
}
