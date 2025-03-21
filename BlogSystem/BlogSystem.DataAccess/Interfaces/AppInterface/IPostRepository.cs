using BlogSystem.DataAccess.Entities;
using BlogSystem.DataAccess.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Interfaces.AppInterface
{
	public interface IPostRepository: IGenericRepository<BlogPost>
	{
		//Task<BlogPost> SearchWithSpecificationAsync(ISpecifications<BlogPost> specifications);
	}
}
