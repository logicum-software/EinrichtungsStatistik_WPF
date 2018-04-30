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
using System.Windows.Shapes;

namespace EinrichtungsStatistik
{
    /// <summary>
    /// Interaktionslogik für Fragebogenentwurf.xaml
    /// </summary>
    public partial class Fragebogenentwurf : Window
    {
        public Fragebogenentwurf()
        {
            InitializeComponent();
        }

        private void buttonSchliessen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
