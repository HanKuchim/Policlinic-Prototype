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
    public class DoctorVM : BaseChanged
    {
        private ApplicationContext context = new ApplicationContext();
        public ObservableCollection<WeekDay> WeekDays { get; set; }
        public ObservableCollection<Pair> Pairs { get; set; }
        public ObservableCollection<Patient> PatientsObs { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<DoctorShedule> DoctorShedules { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }


        public DoctorVM()
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

        private RelayCommand _createDoctor;

        public RelayCommand CreateDoctor
        {
            get
            {
                return _createDoctor ?? (_createDoctor = new RelayCommand(o =>
                {
                    var name = CreateStrings[0];
                    var cabinetNum = CreateStrings[1];

                    //create doctor logic
                    var newdoctor = new Doctor()
                    {
                        DoctorName = CreateStrings[0],
                        CabinetNum = int.Parse(CreateStrings[1]),
                    };
                    
                        context.Doctors.Add(newdoctor);
                        context.SaveChanges();
                    
                    ExitWindow.Execute(o);
                    CreateStrings = new List<string>()
                    {
                        "",
                        ""
                    };
                }, o =>
                {
                    return CreateStrings[0].Length != 0 && int.TryParse(CreateStrings[1], out int result);
                }));
            }
        }

        private RelayCommand _createDoctorWindow;

        public RelayCommand CreateDoctorWindow
        {
            get
            {
                return _createDoctorWindow ?? (_createDoctorWindow = new RelayCommand(o =>
                {
                    var window = new CreateDoctor();
                    window.Owner = o as Window;
                    window.DataContext = this;
                    window.ShowDialog();
                }, o =>
                {
                    return true;
                }));
            }
        }
        private RelayCommand _deleteDoctor;

        public RelayCommand DeleteDoctor
        {
            get
            {
                return _deleteDoctor ?? (_deleteDoctor = new RelayCommand(o =>
                {
                    var doctor = selectedDoctor;
                    //...
                    using (context)
                    {
                        context.Doctors.Remove(doctor);
                        context.SaveChanges();
                    }
                }, o =>
                {
                    return selectedDoctor != null;
                }));
            }
        }

        private RelayCommand _displayDoctorSchedule;

        public RelayCommand DisplayDoctorSchedule
        {
            get => _displayDoctorSchedule ?? (_displayDoctorSchedule = new RelayCommand(obj =>
            {
                var window = new DoctorSchedule();
                window.Owner = Application.Current.MainWindow.OwnedWindows[0];
                window.DataContext = new DoctorShedulesVM(SelectedDoctor);
                window.ShowDialog();
            }, o => SelectedDoctor != null ? true : false));
        }

        
        private RelayCommand _displayDoctorAppointments;

        public RelayCommand DisplayDoctorAppointments
        {
            get => _displayDoctorAppointments ?? (_displayDoctorAppointments = new RelayCommand(obj =>
            {
                var window = new DoctorAppointments(SelectedDoctor);
                window.Owner = Application.Current.MainWindow.OwnedWindows[0];
               // window.DataContext = new DoctorAppointmentsVM(SelectedDoctor);
                window.ShowDialog();
            }, o => SelectedDoctor != null ? true : false));
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
        #endregion
    }
}
