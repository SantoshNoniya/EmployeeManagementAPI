using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxCalculation.Domain.Entities;

namespace TaxCalculation.Infrasctructure.Persistence.Configurations
{
    public class EmployeeConfig:IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee", tb =>
            {
                tb.HasTrigger("Tr_tbl_on_Employee_BackUp");
                tb.UseSqlOutputClause(false);
            });
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);
            builder.Property(e => e.Name);
            builder.Property(e => e.Address); ;
            builder.Property(e => e.Salary);
            builder.Property(e => e.Department);
        }
    }
}
