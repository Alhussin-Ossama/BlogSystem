using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : class
	{
		public Expression<Func<T, bool>> Criteria { get; set; }
		public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>> ();
		public List<string> IncludeStrings { get; set; } = new List<string>();
		public List<Func<IQueryable<T>, IQueryable<T>>> ThenIncludes { get; set; } = new();

		public BaseSpecifications()
		{

		}
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
			Criteria = criteriaExpression;
		}

	}
}
