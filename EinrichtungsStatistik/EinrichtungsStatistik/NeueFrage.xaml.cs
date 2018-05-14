﻿using System;
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

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
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
            this.Close();
        }
    }
}