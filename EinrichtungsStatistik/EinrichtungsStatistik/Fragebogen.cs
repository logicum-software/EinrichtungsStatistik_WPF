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
        private String strName;
        private ArrayList Fragen;

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

        internal String getName()
        {
            return strName;
        }

        internal void setName(String name)
        {
            strName = name;
        }

        internal ArrayList getFragen()
        {
            return Fragen;
        }

        internal void setFragen(ArrayList fragen)
        {
            Fragen = fragen;
        }
    }
}
