﻿using System.Linq.Expressions;

namespace Kuba.Domain.Interfaces
{
    public interface IBaseSpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>>  Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
    }
}
