using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace EinrichtungsStatistik
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void saveData(AppData appData)
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

        private AppData loadData()
        {
            AppData appData;

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
            return appData;
        }

        private void buttonBeenden_Click(object sender, RoutedEventArgs e)
        {
            /*if (MessageBox.Show("Möchten Sie das Programm beenden?", "Programm beenden", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                return;
            else*/
                Application.Current.Shutdown();
        }

        private void buttonFragebogenentwurf_Click(object sender, RoutedEventArgs e)
        {
            Fragebogenauswahl dlgAuswahl = new Fragebogenauswahl();

            dlgAuswahl.setFrageboegen(loadData().appFrageboegen);
            dlgAuswahl.ShowDialog();
            /*Fragebogenentwurf dlgEntwurf = new Fragebogenentwurf();
            dlgEntwurf.ShowDialog();*/
        }
    }
}
