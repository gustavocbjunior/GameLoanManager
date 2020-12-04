using System;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameLoanManager.Data.Maps
{
    public class GameMap : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Game");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(60)
                   .HasColumnType("varchar(60)");
            builder.Property(p => p.Type)
                   .HasConversion(
                       t => t.ToString(),
                       t => (EGameType)Enum.Parse(typeof(EGameType), t)
                   )
                   .HasColumnType("varchar(20)");
            builder.Property(p => p.Description)
                   .HasMaxLength(200)
                   .HasColumnType("varchar(200)");

            builder.HasOne(x => x.User)
                   .WithMany(x => x.MyGames)
                   .HasForeignKey(x => x.IdUser)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
