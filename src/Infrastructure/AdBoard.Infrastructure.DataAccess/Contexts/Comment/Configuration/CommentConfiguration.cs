using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdBoard.Infrastructure.DataAccess.Contexts.Comment.Configuration;
using Comment = AdBoard.Domain.Comment.Comment;
 
public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        // Configuration properties
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Text).HasMaxLength(1024).IsRequired();
        builder.Property(c => c.Created)
            .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc))
            .IsRequired();
        
        // Configuring relations
        // Advert
        builder.HasOne(c => c.Advert)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.AdvertId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        //User
        builder.HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        // Self
        builder.HasOne(c => c.Parent)
            .WithMany(c => c.Childs)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}