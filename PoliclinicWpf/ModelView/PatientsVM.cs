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
using PoliclinicWpf.View;

namespace PoliclinicWpf.ModelView
{
    public class PatientsVM : BaseChanged
    {
        private ApplicationContext context = new ApplicationContext();
        public ObservableCollection<WeekDay> WeekDays { get; set; }
        public ObservableCollection<Pair> Pairs { get; set; }
        public ObservableCollection<Patient> PatientsObs { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<DoctorShedule> DoctorShedules { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }

        public PatientsVM()
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
        }

        #region Commands

        
        private RelayCommand _createPatient;

        public RelayCommand CreatePatient
        {
            get
            {
                return _createPatient = new RelayCommand(o =>
                {
                    var name = CreateStrings[0];
                    var description = CreateStrings[1];
                    //create patient logic
                    var newPatient = new Patient
                    {
                        Name = CreateStrings[0],
                    };

                    context.Patients.Add(newPatient);
                    context.SaveChanges();
                    
                    ExitWindow.Execute(o);
                    CreateStrings = new List<string>()
                    {
                        "",
                        ""
                    };
                }, o =>
                {
                    return CreateStrings[0].Length != 0;
                });
            }
        }

        private RelayCommand _createPatientWindow;

        public RelayCommand CreatePatientWindow
        {
            get
            {
                return _createPatientWindow ?? (_createPatientWindow = new RelayCommand(o =>
                {
                    var window = new CreatePatient();
                    window.Owner = o as Window;
                    window.DataContext = this;
                    window.ShowDialog();
                }, o =>
                {
                    return true;
                }));
            }
        }

        private RelayCommand _deletePatient;

        public RelayCommand DeletePatient
        {
            get
            {
                return _deletePatient ?? (_deletePatient = new RelayCommand(o =>
                {
                    var patient = SelectedPatient;
                    //...
                    using (context)
                    {
                        context.Patients.Remove(patient);
                        context.SaveChanges();
                    }
                }, o =>
                {
                    return SelectedPatient != null;
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
        private RelayCommand _addAppoitment;

        public RelayCommand AddAppoitment
        {
            get
            {
                return _addAppoitment ?? (_addAppoitment = new RelayCommand(obj =>
                        {
                            DateOnly Date = DateOnly.FromDateTime(SelectedDateTime);
                            var Time = new Pair(TimeOnly.FromDateTime(SelectedStartTime),
                                TimeOnly.FromDateTime(SelectedEndTime));

                            Appointment newAppointment = new Appointment
                            {
                                Doctor = SelectedDoctor,
                                Patient = SelectedPatient,
                                AppointmentDate = Date,
                                AppointmentTime = Time,
                            };
                            using (var context = new ApplicationContext())
                            {
                                context.Entry(Time).State = EntityState.Added;
                                context.Entry(newAppointment).State = EntityState.Added;
                                //context.Appointments.Add(newAppointment);
                                context.SaveChanges();
                            }
                        }, o =>
                        {
                            //валидация
                            return true;
                        })
                    );

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

        #endregion

        #region Parametrs
        private List<string> _creationStrings = new List<string>()
        {
            "",
            "",
        };

        public List<string> CreateStrings
        {
            get { return _creationStrings; }
            set
            {
                _creationStrings = value;
                OnPropertyChanged(nameof(CreateStrings));
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
        private DateTime _selectedStartTime;

        public DateTime SelectedStartTime
        {
            get { return _selectedStartTime; }
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



        private DoctorShedule _selectedShedule;

        public DoctorShedule SelectedShedule
        {
            get { return _selectedShedule; }
            set { _selectedShedule = value; OnPropertyChanged(nameof(SelectedShedule)); }
        }
        private DoctorSchedule _selectedScheduleDay;

        public DoctorSchedule SelectedScheduleDay
        {
            get { return _selectedScheduleDay; }
            set
            {
                _selectedScheduleDay = value;
                OnPropertyChanged(nameof(SelectedScheduleDay));
            }
        }
        #endregion
    }
}
