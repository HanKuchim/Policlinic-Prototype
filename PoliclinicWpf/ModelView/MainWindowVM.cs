using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Policlinic_EF.Model;
using PoliclinicWpf.Context;
using PoliclinicWpf.Model.BaseModel;
using PoliclinicWpf.View;


namespace PoliclinicWpf.ModelView
{
    public class MainWindowVM : BaseChanged
    {
        #region DataBase
        
        private  ApplicationContext context;
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
        #endregion




        #region Commands

        private RelayCommand showPatients;

        public RelayCommand ShowPatients
        {
            get
            {
                return showPatients ?? (
                    showPatients = new RelayCommand(o =>
                    {
                        var window = new ContentWindow();
                        window.Owner = Application.Current.MainWindow;
                        window.Title = "Patients";
                        window.DataContext = this;
                        Content = new PatientsControl();
                        window.Show();
                        Application.Current.MainWindow.Hide();
                        window.Closed += (sender, args) =>
                        {
                            Application.Current.MainWindow.Show();
                        };
                    }, o => true ));
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
                        var window = new ContentWindow();
                        window.Owner = Application.Current.MainWindow;
                        window.Title = "Doctors";
                        window.DataContext = this;
                        Content = new DoctorsControl();
                        window.Show();
                        Application.Current.MainWindow.Hide();
                        window.Closed += (sender, args) =>
                        {
                            Application.Current.MainWindow.Show();
                        };
                    }));
            }
        }
        public ObservableCollection<DoctorShedule> SelectedDoctorShedules { get; set; }
        


        private RelayCommand _displayDoctorSchedule;

        public RelayCommand DisplayDoctorSchedule
        {
            get => _displayDoctorSchedule ?? (_displayDoctorSchedule = new RelayCommand(obj =>
            {
                SelectedDoctorShedules = new ObservableCollection<DoctorShedule>(
                    SelectedDoctor.DoctorShedules);
                var window = new DoctorSchedule();
                window.Owner = Application.Current.MainWindow.OwnedWindows[0];
                window.DataContext = this;
                window.ShowDialog();
            },o => SelectedDoctor != null ? true : false ));
        }

        

        private RelayCommand _changeDoctorSchedule;

        public RelayCommand ChangeDoctorSchedule
        {
            get => _changeDoctorSchedule ?? (_changeDoctorSchedule = new RelayCommand(o =>
            {
                // тут написать код изменяющий расписание)
                
                var window = Application.Current.MainWindow.OwnedWindows[0].OwnedWindows[0];
                window.Close();
            }, o =>
            {
                return true;
                /*(SelectedWeekDays != null)? true : false;*/
            } ));
        }
        private RelayCommand _displayDoctorAppointments;

        public RelayCommand DisplayDoctorAppointments
        {
            get => _displayDoctorAppointments ?? (_displayDoctorAppointments = new RelayCommand(obj =>
            {
                var window = new DoctorAppointments();
                window.Owner = Application.Current.MainWindow.OwnedWindows[0];
                window.DataContext = this;
                window.ShowDialog();
            }, o => SelectedDoctor != null ? true : false));
        }

        private RelayCommand _makeAppointmentCommand;

        public RelayCommand ApointmentCommand
        {
            get
            {
                return _makeAppointmentCommand ?? (_makeAppointmentCommand = new RelayCommand(o =>
                {
                    // тут написать код записывающий на прием)
                    var window = Application.Current.MainWindow.OwnedWindows[0].OwnedWindows[0];
                    window.Close();

                }, o =>
                {
                    return (SelectedDoctor != null)&&(SelectedDateTime != null) ? true : false;
                }));
            }
        }
        private RelayCommand _openAppointmentWindow;

        public RelayCommand OpenAppointmentWindow
        {
            get
            {
                return _openAppointmentWindow ?? (_openAppointmentWindow = new RelayCommand(o =>
                {
                    var window = new AppointmentWindow();
                    window.Owner = Application.Current.MainWindow.OwnedWindows[0];
                    window.DataContext = this;
                    window.ShowDialog();
                }, o =>
                {
                    return SelectedPatient != null ? true : false;
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
                    } ))
                    ;

            }
        }

        private RelayCommand _CheckedDays;

        public RelayCommand CheckedDays
        {
            get => _CheckedDays ?? (_CheckedDays = new RelayCommand(o =>
            {

            }));
        }

        #endregion
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
      

        private Patient _selectedPatient;

        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                _selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
            }
        }

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
        private ContentControl _content;
        public ContentControl Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                OnPropertyChanged("Content");
            }
        }
        private ObservableCollection<Appointment> _appointmentsforselecteddate;

        public ObservableCollection<Appointment> AppointmentsForSelectedDate
        {
            get { return _appointmentsforselecteddate; }
            set
            {
                _appointmentsforselecteddate = value;
                OnPropertyChanged(nameof(AppointmentsForSelectedDate));
            }
        }
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
        //Мб как то обнулять время после выхода из окон в которых они используются?
        private DateTime _selectedStartTime;

        public DateTime SelectedStartTime
        {
            get { return _selectedStartTime;}
            set
            {
                _selectedStartTime = value; 
                OnPropertyChanged(nameof(SelectedStartTime));
            }
        }
        private DateTime _selectedEndTime;

        public DateTime SelectedEndTime
        {
            get { return _selectedEndTime; }
            set
            {
                _selectedEndTime = value;
                OnPropertyChanged(nameof(SelectedEndTime));
            }
        }

        private WeekDay _selectedWeekDay;

        public WeekDay SelectedWeekDay
        {
            get { return _selectedWeekDay; }
            set
            { 
                _selectedWeekDay = value;
                OnPropertyChanged(nameof(SelectedWeekDay));
            }
        }

        private RelayCommand _addAppoitment;

        public RelayCommand AddAppoitment
        {
            get
            {
                return _addAppoitment ?? (_addAppoitment = new RelayCommand(obj =>
                    {
                        Appointment newAppointment = new Appointment
                        {
                            AppointmentDate = DateOnly.FromDateTime(SelectedDateTime),
                            AppointmentTime = new Pair(TimeOnly.FromDateTime(SelectedStartTime),
                                TimeOnly.FromDateTime(SelectedEndTime)),
                            Doctor = SelectedDoctor,
                            Patient = SelectedPatient
                        };
                        Appointments.Add(newAppointment);
                    }, o =>
                    {
                        //валидация
                        return true;
                    })
                {

                });

            }
        }

        private DoctorShedule _selectedShedule;

        public DoctorShedule SelectedShedule
        {
            get { return _selectedShedule; }
            set { _selectedShedule = value; OnPropertyChanged(nameof(SelectedShedule)); }
        }

    }
}
