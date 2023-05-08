using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdBoard.Infrastructure.DataAccess.Contexts.User.Configuration;

/// <summary>
/// Конфигурация EF Core сущности <see cref="Domain.User.User"/>
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<Domain.User.User>
{
    public void Configure(EntityTypeBuilder<Domain.User.User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).HasMaxLength(64).IsRequired();
        builder.Property(u => u.Role).HasMaxLength(20).IsRequired();
        builder.Property(u => u.Login).HasMaxLength(64).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(50).IsRequired();
        builder.Property(u => u.RegisteredTime)
            .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc))
            .IsRequired();
        
        builder.HasIndex(u => u.Login).IsUnique();

        builder.HasMany(u => u.Adverts)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}