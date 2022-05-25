using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheets.Models;

namespace Timesheets.Storage.ModelsConfigurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasAlternateKey(u => u.Username);
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasIndex(u => u.FirstName);
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasIndex(u => u.LastName);
            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasIndex(u => u.Email);
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(75);

            builder.Property(u => u.PasswordHash)
                .IsRequired();
        }
    }
}
