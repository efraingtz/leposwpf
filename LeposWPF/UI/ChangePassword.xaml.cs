using LeposWPF.Helpers.Clases;
using LeposWPF.Model;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LeposWPF.UI
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : MetroWindow
    {
        #region Constructors
        /// <summary>
        /// Initialize the current instance
        /// </summary>
        /// <param name="window">Owner window</param>
        public ChangePassword()
        {
            InitializeComponent();
        }
        #endregion
        #region UI Events
        
        /// <summary>
        /// Handle change password event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            errorLabel.Content = string.Empty;
            errorLabel.Foreground = Brushes.Red;
            String oldPassword = actualPasswordTextbox.Password;
            String newPassword = newPasswordTextbox.Password;
            if (oldPassword != string.Empty && newPassword != string.Empty)
            {
                if (oldPassword != UserHelper.loggedUser.Password)
                {
                    errorLabel.Content = "Las contraseña actual es incorrecta";
                }
                else
                {
                    if (oldPassword == newPassword)
                    {
                        errorLabel.Content = "Las contraseñas son iguales";
                    }
                    else
                    {
                        errorLabel.Foreground = Brushes.Green;
                        UserHelper.loggedUser.Password = newPassword;
                        UserHelper.savePassword();
                        errorLabel.Content = "Contraseña guardada";
                        actualPasswordTextbox.Password = newPasswordTextbox.Password = String.Empty;
                    }
                }
            }
            else
            {
                errorLabel.Content = "Error : hay campos vacíos";
            }
        }

        /// <summary>
        /// Window loaded
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Events of sender object</param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion
        
    }
}
