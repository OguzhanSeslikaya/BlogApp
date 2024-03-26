using Blog.Application.Repositories.File.BannerFile;
using Blog.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Repositories.File.BannerFile
{
    public class BannerFileWriteRepository : WriteRepository<Blog.Domain.Entities.File.LocalStorage.BannerFile>, IBannerFileWriteRepository
    {
        public BannerFileWriteRepository(BlogApiDbContext context) : base(context)
        {
        }
    }
}
