using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Policlinic_EF.Model
{
    public class WeekDay
    {
        public int WeekDayId { get; set; }
        private string weekDayName;

        public string WeekDayName
        {
            get
            {
                return weekDayName;
            }
            set
            {
                weekDayName = value;
            }
        }

        public ObservableCollection<DoctorShedule> DoctorShedules { get; set;}
    }
}
