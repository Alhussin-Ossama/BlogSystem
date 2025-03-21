using BlogSystem.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Specifications.AppSpecification
{
	public class RepliesByParentIdSpecification : BaseSpecifications<Comment>
	{
		public RepliesByParentIdSpecification(int parentCommentId): base(c => c.ParentCommentId == parentCommentId)
		{
		}
	}
}
