using Kuba.Domain.Interfaces;
using Kuba.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kuba.Infra.Data
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            IBaseSpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            query = specification.Includes.Aggregate
                (query, (current, include)
                    => current.Include(include));

            return query;
        }
    }
}
