using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdBoard.Infrastructure.DataAccess.Contexts.Category.Configuration;

/// <summary>
/// Конфигурация EF Core сущности <see cref="Domain.Category.Category"/>
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Domain.Category.Category>
{
    public void Configure(EntityTypeBuilder<Domain.Category.Category> builder)
    {
        // Configure properties
        builder.HasKey(category => category.Id);
        builder.Property(category => category.Name).HasMaxLength(64).IsRequired();
        
        // Configure relationships
        builder.HasOne(category => category.Parent)
            .WithMany(x => x.Childs)
            .HasForeignKey(category => category.ParentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        // builder.Property(category => category.ParentId).IsRequired(false);

        builder.HasMany(category => category.Adverts)
            .WithOne(advert => advert.Category)
            .HasForeignKey(advert => advert.CategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}