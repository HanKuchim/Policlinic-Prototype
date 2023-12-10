using PoliclinicWpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PoliclinicWpf.Model.BaseModel;

namespace PoliclinicWpf.Context
{
    public static class LocatorControl 
    {
        private static ContentControl _content;
        public static ContentControl Content    
        {
            get
            {
                if (_content == null)
                {
                    _content = new UserControl1();

                }
                return _content;
            }
            set
            {
                _content = value;

            }
        }

        private static ContentControl _patientControl;

        public static ContentControl PatientControl
        {
            get
            {
                if (_patientControl == null)
                {
                    _patientControl = new PatientsControl();
                }
                return _patientControl;
            }
            set
            {
                _patientControl = value;
            }
        }
        private static ContentControl _doctorControl;

        public static ContentControl DoctorControl
        {
            get
            {
                if (_doctorControl == null)
                {
                    _doctorControl = new DoctorControl();
                }
                return _doctorControl;
            }
            set
            {
                _doctorControl = value;
            }
        }
    }
}
