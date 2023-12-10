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
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }

        public DBContex()
        {
            using (context = new ApplicationContext())
            {
                context.Patients.Load();
                context.Doctors.Load();

                Patients = context.Patients.Local.ToObservableCollection();
                Doctors = context.Doctors.Local.ToObservableCollection();

                
            }
            
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
// как сделать выгрузку обратно