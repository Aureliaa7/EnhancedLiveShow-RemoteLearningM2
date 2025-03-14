﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LiveShow.Dal.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task<IEnumerable<T>> AddRange(IEnumerable<T> entities);
        Task<T> Remove(Guid id);
        Task<IEnumerable<T>> RemoveRange(IEnumerable<T> entities);
        Task<T> Update(T entity);
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
    }
}
