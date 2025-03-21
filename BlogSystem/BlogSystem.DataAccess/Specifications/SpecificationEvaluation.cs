using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Specifications
{
	public static class SpecificationEvaluation<T>where T : class
	{
		public static IQueryable<T> GetQuery(IQueryable<T> query,ISpecifications<T> spec)
		{
			IQueryable<T> Query = query;
			if (spec.Criteria is not null)
			{
				Query = Query.Where(spec.Criteria);
			}

			Query = spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

			if(spec.IncludeStrings.Any())
			{
				Query = spec.IncludeStrings.Aggregate(Query, (current, include) => current.Include(include));
			}

			if (spec.ThenIncludes.Any())
			{
				Query = spec.ThenIncludes.Aggregate(Query, (current, include) => include(current));
			}

			return Query;
		}
	}
}
