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
        builder.HasOne<Domain.Category.Category>()
            .WithMany(x => x.Childs)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<Domain.Advert.Advert>()
            .WithOne(advert => advert.Category)
            .HasForeignKey(advert => advert.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}