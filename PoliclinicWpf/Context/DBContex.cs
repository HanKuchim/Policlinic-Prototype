using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Microsoft.EntityFrameworkCore;
using Policlinic_EF.Model;

namespace PoliclinicWpf.Context
{
    public class DBContex
    {
        private ApplicationContext context;
        public ObservableCollection<WeekDay> WeekDays { get; set; }
        public ObservableCollection<Pair> Pairs { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public  ObservableCollection<DoctorShedule> DoctorShedules { get; set; }
        public  ObservableCollection<Speciality> Specialities { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }



        public DBContex()
        {
            using (context = new ApplicationContext())
            {
                context.WeekDayList.Load();
                context.Pairs.Load();
                context.Patients.Load();
                context.Doctors.Load();
                context.DoctorShedules.Load();
                context.Specialities.Load();
                context.Appointments.Load();

                WeekDays = context.WeekDayList.Local.ToObservableCollection();
                Pairs = context.Pairs.Local.ToObservableCollection();
                Patients = context.Patients.Local.ToObservableCollection();
                Doctors = context.Doctors.Local.ToObservableCollection();
                DoctorShedules = context.DoctorShedules.Local.ToObservableCollection();
                Specialities = context.Specialities.Local.ToObservableCollection();
                Appointments = context.Appointments.Local.ToObservableCollection();
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
// как сделать выгрузку обратно