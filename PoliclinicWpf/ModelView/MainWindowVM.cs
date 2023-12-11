using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PoliclinicWpf.Context;
using PoliclinicWpf.Model.BaseModel;
using PoliclinicWpf.View;


namespace PoliclinicWpf.ModelView
{
    public class MainWindowVM : BaseChanged
    {
        public MainWindowVM()
        {

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
