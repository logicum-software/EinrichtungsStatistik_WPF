using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;

namespace EinrichtungsStatistik
{
    /// <summary>
    /// Interaktionslogik für Fragebogenentwurf.xaml
    /// </summary>
    public partial class Fragebogenentwurf : Window
    {
        private AppData appData;
        private Fragebogen tmpFragebogen;
        private List<Frage> tmpFragen = new List<Frage>();
        private Boolean bChanged = false;

        public Fragebogenentwurf()
        {
            InitializeComponent();
            loadData();

            int i = 1;
            Boolean bFound = false;

            if (appData.appFrageboegen.Count > 0)
            {
                while (!bFound)
                {
                    foreach (Fragebogen item in appData.appFrageboegen)
                    {
                        if (item.strName.Equals("Fragebogen " + i))
                        {
                            i++;
                            break;
                        }

                        if (item.strName.Equals(appData.appFrageboegen[appData.appFrageboegen.Count - 1].strName))
                            bFound = true;
                    }
                }
            }

            tmpFragebogen = new Fragebogen("Fragebogen " + i, new List<Frage>());

            listViewFragen.ItemsSource = tmpFragen;
            listViewEnthalteneFragen.ItemsSource = tmpFragebogen.Fragen;
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
            tmpFragen.Clear();

            foreach (Frage item in appData.appFragen)
            {
                if (!tmpFragebogen.Fragen.Contains(item))
                    tmpFragen.Add(item);
            }

            listViewFragen.Items.Refresh();
            listViewEnthalteneFragen.Items.Refresh();
            textBoxFragebogenName.Text = tmpFragebogen.strName;
        }

        private void buttonSchliessen_Click(object sender, RoutedEventArgs e)
        {
            if (bChanged == true)
            {
                if (MessageBox.Show("Der aktuelle Fragebogen enthält ungesicherte Änderungen.\nMöchten Sie ihn vorher speichern?",
                    "Fragebogen speichern", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    appData.appFrageboegen.Add(tmpFragebogen);
                    saveData();
                    bChanged = false;
                }
            }
            this.Close();
        }

        private void buttonNeueFrage_Click(object sender, RoutedEventArgs e)
        {
            NeueFrage dlgNeueFrage = new NeueFrage();
            dlgNeueFrage.ShowDialog();

            // Only if Result OK
            if (dlgNeueFrage.DialogResult.HasValue && dlgNeueFrage.DialogResult.Value == true)
            {
                foreach (Frage item in appData.appFragen)
                {
                    if (String.Compare(item.strFragetext, dlgNeueFrage.getFrage().strFragetext, true) > -1 &&
                        String.Compare(item.strFragetext, dlgNeueFrage.getFrage().strFragetext, true) < 1)
                    {
                        if (MessageBox.Show("Die eingegebene Frage hat Ähnlichkeit mit folgender Frage:\n\n" + item.strFragetext + "\n\n" + "Möchten Sie die Frage dennoch speichern?",
                            "Frage bereits vorhanden", MessageBoxButton.YesNo) == MessageBoxResult.No)
                            return;
                    }
                }
                appData.appFragen.Add(dlgNeueFrage.getFrage());
                saveData();
                refreshLists();
            }
        }

        private void listViewFragen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listViewEnthalteneFragen.SelectedItem = null;

            if (listViewFragen.SelectedItem != null )
            {
                buttonFrageLoeschen.IsEnabled = true;
                buttonFrageBearbeiten.IsEnabled = true;
                buttonArrowLeft.IsEnabled = true;
                buttonArrowRight.IsEnabled = false;
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
                if (MessageBox.Show("Möchten Sie die folgende Frage wirklich aus dem Fragenkatalog löschen?\n\n\n" + 
                    appData.appFragen.ElementAt(listViewFragen.SelectedIndex).strFragetext +
                    "\n\n(Die Frage steht dann nicht mehr zur Verfügung!)", "Frage löschen", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
                else
                {
                    appData.appFragen.RemoveAt(listViewFragen.SelectedIndex);
                    saveData();
                    refreshLists();
                }
            }
        }

        private void buttonFrageBearbeiten_Click(object sender, RoutedEventArgs e)
        {
            NeueFrage dlgFrageBearbeiten = new NeueFrage();
            Frage tmpFrage = new Frage();

            if (listViewEnthalteneFragen.SelectedItem == null)
                tmpFrage = tmpFragen.ElementAt(listViewFragen.SelectedIndex);
            else
                tmpFrage = tmpFragebogen.Fragen.ElementAt(listViewEnthalteneFragen.SelectedIndex);

            dlgFrageBearbeiten.setFrage(tmpFrage);
            dlgFrageBearbeiten.ShowDialog();

            if (dlgFrageBearbeiten.DialogResult.HasValue && dlgFrageBearbeiten.DialogResult.Value)
            {
                if (MessageBox.Show("Möchten Sie die Frage:\n\n" + tmpFrage.strFragetext +
                    "\n\n" + "wirklich ändern in:\n\n" + dlgFrageBearbeiten.getFrage().strFragetext,
                    "Frage ändern", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }

            if (listViewEnthalteneFragen.SelectedItem == null)
            {
                appData.appFragen.ElementAt(listViewFragen.SelectedIndex).strFragetext = dlgFrageBearbeiten.getFrage().strFragetext;
                appData.appFragen.ElementAt(listViewFragen.SelectedIndex).nAntwortart = dlgFrageBearbeiten.getFrage().nAntwortart;
            }
            else
            {
                appData.appFragen.ElementAt(listViewEnthalteneFragen.SelectedIndex).strFragetext = dlgFrageBearbeiten.getFrage().strFragetext;
                appData.appFragen.ElementAt(listViewEnthalteneFragen.SelectedIndex).nAntwortart = dlgFrageBearbeiten.getFrage().nAntwortart;
            }
            saveData();
            refreshLists();
        }

        private void buttonArrowLeft_Click(object sender, RoutedEventArgs e)
        {
            if (listViewFragen.SelectedItem != null)
            {
                tmpFragebogen.Fragen.Add(tmpFragen.ElementAt(listViewFragen.SelectedIndex));
                MessageBox.Show("Die Frage:\n\n" + tmpFragen.ElementAt(listViewFragen.SelectedIndex).strFragetext +
                    "\n\nwurde dem Fragebogen hinzugefügt.", "Frage hinzugefügt", MessageBoxButton.OK);
                bChanged = true;
                tmpFragen.RemoveAt(listViewFragen.SelectedIndex);
                refreshLists();
                buttonArrowLeft.IsEnabled = false;
            }
        }

        private void buttonSpeichern_Click(object sender, RoutedEventArgs e)
        {
            // <-- ToDo ÜBERARBEITEN  Was, wenn er vorher schon gespeichert wurde? -->

            if (tmpFragebogen.Fragen.Count < 1)
            {
                if (MessageBox.Show("Der Fragebogen:\n\n" + tmpFragebogen.strName + "\n\n" + "enthält keine Fragen.\n\n" +
                    "Möchten Sie ihn trotzdem speichern?", "Frage speichern", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }

            foreach (Fragebogen item in appData.appFrageboegen)
            {
                if (item.strName.Equals(tmpFragebogen.strName))
                {
                    item.strName = tmpFragebogen.strName;
                    item.Fragen = tmpFragebogen.Fragen;
                    saveData();
                    bChanged = false;
                    MessageBox.Show("Die Änderungen im Fragebogen:\n\n" + tmpFragebogen.strName + "\n\nwurden gespeichert.", 
                        "Änderungen gespeichert", MessageBoxButton.OK);
                    return;
                }
            }

            appData.appFrageboegen.Add(tmpFragebogen);
            saveData();
            bChanged = false;
            MessageBox.Show("Der Fragebogen:\n\n" + tmpFragebogen.strName + "\n\nwurde gespeichert.", "Frage gespeichert", MessageBoxButton.OK);
        }

        private void listViewEnthalteneFragen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listViewFragen.SelectedItem = null;

            if (listViewEnthalteneFragen.SelectedItem != null )
            {
                buttonArrowRight.IsEnabled = true;
                buttonFrageLoeschen.IsEnabled = true;
                buttonFrageBearbeiten.IsEnabled = true;
                buttonArrowLeft.IsEnabled = false;
            }
            else
            {
                buttonFrageLoeschen.IsEnabled = false;
                buttonFrageBearbeiten.IsEnabled = false;
                buttonArrowRight.IsEnabled = false;
            }
        }

        private void buttonArrowRight_Click(object sender, RoutedEventArgs e)
        {
            if (listViewEnthalteneFragen.SelectedItem != null)
            {
                tmpFragen.Add(tmpFragebogen.Fragen.ElementAt(listViewEnthalteneFragen.SelectedIndex));
                MessageBox.Show("Die Frage:\n\n" + tmpFragebogen.Fragen.ElementAt(listViewEnthalteneFragen.SelectedIndex).strFragetext +
                    "\n\nwurde aus dem Fragebogen entfernt.", "Frage entfernt", MessageBoxButton.OK);
                tmpFragebogen.Fragen.Remove(tmpFragebogen.Fragen.ElementAt(listViewEnthalteneFragen.SelectedIndex));
                bChanged = true;
                refreshLists();
                buttonArrowRight.IsEnabled = false;
            }
        }

        private void buttonLaden_Click(object sender, RoutedEventArgs e)
        {
            Fragebogenauswahl dlgAuswahl = new Fragebogenauswahl();

            dlgAuswahl.setFrageboegen(appData.appFrageboegen);
            dlgAuswahl.ShowDialog();

            if (dlgAuswahl.DialogResult == true)
            {
                tmpFragebogen = dlgAuswahl.getFragebogen();
                listViewEnthalteneFragen.ItemsSource = tmpFragebogen.Fragen;
                refreshLists();
            }
        }

        private void buttonLoeschen_Click(object sender, RoutedEventArgs e)
        {
            Fragebogenauswahl dlgLoeschen = new Fragebogenauswahl();

            dlgLoeschen.setFrageboegen(appData.appFrageboegen);
            dlgLoeschen.buttonLaden.Content = "Löschen";
            dlgLoeschen.ShowDialog();

            if (dlgLoeschen.DialogResult == true)
            {
                if (MessageBox.Show("Möchten Sie den folgenden Fragebogen wirklich dauerhaft löschen?\n\n\n" +
                    dlgLoeschen.getFragebogen().strName, "Frage löschen", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
                else
                {
                    Fragebogen itemToRemove = appData.appFrageboegen.SingleOrDefault(r => r.strName.Equals(dlgLoeschen.getFragebogen().strName));
                    if (itemToRemove != null)
                        appData.appFrageboegen.Remove(itemToRemove);

                    if (tmpFragebogen.strName.Equals(dlgLoeschen.getFragebogen().strName))
                    {
                        tmpFragebogen = new Fragebogen("Fragebogen " + (appData.appFrageboegen.Count + 1), new List<Frage>());
                        listViewEnthalteneFragen.ItemsSource = tmpFragebogen.Fragen;
                    }
                    saveData();
                    refreshLists();
                }
            }
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Möchten Sie den Editor wirklich zurücksetzen?\n\nAlle ungesicherten Änderungen gehen verloren.\n", "Editor zurücksetzen",
                MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            else
            {
                int i = 1;
                Boolean bFound = false;

                if (appData.appFrageboegen.Count > 0)
                {
                    while (!bFound)
                    {
                        foreach (Fragebogen item in appData.appFrageboegen)
                        {
                            if (item.strName.Equals("Fragebogen " + i))
                            {
                                i++;
                                break;
                            }

                            if (item.strName.Equals(appData.appFrageboegen[appData.appFrageboegen.Count - 1].strName))
                                bFound = true;
                        }
                    }
                }

                tmpFragebogen = new Fragebogen("Fragebogen " + i, new List<Frage>());
                listViewEnthalteneFragen.ItemsSource = tmpFragebogen.Fragen;
                bChanged = false;
                refreshLists();
            }
        }

        private void textBoxFragebogenName_TextChanged(object sender, TextChangedEventArgs e)
        {
            tmpFragebogen.strName = textBoxFragebogenName.Text;
        }

        private void textBoxFragebogenName_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxFragebogenName.Focus();
            textBoxFragebogenName.SelectAll();
        }
    }
}
