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
        private Fragebogen tmpFragebogen;

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

        internal Fragebogen getFragebogen()
        {
            return tmpFragebogen;
        }

        private void buttonAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void listViewAuswahl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewAuswahl.SelectedItem != null)
                buttonLaden.IsEnabled = true;
            else
                buttonLaden.IsEnabled = false;
        }

        private void buttonLaden_Click(object sender, RoutedEventArgs e)
        {
            if (listViewAuswahl.SelectedItem != null)
            {
                tmpFragebogen = new Fragebogen(tmpFrageboegen.ElementAt(listViewAuswahl.SelectedIndex).strName,
                tmpFrageboegen.ElementAt(listViewAuswahl.SelectedIndex).Fragen);
            }

            this.Close();
        }
    }
}
