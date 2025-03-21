using BlogSystem.DataAccess.Context;
using BlogSystem.DataAccess.Interfaces;
using BlogSystem.DataAccess.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly BlogContext _context;

		public GenericRepository(BlogContext context)
		{
			_context = context;
		}

		public async Task AddAsync(T item)
			=> await _context.Set<T>().AddAsync(item);

		public void Delete(T item)
			=> _context.Remove(item);

		public async Task<IEnumerable<T>> GetAllAsync()
			=> await _context.Set<T>().ToListAsync();

		public async Task<T> GetByIdAsync(int id)
			=> await _context.Set<T>().FindAsync(id);

		public void Update(T item)
			=> _context.Update(item);

		public async Task<IEnumerable<T>> GetAllWithSpecificationAsync(ISpecifications<T> specifications)
			=> await ApplySepcification(specifications).ToListAsync();
		

		public async Task<T> SearchWithSpecificationAsync(ISpecifications<T> specifications)
		
			=> await ApplySepcification(specifications).FirstOrDefaultAsync();
		
		private IQueryable<T> ApplySepcification(ISpecifications<T> specifications)
		{
			return SpecificationEvaluation<T>.GetQuery(_context.Set<T>(), specifications);
		}
	}
}
