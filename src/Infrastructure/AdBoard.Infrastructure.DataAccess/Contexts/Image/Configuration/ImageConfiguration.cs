using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdBoard.Infrastructure.DataAccess.Contexts.Image.Configuration;

/// <summary>
/// Конфигурация EF Core сущности <see cref="Domain.Image.Image"/>
/// </summary>
public class ImageConfiguration : IEntityTypeConfiguration<Domain.Image.Image>
{
    public void Configure(EntityTypeBuilder<Domain.Image.Image> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Content).IsRequired();

        builder.HasOne(i => i.Advert)
            .WithMany(a => a.Images)
            .HasForeignKey(i => i.AdvertId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}