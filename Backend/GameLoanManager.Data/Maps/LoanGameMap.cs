using System;
using GameLoanManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameLoanManager.Data.Maps
{
    public class LoanGameMap : IEntityTypeConfiguration<LoanedGame>
    {

        public void Configure(EntityTypeBuilder<LoanedGame> builder)
        {
            builder.ToTable("LoanGame");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.IdGame)
                .IsRequired();

            builder.Property(u => u.IdUser)
            .IsRequired();

            builder.Property(u => u.Returned);

            builder.HasOne(x => x.Game)
                   .WithMany(x => x.LoanedGames)
                   .HasForeignKey(x => x.IdGame)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.User)
                .WithMany(x => x.LoanedGames)
                .HasForeignKey(x => x.IdUser)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
