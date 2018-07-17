using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinrichtungsStatistik
{
    [Serializable]
    class Fragebogen
    {
        public String strName { get; set; }
        public List<Frage> Fragen { get; set; }

        public Fragebogen()
        {
            strName = "";
            Fragen = new List<Frage>();
        }

        public Fragebogen(String name, List<Frage> fragen)
        {
            strName = name;
            Fragen = new List<Frage>(fragen);
        }
    }
}
