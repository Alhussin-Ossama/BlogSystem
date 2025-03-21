using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.DTOs.Application
{
	public class AddCommentDto
	{
        public int PostId { get; set; }
		public string Comment { get; set; }
		
	}
}
