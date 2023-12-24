using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Policlinic_EF.Model;
using PoliclinicWpf.Model.BaseModel;

namespace PoliclinicWpf.ModelView
{
    public class DoctorAppointmentsVM : BaseChanged     
    {
        private ApplicationContext context = new ApplicationContext();
        public ObservableCollection<WeekDay> WeekDays { get; set; }
        public ObservableCollection<Pair> Pairs { get; set; }
        public ObservableCollection<Patient> PatientsObs { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<DoctorShedule> DoctorShedules { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }

        public DoctorAppointmentsVM(Doctor SelectedDoctorForApp)
        {
            context.WeekDayList.Load();
            context.Pairs.Load();
            context.Patients.Load();
            context.Doctors.Load();
            context.DoctorShedules.Load();
            context.Appointments.Load();

            WeekDays = context.WeekDayList.Local.ToObservableCollection();
            Pairs = context.Pairs.Local.ToObservableCollection();
            PatientsObs = context.Patients.Local.ToObservableCollection();
            Doctors = context.Doctors.Local.ToObservableCollection();
            DoctorShedules = context.DoctorShedules.Local.ToObservableCollection();
            Appointments = context.Appointments.Local.ToObservableCollection();

            SelectedDoctor = SelectedDoctorForApp;
        }
        public ObservableCollection<Appointment> AppointmentsForSelectedDate { get; set; } = new ObservableCollection<Appointment>();
        private RelayCommand _viewAppointmentsCommand;
        public string Text { get; set; }
        public RelayCommand ViewAppointmentsCommand
        {
            get
            {
                return _viewAppointmentsCommand ??= new RelayCommand(param =>
                {
                    // поменял метод поиска, заработало. Скорее всего проблема в коробке 
                    foreach (var item in Appointments)
                    {
                        if (item.AppointmentDate == DateOnly.FromDateTime(SelectedDateTime));
                        {
                            AppointmentsForSelectedDate.Add(item);
                        }
                    }
                    
                }, o =>
                {
                    return true;
                });
            }
        }
        private RelayCommand _exitWindow;

        public RelayCommand ExitWindow
        {
            get
            {
                return _exitWindow ?? (_exitWindow = new RelayCommand(o =>
                {
                    (o as Window).Close();
                }));
            }
        }

        private RelayCommand _mainMenu;

        public RelayCommand MainMenu
        {
            get
            {
                return _mainMenu ?? (_mainMenu = new RelayCommand(obj =>
                    {
                        var window = Application.Current.MainWindow.OwnedWindows[Application.Current.MainWindow.OwnedWindows.Count - 1];
                        window.Close();

                    }, o =>
                    {
                        return Application.Current.MainWindow.OwnedWindows.Count > 0 ? true : false;
                    }))
                    ;

            }
        }       

        #region Parametrs

        private Doctor selectedDoctor;

        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
            }
        }

        private DateTime _selecteDateTime;

        public DateTime SelectedDateTime
        {
            get { return _selecteDateTime; }
            set
            {
                _selecteDateTime = value;
                OnPropertyChanged("SelectedDateTime");
            }
        }


        #endregion
    }
}
