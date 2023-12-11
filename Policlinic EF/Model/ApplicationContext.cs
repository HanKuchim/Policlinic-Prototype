using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Policlinic_EF.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities{ get; set; }
        public DbSet<WeekDay> WeekDayList { get; set; }
        public DbSet<DoctorShedule> DoctorShedules { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Pair> Pairs { get; set; }

        public ApplicationContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Учеба\\Лабы\\C#\\SQL\\Policlinic Prototype\\Policlinic EF\\DB\\Policlinic.mdf\";Integrated Security=True;Initial Catalog=Policlinic");
        }
    }
}
