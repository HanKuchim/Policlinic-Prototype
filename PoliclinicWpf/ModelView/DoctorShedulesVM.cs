using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using Policlinic_EF.Model;
using PoliclinicWpf.Model.BaseModel;
using PoliclinicWpf.View;

namespace PoliclinicWpf.ModelView
{
    public class DoctorShedulesVM : BaseChanged
    {
        private ApplicationContext context = new ApplicationContext();
        public ObservableCollection<DoctorShedule> SelectedDoctorShedules { get; set; }
        public ObservableCollection<WeekDay> WeekDays { get; set; }
        public ObservableCollection<Pair> Pairs { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<DoctorShedule> DoctorShedules { get; set; }
        public DoctorShedulesVM(Doctor SelectedDoctorShedulesVM)
        {
            context.WeekDayList.Load();
            context.Pairs.Load();
            context.Doctors.Load();
            context.DoctorShedules.Load();

            WeekDays = context.WeekDayList.Local.ToObservableCollection();
            Pairs = context.Pairs.Local.ToObservableCollection();
            Doctors = context.Doctors.Local.ToObservableCollection();
            DoctorShedules = context.DoctorShedules.Local.ToObservableCollection();
            SelectedDoctor = SelectedDoctorShedulesVM;
            SelectedDoctorShedules = new ObservableCollection<DoctorShedule>(
                SelectedDoctorShedulesVM.DoctorShedules);
        }
        

        private RelayCommand _addScheduleDayWindow;

        public RelayCommand AddScheduleDayWindow
        {
            get
            {
                return _addScheduleDayWindow ?? (_addScheduleDayWindow = new RelayCommand(o =>
                {
                    var window = new NewSchedule();
                    window.DataContext = this;
                    window.Owner = o as Window;
                    window.ShowDialog();
                }));
            }
        }
        private RelayCommand _addScheduleDay;

        public RelayCommand AddScheduleDay
        {
            get
            {
                return _addScheduleDay ?? (_addScheduleDay = new RelayCommand(o =>
                {
                    var PairForNewShedule = new Pair(TimeOnly.FromDateTime(SelectedStartTime),
                        TimeOnly.FromDateTime(SelectedEndTime));
                    var newshedule = new DoctorShedule()
                    {
                        WeekDay = SelectedWeekDay,
                        Doctor = SelectedDoctor,
                        WorkTime = PairForNewShedule
                    };
                    using (var con = new ApplicationContext())
                    {
                        //con.Pairs.Add(PairForNewShedule);
                        //con.DoctorShedules.Add(newshedule);
                        SelectedDoctorShedules.Add(newshedule);
                        con.Entry(PairForNewShedule).State = EntityState.Added;
                        con.Entry(newshedule).State = EntityState.Added;
                        con.SaveChanges();
                    }
                    
                }));
            }
        }
        private RelayCommand _deleteShedule;

        public RelayCommand DeleteShedule
        {
            get
            {
                return _deleteShedule ?? (_deleteShedule = new RelayCommand(o =>
                {
                    using (var q = new ApplicationContext())
                    {
                        q.DoctorShedules.Remove(SelectedShedule);
                        //context.Entry(SelectedShedule).State = EntityState.Deleted;
                        SelectedDoctorShedules.Remove(SelectedShedule);
                        q.SaveChanges();
                    }
                    
                    
                }, o =>
                {
                    return SelectedShedule != null;
                }));
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
