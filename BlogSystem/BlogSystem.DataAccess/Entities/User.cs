﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Entities
{
	public class User:IdentityUser
	{
		public string FirstName {  get; set; }
		public string LastName { get; set; }
		public string Role { get; set; }
		public IEnumerable<BlogPost> BlogPosts { get; set; }
		public IEnumerable<Comment> Comments { get; set; }
	}
}
