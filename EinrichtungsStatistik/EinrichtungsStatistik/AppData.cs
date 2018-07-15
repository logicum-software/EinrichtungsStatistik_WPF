using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinrichtungsStatistik
{
    [Serializable]
    class AppData
    {
        public List<Fragebogen> appFrageboegen { get; set; }
        public List<Frage> appFragen { get; set; }

        public AppData()
        {
            appFrageboegen = new List<Fragebogen>();
            appFragen = new List<Frage>();
        }
    }
}
