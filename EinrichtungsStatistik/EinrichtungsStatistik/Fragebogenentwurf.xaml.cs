using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
        private AppData appData;

        public Fragebogenentwurf()
        {
            InitializeComponent();
            IFormatter formatter = new BinaryFormatter();
            try
            {
                Stream stream = new FileStream("udata.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
                appData = (AppData)formatter.Deserialize(stream);
                stream.Close();
                refreshLists();
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message, "Dateifehler", MessageBoxButton.OK);
                appData = new AppData();
                //Application.Current.Shutdown();
                //throw;
            }
        }

        private void refreshLists()
        {
            listViewFragen.Items.Clear();

            foreach (Frage item in appData.getFragen())
            {
                listViewFragen.Items.Add(item.getFragetext());
            }
        }

        private void buttonSchliessen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonNeueFrage_Click(object sender, RoutedEventArgs e)
        {
            NeueFrage dlgNeueFrage = new NeueFrage();
            dlgNeueFrage.ShowDialog();

            appData.addFrage(dlgNeueFrage.getFrage());

            /* Überprüfung der Eingabe
            MessageBox.Show(dlgNeueFrage.getFrage().getFragetext(), "Eingegebene Frage", MessageBoxButton.OK);*/

            FileStream fs = new FileStream("udata.dat", FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, appData);
            }
            catch (SerializationException ec)
            {
                MessageBox.Show(ec.Message, "Speicherfehler", MessageBoxButton.OK);
                //Console.WriteLine("Failed to serialize. Reason: " + ec.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
            refreshLists();
        }
    }
}
