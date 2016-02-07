using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for ActivateProduct.xaml
    /// </summary>
    public partial class ActivateProduct : MetroWindow
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public ActivateProduct()
        {
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Activate product
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void activateButton_Click(object sender, RoutedEventArgs e)
        {
            if (false)
            {
                errorTextBlock.Visibility = Visibility.Visible;
                errorTextBlock.Text = "Campos Incompletos";
            }
            else
            {
                Hide();
                new HomeRegister(this).ShowDialog();
            }
        }
        #endregion
    }
}
