using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Policlinic_EF.Model
{
    public class Speciality
    {
        public int Id { get; set; }
        private string specialityName;

        public string SpecialityName
        {
            get
            {
                return specialityName;
            }
            set
            {
                specialityName = value;
            }
        }
        
        public ObservableCollection<Doctor> Doctors { get; set; } = new();
    }
}
