using LeposWPF.Helpers;
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
    /// Interaction logic for CompanyInfo.xaml
    /// </summary>
    public partial class CompanyInfo : UserControl
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public CompanyInfo()
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
            displayCurrentSavedValues();
        }

        /// <summary>
        /// Loaded event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            messageTextBlock.Width = logoImage.ActualWidth;
            displayCurrentSavedValues();
            logoImage.Source = WindowHelper.getLogo(CompanyHelper.getLogoPath());
        }
        /// <summary>
        /// Saving event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowHelper.validateEmptyTextBoxes(addressTextBox,descriptionTextBox,nameTextBox,rfcTextBox,zipTextBox))
            {
                CompanyHelper.currentCompany.Address = addressTextBox.Text;
                CompanyHelper.currentCompany.Description = descriptionTextBox.Text;
                CompanyHelper.currentCompany.Name = nameTextBox.Text;
                CompanyHelper.currentCompany.RFC = rfcTextBox.Text;
                CompanyHelper.currentCompany.ZIP = zipTextBox.Text;
                CompanyHelper.currentCompany.IVAType = ivaComboBox.SelectedIndex;
                CompanyHelper.updateCompany();
                messageTextBlock.Foreground = Brushes.LightBlue;
                messageTextBlock.Text = "Información Guardada";
            }
            else
            {
                messageTextBlock.Foreground = Brushes.Red;
                messageTextBlock.Text = "Hay campos vacíos";
            }
        }
        #endregion
        #region Helpers

        /// <summary>
        /// Display the values of company stored in the database
        /// </summary>
        private void displayCurrentSavedValues()
        {
            addressTextBox.Text = CompanyHelper.currentCompany.Address;
            descriptionTextBox.Text = CompanyHelper.currentCompany.Description;
            nameTextBox.Text = CompanyHelper.currentCompany.Name;
            rfcTextBox.Text = CompanyHelper.currentCompany.RFC;
            zipTextBox.Text = CompanyHelper.currentCompany.ZIP;
            ivaComboBox.SelectedIndex = CompanyHelper.currentCompany.IVAType;
        }
        #endregion
    }
}
