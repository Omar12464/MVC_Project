using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Project.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project.DAL.Data.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
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
        }
    }
}
