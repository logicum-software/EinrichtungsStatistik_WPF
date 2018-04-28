using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinrichtungsStatistik
{
    class Frage
    {
        private String strFragetext;
        private int nAntwortart;

        public Frage()
        {
            strFragetext = "";
            nAntwortart = 0;
        }

        public Frage(String fragetext, int antwortart)
        {
            strFragetext = fragetext;
            nAntwortart = antwortart;
        }

        internal String getFragetext()
        {
            return strFragetext;
        }

        internal void setFragetext(String fragetext)
        {
            strFragetext = fragetext;
        }

        internal int getAntwortart()
        {
            return nAntwortart;
        }

        internal void setAntwortart(int antwortart)
        {
            nAntwortart = antwortart;
        }
    }
}
