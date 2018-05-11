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
    /// Interaktionslogik für NeueFrage.xaml
    /// </summary>
    public partial class NeueFrage : Window
    {
        public NeueFrage()
        {
            InitializeComponent();
            textBoxFragetext.Focus();
            textBoxFragetext.SelectAll();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBoxFragetext_TextInput(object sender, TextCompositionEventArgs e)
        {
            // Funktioniert noch nicht !!!
            buttonSave.IsEnabled = true;
        }
    }
}
