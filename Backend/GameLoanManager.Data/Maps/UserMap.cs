using System;
using GameLoanManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameLoanManager.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Email).HasMaxLength(120).HasColumnType("varchar(120)");
            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(80)
                   .HasColumnType("varchar(80)");

            builder.Property(u => u.Login)
                   .IsRequired()
                   .HasMaxLength(40)
                   .HasColumnType("varchar(40)");
            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(20)
                   .HasColumnType("varchar(20)");
            builder.Property(p => p.Phone)
                   .IsRequired()
                   .HasMaxLength(11)
                   .HasColumnType("varchar(11)");
        }
    }
}
