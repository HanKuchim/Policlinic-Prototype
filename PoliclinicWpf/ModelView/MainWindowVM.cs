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
using PoliclinicWpf.Model.BaseModel;
using PoliclinicWpf.View;


namespace PoliclinicWpf.ModelView
{
    public class MainWindowVM : BaseChanged
    { 
        #region DataBase

        private RelayCommand showPatients;

        public RelayCommand ShowPatients
        {
            get
            {
                return showPatients ?? (
                    showPatients = new RelayCommand(o =>
                    {
                        var window = new PatientWindow();
                        window.Owner = Application.Current.MainWindow;
                        window.Title = "Patients";
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
                        var window = new DoctorWindow();
                        window.Owner = Application.Current.MainWindow;
                        window.Title = "Doctors";
                        window.Show();
                        Application.Current.MainWindow.Hide();
                        window.Closed += (sender, args) =>
                        {
                            Application.Current.MainWindow.Show();
                        };
                    }));
            }
        }
        
        #endregion

    }
}
