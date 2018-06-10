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
            loadData();
            textBoxFragebogenName.Text = "Fragebogen " + (appData.getFrageboegenCount() + 1);
            refreshLists();
        }

        private void saveData()
        {
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
        }

        private void loadData()
        {
            IFormatter formatter = new BinaryFormatter();
            try
            {
                Stream stream = new FileStream("udata.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
                appData = (AppData)formatter.Deserialize(stream);
                stream.Close();
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

            foreach (Fragebogen item in appData.getFrageboegen())
            {
                if (String.Compare(item.getName(), textBoxFragebogenName.Text, true) == 0)
                {
                    foreach (Frage item2 in item.getFragen())
                        listViewEnthalteneFragen.Items.Add(item2.getFragetext());
                }
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

            // Only if Result OK
            if (dlgNeueFrage.DialogResult.HasValue && dlgNeueFrage.DialogResult.Value)
            {
                foreach (Frage item in appData.getFragen())
                {
                    if (String.Compare(item.getFragetext(), dlgNeueFrage.getFrage().getFragetext(), true) > -1 &&
                        String.Compare(item.getFragetext(), dlgNeueFrage.getFrage().getFragetext(), true) < 1)
                    {
                        if (MessageBox.Show("Die eingegebene Frage hat Ähnlichkeit mit folgender Frage:\n\n" + item.getFragetext() + "\n\n" + "Möchten Sie die Frage dennoch speichern?",
                            "Frage bereits vorhanden", MessageBoxButton.YesNo) == MessageBoxResult.No)
                            return;
                    }
                }
            }

            appData.addFrage(dlgNeueFrage.getFrage());

            /* Überprüfung der Eingabe
            MessageBox.Show(dlgNeueFrage.getFrage().getFragetext(), "Eingegebene Frage", MessageBoxButton.OK);*/

            saveData();
            refreshLists();
        }

        private void listViewFragen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewFragen.SelectedItem != null )
            {
                buttonFrageLoeschen.IsEnabled = true;
                buttonFrageBearbeiten.IsEnabled = true;
                buttonArrowLeft.IsEnabled = true;
            }
            else
            {
                buttonFrageLoeschen.IsEnabled = false;
                buttonFrageBearbeiten.IsEnabled = false;
                buttonArrowRight.IsEnabled = false;
            }
        }

        private void buttonFrageLoeschen_Click(object sender, RoutedEventArgs e)
        {
            if (listViewFragen.SelectedItem != null)
            {
                if (MessageBox.Show("Möchten Sie die folgende Frage wirklich löschen?\n\n" + listViewFragen.SelectedItem.ToString(), "Frage löschen", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
                else
                {
                    foreach (Frage item in appData.getFragen())
                    {
                        if (String.Compare(item.getFragetext(), listViewFragen.SelectedItem.ToString(), true) == 0)
                        {
                            appData.deleteFrage(item);
                            listViewFragen.Items.Remove(listViewFragen.SelectedItem.ToString());
                            saveData();
                            refreshLists();
                            break;
                        }
                    }
                }
            }
        }

        private void buttonFrageBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            NeueFrage dlgFrageBearbeiten = new NeueFrage();

            foreach (Frage item in appData.getFragen())
            {
                if (String.Compare(item.getFragetext(), listViewFragen.SelectedItem.ToString(), true) == 0)
                {
                    dlgFrageBearbeiten.setFrage(item);
                    dlgFrageBearbeiten.ShowDialog();

                    if (dlgFrageBearbeiten.DialogResult.HasValue && dlgFrageBearbeiten.DialogResult.Value)
                    {
                        if (MessageBox.Show("Möchten Sie die Frage:\n\n" + item.getFragetext() + "\n\n" + "wirklich ändern in:\n\n" +
                            dlgFrageBearbeiten.getFrage().getFragetext(), "Frage ändern", MessageBoxButton.YesNo) == MessageBoxResult.No)
                            return;
                    }
                    item.setFragetext(dlgFrageBearbeiten.getFrage().getFragetext());
                    item.setAntwortart(dlgFrageBearbeiten.getFrage().getAntwortart());
                    saveData();
                    refreshLists();
                    break;
                }
            }
        }

        private void buttonArrowLeft_Click(object sender, RoutedEventArgs e)
        {
            foreach (Frage item in appData.getFragen())
            {
                if (String.Compare(item.getFragetext(), listViewFragen.SelectedItem.ToString(), true) == 0)
                {

                }
            }
        }
    }
}
