using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccineRegistration.Models;

namespace VaccineRegistration.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VaccineRegistreeModel>().HasOne(v => v.AnswerModel).WithOne(a => a.VaccineRegistree).HasForeignKey<AnswerModel>(b => b.PatientId);
        }

        public DbSet<VaccineRegistreeModel> Patient { get; set; }
        public DbSet<AnswerModel> Questionaire { get; set; }

       
    }
}
