using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.DAL.Data.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsRequired();
            builder.Property(d => d.Id).UseIdentityColumn(2, 10);
            builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");
            builder.Property(E => E.gender)
                    .HasConversion(
                        (Gender)=>Gender.ToString(),
                        (Gender)=>(Gender)Enum.Parse(typeof(Gender),Gender)
                     );
            builder.Property(E => E.Emp_type)
                .HasConversion(
                 (EmpType) => EmpType.ToString(),
                 (TypeAs)=>(EmpType)Enum.Parse(typeof(EmpType),TypeAs)
                 );
            builder.Property(E=>E.Name).IsRequired(true).HasMaxLength(100);
            builder.Property(E => E.Salary).HasColumnType("money");

        }
    }
}
