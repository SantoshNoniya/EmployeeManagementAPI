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
    public class UserConfig:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.UserName).IsRequired();
            builder.Property(e => e.Password).IsRequired();
            builder.Property(e => e.PasswordSalt).IsRequired();
            builder.Property(e => e.UserTypeId).IsRequired();
            builder.Property(e => e.IsActive).IsRequired();
            builder.Property(e => e.IsDeleted).IsRequired();
            builder.Property(e => e.CreatedBy).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.ModifiedBy).IsRequired();
            builder.Property(e => e.ModifiedDate).IsRequired();

            builder.HasOne(e => e.UserType)
                   .WithMany(e=>e.Users)
                   .HasForeignKey(e => e.UserTypeId)
                   .HasConstraintName("FK_Users_UserTypes");

        }
    }
}
