using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Policlinic_EF.Model;
using PoliclinicWpf.Model.BaseModel;

namespace PoliclinicWpf.ModelView
{
    public class DoctorAppointmentsVM : BaseChanged     
    {
        private ApplicationContext context = new ApplicationContext();
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }

        public DoctorAppointmentsVM(Doctor SelectedDoctorForApp)
        {
            context.Doctors.Load();
            context.Appointments.Load();

            Doctors = context.Doctors.Local.ToObservableCollection();
            Appointments = context.Appointments.Local.ToObservableCollection();
            SelectedDoctor = SelectedDoctorForApp;
        }
        public ObservableCollection<Appointment> AppointmentsForSelectedDate { get; set; }
        private RelayCommand _viewAppointmentsCommand;

        public RelayCommand ViewAppointmentsCommand
        {
            get
            {
                return _viewAppointmentsCommand ??= new RelayCommand(param =>
                {
                    AppointmentsForSelectedDate = new ObservableCollection<Appointment>(
                        from appointment in SelectedDoctor.DoctorAppoitments
                        where appointment.AppointmentDate == DateOnly.FromDateTime(SelectedDateTime)
                        select appointment);
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
