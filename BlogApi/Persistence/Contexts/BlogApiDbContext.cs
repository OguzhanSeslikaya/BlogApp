using Blog.Domain.Entities.Common;
using Blog.Domain.Entities.File;
using Blog.Domain.Entities.File.LocalStorage;
using Blog.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Contexts
{
    public class BlogApiDbContext : IdentityDbContext<AppUser, AppRole, string>
    {

        public DbSet<PageFile> pageFiles { get; set; }
        public DbSet<BannerFile> bannerFiles { get; set; }
        public DbSet<Endpoint> endpoints { get; set; }

        public BlogApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.createdDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.updatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
