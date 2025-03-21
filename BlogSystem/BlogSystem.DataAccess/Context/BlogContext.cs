using BlogSystem.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Context
{
	public class BlogContext : IdentityDbContext<User>
	{
		public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<BlogPostTag> BlogPostTags { get; set; }
		public DbSet<Comment> Comments { get; set; }
	}
}
