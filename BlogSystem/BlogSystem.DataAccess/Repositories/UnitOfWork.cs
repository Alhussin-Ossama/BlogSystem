using BlogSystem.DataAccess.Context;
using BlogSystem.DataAccess.Interfaces;
using System.Collections;

namespace BlogSystem.DataAccess.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly BlogContext _context;
		private Hashtable _repositories;

		public UnitOfWork(BlogContext context)
		{
			_context = context;
			_repositories = new Hashtable();
		}
		public async Task<int> CompleteAsync()
			=> await _context.SaveChangesAsync();

		public async ValueTask DisposeAsync()

			=> await _context.DisposeAsync();


		public IGenericRepository<T> Repository<T>() where T : class
		{
			var type = typeof(T).Name;
			if (!_repositories.ContainsKey(type))
			{
				var Repository = new GenericRepository<T>(_context);
				_repositories.Add(type, Repository);
			}
			return _repositories[type] as IGenericRepository<T>;
		}
	}
}
