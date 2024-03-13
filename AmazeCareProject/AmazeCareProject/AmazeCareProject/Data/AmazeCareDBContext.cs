using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CodeFirst.Models;

namespace AmazeCareProject.Data
{
    public class AmazeCareDBContext : DbContext
    {
        public AmazeCareDBContext (DbContextOptions<AmazeCareDBContext> options)
            : base(options)
        {
        }

        public DbSet<CodeFirst.Models.Admin> Admin { get; set; } = default!;

        public DbSet<CodeFirst.Models.Appointments>? Appointments { get; set; }

        public DbSet<CodeFirst.Models.Doctors>? Doctors { get; set; }

        public DbSet<CodeFirst.Models.MedicalRecords>? MedicalRecords { get; set; }

        public DbSet<CodeFirst.Models.Patient>? Patient { get; set; }
    }
}
