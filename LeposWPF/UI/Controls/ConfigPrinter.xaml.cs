﻿using LeposWPF.Helpers;
using LeposWPF.Helpers.Clases;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeposWPF.UI.Controls
{
    /// <summary>
    /// Interaction logic for ConfigPrinter.xaml
    /// </summary>
    public partial class ConfigPrinter : UserControl
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public ConfigPrinter()
        {
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Cancel editing data
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            resetValues();
        }
        /// <summary>
        /// Loaded event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            logoImage.Source = WindowHelper.getLogo(CompanyHelper.getLogoPath());
            printersListView.ItemsSource = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            resetValues();
            messageTextBlock.Width = logoImage.ActualWidth;
        }
        /// <summary>
        /// Saving event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (printersListView.SelectedIndex >= 0)
            {
                CompanyHelper.currentCompany.FontSize = fontIntegerUpDown.Value.Value.ToString();
                CompanyHelper.currentCompany.CharsPerLine = charsPerLineIntegerUpDown.Value.Value.ToString();
                CompanyHelper.currentCompany.Printer = printersListView.SelectedValue.ToString();
                CompanyHelper.updateCompany();
                messageTextBlock.Foreground = Brushes.Green;
                messageTextBlock.Text = "La impresora y sus datos han sido guardada";
            }
            else
            {
                messageTextBlock.Foreground = Brushes.Red;
                messageTextBlock.Text = " No ha seleccionado una impresora";
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Select saved values
        /// </summary>
        private void resetValues()
        {
            charsPerLineIntegerUpDown.Value = int.Parse(CompanyHelper.currentCompany.CharsPerLine);
            fontIntegerUpDown.Value = int.Parse(CompanyHelper.currentCompany.FontSize);
            printersListView.SelectedIndex = printersListView.Items.IndexOf(CompanyHelper.currentCompany.Printer); ;
        }
        #endregion
    }
}
