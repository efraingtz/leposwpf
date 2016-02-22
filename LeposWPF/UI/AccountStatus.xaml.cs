﻿using LeposWPF.Model;
using MahApps.Metro.Controls;
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

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for AccountStatus.xaml
    /// </summary>
    public partial class AccountStatus : MetroWindow
    {
        #region Declaration 
        /// <summary>
        /// Client to whom status is going to be displayed
        /// </summary>
        public Client Client { get; set; }
        #endregion
        #region Constructors
        /// <summary>
        /// Inititalize new instance of current window
        /// </summary>
        public AccountStatus(Window window, Client client)
        {
            this.Client = client;
            this.Owner = Owner;
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Filter by ID
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saleIDButton_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Loaded window event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var source = new List<Test>();
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            source.Add(new Test { name = "Name", name2 = "A", name3 = "ADG" });
            debtsDataGrid.ItemsSource = source;
            debtsDataGrid.Width = ActualWidth;
            debtsDataGrid.Height = dataGridContainerViewBox.ActualHeight;
        }
        /// <summary>
        /// Filter results between the selected dates
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void filterButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
