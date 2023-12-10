using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Policlinic_EF.Model;
using PoliclinicWpf.Context;
using PoliclinicWpf.ModelView;

namespace PoliclinicWpf.View
{
    /// <summary>
    /// Interaction logic for PatientsControl.xaml
    /// </summary>
    public partial class PatientsControl : UserControl
    {
        public PatientsControl()
        {
            InitializeComponent();
            DataContext = new PatientViewVM();
        }
    }
}
