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
        public ArrayList Fragen { get; set; }

        public Fragebogen()
        {
            strName = "";
            Fragen = new ArrayList();
        }

        public Fragebogen(String name, ArrayList fragen)
        {
            strName = name;
            Fragen = new ArrayList(fragen);
        }
    }
}
