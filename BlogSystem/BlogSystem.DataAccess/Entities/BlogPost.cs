using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Entities
{
	public class BlogPost
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }
		public string Status { get; set; } 


		public string AuthorId { get; set; }
		public User Author { get; set; }

		public int CategoryId { get; set; }
		public Category Category { get; set; }

		public IEnumerable<BlogPostTag> BlogPostTags { get; set; }

		public IEnumerable<Comment>? Comments { get; set; }
	}
}
