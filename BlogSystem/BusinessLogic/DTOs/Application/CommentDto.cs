using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.DTOs.Application
{
	public class CommentDto
	{
		public int Id { get; set; }
		public string AuthorName { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Content { get; set; }

		public List<CommentDto>? Replies { get; set; } = new List<CommentDto>();
	}
}
