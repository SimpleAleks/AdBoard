using AdBoard.Domain.Advert;
using AdBoard.Domain.Category;
using AdBoard.Domain.Image;
using AdBoard.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdBoard.Infrastructure.DataAccess.Contexts.Advert.Configuration;

/// <summary>
/// Конфигурация сущности <see cref="Domain.Advert.Advert"/>
/// </summary>
public class AdvertConfiguration : IEntityTypeConfiguration<Domain.Advert.Advert>
{
    /// <summary>
    /// Метод конфигурации
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Domain.Advert.Advert> builder)
    {
        // Configuring properties
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).HasMaxLength(256).IsRequired();
        builder.Property(a => a.Description).HasMaxLength(1024);
        builder.Property(a => a.Cost);
        builder.Property(a => a.Created).HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
        builder.Property(a => a.Email).HasMaxLength(64);
        builder.Property(a => a.Location).HasMaxLength(256).IsRequired();
        builder.Property(a => a.Phone).HasMaxLength(16);

        // Configuring relations
        // User
        builder.HasOne(advert => advert.User)
            .WithMany(user => user.Adverts)
            .HasForeignKey(advert => advert.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        // Images
        builder.HasMany(advert => advert.Images)
            .WithOne(image => image.Advert)
            .HasForeignKey(image => image.AdvertId)
            .OnDelete(DeleteBehavior.Cascade);

        // Category
        builder.Property(advert => advert.Category).IsRequired();
        builder.Property(advert => advert.CategoryId).IsRequired();
        builder.HasOne(advert => advert.Category)
            .WithMany(category => category.Adverts)
            .HasForeignKey(advert => advert.CategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}