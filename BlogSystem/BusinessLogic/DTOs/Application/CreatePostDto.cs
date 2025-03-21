using BlogSystem.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.DTOs.Application
{
	public class CreatePostDto
	{
		public string Title { get; set; }
		public string Content { get; set; }
		public int CategoryId { get; set; }
		public List<int> TagIds { get; set; } = new List<int>();
	}
}
