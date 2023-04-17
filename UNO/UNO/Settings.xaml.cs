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

namespace UNO
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            sdrMaster.Value = Sounds.GetMaster();
            sdrSFX.Value = Sounds.GetSFX();
            sdrMusic.Value = Sounds.GetMusic();
            UpdateValues();
            initialized = true;
        }

        bool initialized = false;

        /// <summary>
        /// applies the current volume settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            Sounds.SetMaster((int)sdrMaster.Value);
            Sounds.SetSFX((int)sdrSFX.Value);
            Sounds.SetMusic((int)sdrMusic.Value);
            this.Close();
        }

        /// <summary>
        /// displays the slider value in the labels
        /// </summary>
        private void UpdateValues()
        {
            lblMaster.Content =  ((int)sdrMaster.Value).ToString();
            lblSFX.Content = ((int)sdrSFX.Value).ToString();
            lblMusic.Content = ((int)sdrMusic.Value).ToString();
        }

        private void MasterChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized) {UpdateValues();};
        }

        private void SFXChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized) { UpdateValues(); };
        }

        private void MusicChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initialized) { UpdateValues(); };
        }
    }
}