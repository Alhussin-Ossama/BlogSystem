using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.BusinessLogic.DTOs.Application
{
	public class ReplayCommentDto
	{
        public int ParentCommentId { get; set; }
        public string Content { get; set; }
    }
}
