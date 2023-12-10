using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Policlinic_EF.Model;
using PoliclinicWpf.Context;

namespace PoliclinicWpf.ModelView
{
    public class PatientViewVM
    {
        public ObservableCollection<Patient> Patients { get; set; }
        public PatientViewVM()
        {
            Patients = DatabaseLocator.Context.Patients;
        }
    }
}
