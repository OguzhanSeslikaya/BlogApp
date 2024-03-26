using Blog.Application.Repositories.File.PageFile;
using Blog.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Repositories.File.PageFile
{
    public class PageFileWriteRepository : WriteRepository<Domain.Entities.File.LocalStorage.PageFile>,IPageFileWriteRepository
    {
        public PageFileWriteRepository(BlogApiDbContext context) : base(context)
        {
        }
    }
}
