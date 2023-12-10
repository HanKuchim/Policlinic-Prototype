using Policlinic_EF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliclinicWpf.Context
{
    public static class DatabaseLocator
    {
        public static DBContex Context{ get; set; } = new DBContex();

    }
}
