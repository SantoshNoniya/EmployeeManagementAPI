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
    public class UserRoleMappingConfig:IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.ToTable("UserRoleMappings");           
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.HasIndex(e => new {e.UserId,e.RoleId}, "UK_UserRoleMapping").IsUnique();
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.RoleId).IsRequired();


            builder.HasOne(e => e.Role)
              .WithMany(e => e.UserRoleMappings)
              .HasForeignKey(e => e.RoleId)
              .HasConstraintName("FK_UserRoleMapping_Role");

            builder.HasOne(e => e.User)
            .WithMany(e => e.UserRoleMappings)
            .HasForeignKey(e => e.UserId)
            .HasConstraintName("FK_UserRoleMapping_User");
        }
    }
}
