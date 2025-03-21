using BlogSystem.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.DTOs.Application
{
	public class UpdatePostDto
	{
        public PostStatus Status { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public int CategoryId { get; set; }

    }
}
