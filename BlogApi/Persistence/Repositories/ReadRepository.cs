using Blog.Application.Repositories;
using Blog.Domain.Entities.Common;
using Blog.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Repositories
{
    public class ReadRepository<T> :IReadRepository<T> where T : BaseEntity
    {
        private readonly BlogApiDbContext _context;

        public ReadRepository(BlogApiDbContext context)
        {
            _context = context;
        }

        public DbSet<T> table => _context.Set<T>();

        public IQueryable<T> getAll(bool tracking = true)
        {
            var query = table.AsQueryable();
            if (!tracking)
                query.AsNoTracking();
            return query;
        }


        public async Task<T> getByIdAsync(string id, bool tracking = true)
        {
            var query = table.AsQueryable();
            if (!tracking)
                query.AsNoTracking();
            return await query.FirstOrDefaultAsync(d => d.id == Guid.Parse(id));
        }


        public async Task<T> getSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = table.AsQueryable();
            if (!tracking)
                query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> getWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = table.AsNoTracking();
            if (!tracking)
                query.AsNoTracking();
            return query.Where(method);
        }
    }
}
