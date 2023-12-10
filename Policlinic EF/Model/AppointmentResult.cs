using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Policlinic_EF.Model
{
    [Owned]
    public class AppointmentResult
    {
        //Зависит от PatientAppointment
        private string simptomsDescriptoin;

        public string SimptomsDescriptoin
        {
            get
            {
                return simptomsDescriptoin;
            }
            set
            {
                simptomsDescriptoin = value;
            }
        }

        private string treatmentRecommendations;

        public string TreatmentRecommendations
        {
            get
            {
                return treatmentRecommendations;
            }
            set
            {
                treatmentRecommendations = value;
            }
        }
    }
}
