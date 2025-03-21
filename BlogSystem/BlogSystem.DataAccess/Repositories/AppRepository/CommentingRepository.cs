using BlogSystem.DataAccess.Context;
using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Interfaces.AppInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Repositories.AppRepository
{
	public class CommentingRepository : GenericRepository<Comment>, ICommintingRepository
	{
		private readonly BlogContext _context;

		public CommentingRepository(BlogContext context) : base(context)
		{
			_context = context;
		}
	}

}
