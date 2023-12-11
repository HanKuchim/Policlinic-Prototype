using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using Policlinic_EF.Model;
using PoliclinicWpf.Context;
using PoliclinicWpf.Model.BaseModel;
using PoliclinicWpf.View;


namespace PoliclinicWpf.ModelView
{
    public class MainWindowVM : BaseChanged
    {
        private ApplicationContext context;
        public ObservableCollection<WeekDay> WeekDays { get; set; }
        public ObservableCollection<Pair> Pairs { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<DoctorShedule> DoctorShedules { get; set; }
        public ObservableCollection<Speciality> Specialities { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public MainWindowVM()
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
        private RelayCommand showPatients;

        public RelayCommand ShowPatients
        {
            get
            {
                return showPatients ?? (
                    showPatients = new RelayCommand(obj=>
                    {
                        ViewWindow viewWindow = new ViewWindow(new PatientsControl());
                        viewWindow.Show();

                    }));
            }
        }

        private RelayCommand showDoctors;

        public RelayCommand ShowDoctors
        {
            get
            {
                return showDoctors ?? (
                    showDoctors = new RelayCommand(obj =>
                    {
                        ViewWindow viewWindow = new ViewWindow(new DoctorControl());
                        viewWindow.Show();
                        //LocatorControl.PatientControl.Content = new 
                        
                    }));
            }
        }
        
    }
}
