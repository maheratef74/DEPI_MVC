﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPH_AllHierarchy.DataAccessLayer.Entities;

namespace TPH.DataAccessLayer.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasDiscriminator<string>("EmployeeType")
                .HasValue<Admin>("Admin")
                .HasValue<Developer>("Dev");
        }
    }
}
