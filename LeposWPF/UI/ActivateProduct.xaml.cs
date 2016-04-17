using LeposWPF.Helpers.Clases;
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
            if (codeTextBox.Password == string.Empty)
            {
                errorTextBlock.Visibility = Visibility.Visible;
                errorTextBlock.Text = "Campos Incompletos";
            }
            else
            {
                if (codeTextBox.Password == "cartman")
                {
                    CompanyHelper.currentCompany.IsActivated = true;
                    CompanyHelper.updateCompany();
                    Close();
                }
                else
                {
                    errorTextBlock.Visibility = Visibility.Visible;
                    errorTextBlock.Text = "Código inválido";
                }
            }
        }
        #endregion
    }
}
