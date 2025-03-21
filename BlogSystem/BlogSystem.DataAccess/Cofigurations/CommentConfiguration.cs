using BlogSystem.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DataAccess.Cofigurations
{
	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.Property(c => c.Content)
				.IsRequired()
				.HasMaxLength(500);


			builder.HasOne(c => c.BlogPost)
				.WithMany(p => p.Comments)
				.HasForeignKey(c => c.PostId)
				.OnDelete(DeleteBehavior.Cascade);


			builder.HasOne(c => c.Author)
				.WithMany(u => u.Comments)
				.HasForeignKey(c => c.AuthorId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(c => c.ParentComment)
			.WithMany(c => c.Replies)
			.HasForeignKey(c => c.ParentCommentId)
			.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
