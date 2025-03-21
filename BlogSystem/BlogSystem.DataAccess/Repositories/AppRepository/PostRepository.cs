using BlogSystem.DataAccess.Context;
using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Interfaces.AppInterface;
using BlogSystem.DataAccess.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Repositories.AppRepository
{
	public class PostRepository:GenericRepository<BlogPost>,IPostRepository
	{
		private readonly BlogContext _context;

		public PostRepository(BlogContext context):base(context)
		{
			_context = context;
		}

		//public async Task<BlogPost> SearchWithSpecificationAsync(ISpecifications<BlogPost> specifications)
		
		//=> await SpecificationEvaluation<BlogPost>.GetQuery(_context.Set<BlogPost>(), specifications).FirstOrDefaultAsync();


	}
}
