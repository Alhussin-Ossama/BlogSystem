using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Entities
{
	public class Comment
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


		public int? ParentCommentId { get; set; }
		public Comment? ParentComment { get; set; }
		public List<Comment>? Replies { get; set; } = new List<Comment>();

		public int PostId { get; set; }
		public BlogPost BlogPost { get; set; }

		public string AuthorId { get; set; }
		public User Author { get; set; }
	}
}
