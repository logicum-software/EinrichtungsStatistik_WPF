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
        private ArrayList Frageboegen;
        private ArrayList Fragen;

        public AppData()
        {
            Frageboegen = new ArrayList();
            Fragen = new ArrayList();
        }

        public AppData(ArrayList frageboegen, ArrayList fragen)
        {
            Frageboegen = frageboegen;
            Fragen = fragen;
        }

        internal ArrayList getFrageboegen()
        {
            return Frageboegen;
        }

        internal void setFrageboegen(ArrayList frageboegen)
        {
            Frageboegen = frageboegen;
        }

        internal ArrayList getFragen()
        {
            return Fragen;
        }

        internal void setFragen(ArrayList fragen)
        {
            Fragen = fragen;
        }

        internal void addFrage(Frage frage)
        {
            if (frage != null)
                Fragen.Add(frage);
        }
    }
}
