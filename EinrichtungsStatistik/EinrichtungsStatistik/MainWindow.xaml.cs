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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EinrichtungsStatistik
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppData appData;

        public MainWindow()
        {
            InitializeComponent();
            IFormatter formatter = new BinaryFormatter();
            try
            {
                Stream stream = new FileStream("udata.dat", FileMode.Open, FileAccess.Read, FileShare.Read);
                appData = (AppData)formatter.Deserialize(stream);
                stream.Close();
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("Datei wurde nicht gefunden.\n" + e.Message, "Dateifehler", MessageBoxButton.OK);
                //Application.Current.Shutdown();
                //throw;
            }
        }

        private void buttonBeenden_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Möchten Sie das Programm beenden?", "Programm beenden", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                return;
            else
                Application.Current.Shutdown();
        }

        private void buttonFragebogenentwurf_Click(object sender, RoutedEventArgs e)
        {
            Fragebogenentwurf dlgEntwurf = new Fragebogenentwurf();
            dlgEntwurf.ShowDialog();

        }
    }
}
