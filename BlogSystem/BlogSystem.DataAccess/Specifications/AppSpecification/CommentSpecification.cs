using BlogSystem.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Specifications.AppSpecification
{
	public class CommentSpecification : BaseSpecifications<Comment>
	{
		public CommentSpecification(int id) : base(C => C.PostId == id && C.ParentCommentId == null)
		{
			Includes.Add(c => c.Author);
			Includes.Add(c => c.Replies);
			ThenIncludes.Add(q => q.Include(c => c.Replies)
							 .ThenInclude(r => r.Author));
							 


		}
	}
}