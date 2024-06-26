﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Data.Configrations
{
    internal class DepartmentConfigrations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(D => D.Code).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(D => D.Id).UseIdentityColumn(10,10);
            
        }
    }
}
