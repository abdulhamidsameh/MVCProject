using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Data.Configrations
{
    internal class EmployeeConfigrations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar")
                .HasMaxLength(50).IsRequired();
            builder.Property(E => E.Address).IsRequired();
            builder.Property(E => E.Salary).HasColumnType("decimal(12,2)");
            builder.Property(E => E.Gender).HasConversion(
                (Gender) => Gender.ToString(),
                (GenderStr) => (Gender) Enum.Parse(typeof(Gender),GenderStr)
                );
            builder.Property(E => E.EmployeeType).HasConversion(
                (EmpType) => EmpType.ToString(),
                (EmpTypeStr) => (EmpType)Enum.Parse(typeof(EmpType), EmpTypeStr)
                );
        }
    }
}
