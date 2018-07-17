using System.Windows;
using System.Windows.Controls;

namespace EinrichtungsStatistik
{
    /// <summary>
    /// Interaktionslogik für NeueFrage.xaml
    /// </summary>
    public partial class NeueFrage : Window
    {
        private Frage tmpFrage;

        public NeueFrage()
        {
            InitializeComponent();
            textBoxFragetext.Focus();
            textBoxFragetext.SelectAll();
        }

        internal Frage getFrage()
        {
            return tmpFrage;
        }

        internal void setFrage(Frage frage)
        {
            tmpFrage = frage;
            textBoxFragetext.Text = tmpFrage.strFragetext;

            if (tmpFrage.nAntwortart == 0)
                radioButtonAuswahl.IsChecked = true;
            else
                radioButtonFreierText.IsChecked = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void textBoxFragetext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buttonSave != null)
            {
                if (textBoxFragetext.Text != "" && textBoxFragetext.Text != "Bitte Fragetext eingeben...")
                    buttonSave.IsEnabled = true;
                else
                    buttonSave.IsEnabled = false;
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            int i;
            if (radioButtonAuswahl.IsChecked == true)
                i = 0;
            else
                i = 1;

            tmpFrage = new Frage(textBoxFragetext.Text, i);

            DialogResult = true;
            this.Close();
        }
    }
}
