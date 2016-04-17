using LeposWPF.Helpers;
using LeposWPF.Helpers.Clases;
using LeposWPF.Model;
using MahApps.Metro.Controls;
using System;
using System.Windows;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : MetroWindow
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public LogIn()
        {
            InitializeComponent();
            logoImage.Source = WindowHelper.getLogo(CompanyHelper.getLogoPath());
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Handle log in event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            if (userTextBox.Text == String.Empty || passwordTextBox.Password == String.Empty)
            {
                errorTextBlock.Text = "Campos incompletos";
            }
            else if (UserHelper.isUser(userTextBox.Text, passwordTextBox.Password))
            {
                if (CompanyHelper.companyExists())
                {
                    if (CompanyHelper.currentCompany.IsActivated)
                    {
                        Hide();
                        new HomeRegister(this).ShowDialog();
                    }
                    else
                    {
                        errorTextBlock.Text = "Error : el producto no esta activado.";
                    }
                }
                else
                {
                    errorTextBlock.Text = "Error : la base de datos esta dañada.";
                }
            }
            else
            {
                errorTextBlock.Text = "Datos incorrectos";
            }
        }
        /// <summary>
        /// Listen to keyboard
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void MetroWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F12)
            {
                Window window = new ActivateProduct();
                Hide();
                window.ShowDialog();
                ShowDialog();
            }
        }
        #endregion
    }
}
