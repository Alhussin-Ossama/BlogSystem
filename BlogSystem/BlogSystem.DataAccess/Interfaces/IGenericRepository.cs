using BlogSystem.DataAccess.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		#region With Sepcifications
		Task<IEnumerable<T>> GetAllWithSpecificationAsync(ISpecifications<T> specifications);
		Task<T> SearchWithSpecificationAsync(ISpecifications<T> specifications);
		#endregion

		#region Without Sepcifications
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		#endregion
		Task AddAsync(T item);
		void Update(T item);
		void Delete(T item);
	}
}
