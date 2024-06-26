﻿using Blog.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> addAsync(T model);
        Task<bool> addRangeAsync(List<T> model);
        bool remove(T model);
        Task<bool> removeAsync(string id);
        bool update(T model);
        Task<int> saveAsync();
    }
}
