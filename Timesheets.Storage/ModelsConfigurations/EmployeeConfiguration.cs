using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheets.Models;

namespace Timesheets.Storage.ModelsConfigurations
{
    class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        void IEntityTypeConfiguration<Employee>.Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Department);

            builder.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserId)
                .HasPrincipalKey<User>(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
