using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Entities
{
	public class Tag
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<BlogPostTag> BlogPostTags { get; set; }
	}
}
