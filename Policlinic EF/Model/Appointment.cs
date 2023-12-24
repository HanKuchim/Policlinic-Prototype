using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Policlinic_EF.Model
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        public  int DoctorId { get; set; }
        public Doctor ? Doctor { get; set; }
        public int ServiceId { get; set; }
        

        private DateOnly appointmentDate;

        public DateOnly AppointmentDate
        {
            get
            {
                return appointmentDate;
            }
            set
            {
                if (Doctor != null && Doctor.DoctorShedules != null &&
                    Doctor.DoctorShedules.Any(ds => ds.WeekDay?.WeekDayName == value.DayOfWeek.ToString()))
                {
                    appointmentDate = value;
                }
                else
                {
                    throw new Exception("Выбранная дата не соответствует рабочим дням доктора.");
                }
            }
        }

        public int PairId;

        private Pair appointmentTime;

        public Pair AppointmentTime
        {
            get
            {
                return appointmentTime;
            }
            set
            {
                // Проверка находится ли время в промежутке работы доктора в этот день
                if (Doctor != null && Doctor.DoctorShedules.Any(ds => ds.WeekDay?.WeekDayName == AppointmentDate.DayOfWeek.ToString() && value.IsBetween(ds.WorkTime.StartTime, ds.WorkTime.EndTime)))
                {
                    // Проверка наличия перекрытий с другими приемами у доктора в этот день
                    if (Doctor.DoctorAppoitments.Any(ap => ap.AppointmentDate == AppointmentDate && value.Overlaps(ap.AppointmentTime)))
                    {
                        throw new Exception("Выбранное время пересекается с другим приемом доктора в этот день");
                    }
                    appointmentTime = value;
                }
                else
                {
                    throw new Exception("Выбранное время не входит в промежуток работы доктора в этот день");
                }
            }
        }
        
    }
}
