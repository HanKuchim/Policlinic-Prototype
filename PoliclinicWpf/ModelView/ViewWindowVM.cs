using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PoliclinicWpf.Context;

namespace PoliclinicWpf.ModelView
{
    public class ViewWindowVM
    {
        public ViewWindowVM(ContentControl con)
        {
            ViewWindowControl = con;
        }

        public ContentControl ViewWindowControl { get; set; }
    }
}
