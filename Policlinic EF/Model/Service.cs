using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Policlinic_EF.Model
{
    public class Service
    {
        public int Id { get; set; }

        private string serviceName;

        public string ServiceName
        {
            get
            {
                return serviceName;
            }
            set
            {
                serviceName = value;
            }
        }

        public ObservableCollection<Speciality> SpecialityList { get; set; } = new();
        private double cost;

        public double Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }
    }
}
