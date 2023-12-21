using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Policlinic_EF.Model
{
    public class Pair
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        private TimeOnly policlinicStartTime = new TimeOnly(8, 00);
        private TimeOnly policlinicEndTime = new TimeOnly(18, 00);

        private TimeOnly startTime;

        public TimeOnly StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                if (value.IsBetween(policlinicStartTime, policlinicEndTime))
                {
                    startTime = value;
                }
                else
                {
                    throw new Exception("Время вне диапозона работы");
                }
            }
        }

        private TimeOnly endTime;
        public TimeOnly EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                if (value.IsBetween(policlinicStartTime, policlinicEndTime))
                {
                    endTime = value;
                }
                else
                {
                    throw new Exception("Время вне диапозона работы");
                }
            }
        }

        public ObservableCollection<Appointment> Appoitments { get; set; }

        public Pair()
        {
        }

        public Pair(TimeOnly startTime, TimeOnly endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
        public Pair(int id, TimeOnly startTime, TimeOnly endTime)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
        }
        public bool IsBetween(TimeOnly startTime, TimeOnly endTime)
        {
            return this.startTime >= startTime && this.endTime <= endTime;
        }
        public bool Overlaps(Pair other)
        {
            return StartTime < other.EndTime && other.StartTime < EndTime;
        }

        public override string ToString() => $"{StartTime} - {EndTime}";


    }
}
