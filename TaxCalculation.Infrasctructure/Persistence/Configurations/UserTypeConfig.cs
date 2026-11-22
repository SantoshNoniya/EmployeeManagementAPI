using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Infrasctructure.Persistence.Configurations
{
    public class UserTypeConfig : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("UserTypes");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(1500);

            builder.HasData(new List<UserType>()
            {
                new UserType
                {
                    Id = 1, Name = "Admin", Description = "Admin user with full access"
                },
                new UserType
                {
                    Id = 2, Name = "User", Description = "Regular user with limited access"
                }
           });

        }         
    
    }
}
