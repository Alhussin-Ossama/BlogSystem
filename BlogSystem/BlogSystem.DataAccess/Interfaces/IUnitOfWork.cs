using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Interfaces
{
	public interface IUnitOfWork: IAsyncDisposable
	{
		IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
		Task<int> CompleteAsync();
	}
}
