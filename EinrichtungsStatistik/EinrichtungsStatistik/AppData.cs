using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinrichtungsStatistik
{
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
    }
}
