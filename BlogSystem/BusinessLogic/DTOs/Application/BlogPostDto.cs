using BlogSystem.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.DTOs.Application
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public List<string>? Tags { get; set; }
	}
}
