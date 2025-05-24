﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Domain.Common;

namespace TopSpeed.Application.Contracts.Presistence
{
    public interface IGenericRepository<T>  where T : BaseModel
    {
        Task Create(T entity);
        Task Delete(T entity);
        Task<List<T>> Get(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllAsync();
        Task<IEnumerable<T>> Query(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> Query();

        Task<T> GetByIdAsync(Guid id);
        Task<bool> IsRecordExists(Expression<Func<T, bool>> predicate);


    }
}
