using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Specifications
{
	public interface ISpecifications<T> where T : class
	{
		Expression<Func<T, bool>> Criteria { get; set; }
		List<Expression<Func<T,object>>> Includes { get; set; }
		List<string> IncludeStrings { get; set; }
		List<Func<IQueryable<T>, IQueryable<T>>> ThenIncludes { get; set; }
	}
}
