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
        builder.Property(a => a.Cost).IsRequired();
        builder.Property(a => a.Created)
            .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc))
            .IsRequired();
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
        builder.Property(a => a.UserId).IsRequired();
        
        // Images
        builder.HasMany(advert => advert.Images)
            .WithOne(image => image.Advert)
            .HasForeignKey(image => image.AdvertId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        // Category
        builder.HasOne(advert => advert.Category)
            .WithMany(category => category.Adverts)
            .HasForeignKey(advert => advert.CategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.Property(a => a.CategoryId).IsRequired();
    }
}