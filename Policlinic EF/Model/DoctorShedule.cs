using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Policlinic_EF.Model
{
    public class DoctorShedule
    {
        public int DoctorSheduleId { get; set; }
        public int WeekDayId { get; set; }
        public WeekDay? WeekDay { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        private Pair workTime;
        public Pair WorkTime
        {
            get
            {
                return workTime;
            }
            set
            {
                workTime = value;
            }
        }
    }
}
