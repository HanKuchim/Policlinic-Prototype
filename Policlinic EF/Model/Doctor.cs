using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Policlinic_EF.Model
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        private string doctorName;

        public string DoctorName
        {
            get
            {
                return doctorName;
            }
            set
            {
                doctorName = value;
            }
        }

        private int cabinetNum;
        public int CabinetNum
        {
            get
            {
                return cabinetNum;
            }
            set
            {
                cabinetNum = value;
            }
        }

        public ObservableCollection<Speciality> DoctorSpecialityLists { get; set; } = new();
        public ObservableCollection<DoctorShedule> DoctorShedules { get; set;} = new();
        public ObservableCollection<Appointment> DoctorAppoitments { get; set; } = new();
    }   
}
