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
    /// Interaktionslogik für Fragebogenauswahl.xaml
    /// </summary>
    public partial class Fragebogenauswahl : Window
    {
        private List<Fragebogen> tmpFrageboegen = new List<Fragebogen>();

        public Fragebogenauswahl()
        {
            InitializeComponent();

            listViewAuswahl.ItemsSource = tmpFrageboegen;
            listViewAuswahl.Items.Refresh();
        }

        internal void setFrageboegen(List<Fragebogen> frageboegen)
        {
            foreach (Fragebogen item in frageboegen)
                tmpFrageboegen.Add(item);
        }

        private void buttonAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
