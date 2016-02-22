using LeposWPF.Helpers;
using LeposWPF.Helpers.Clases;
using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LeposWPF.UI.Controls
{
    /// <summary>
    /// Interaction logic for Logo.xaml
    /// </summary>
    public partial class Logo : UserControl
    {
        #region Declaration
        /// <summary>
        /// Name of the path to browsed logo
        /// </summary>
        private string fileName
        {
            get;
            set;
        }
        /// <summary>
        /// Name of the path to browsed logo
        /// </summary>
        private string fullFileName
        {
            get;
            set;
        }
        #endregion
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public Logo()
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
            displayCurrentLogo(actualImage, browseImage);
            fileName = string.Empty;
        }
        /// <summary>
        /// Loaded event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            displayCurrentLogo(actualImage, browseImage);
        }
        /// <summary>
        /// Saving event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    var destinyPath = System.IO.Directory.GetCurrentDirectory() + "/" + fileName;
                    System.IO.File.Copy(fullFileName, destinyPath, true);
                    CompanyHelper.currentCompany.Logo = fileName;
                    CompanyHelper.updateCompany();
                    messageTextBlock.Foreground = System.Windows.Media.Brushes.Green;
                    messageTextBlock.Text = "Logo guardado";
                    displayCurrentLogo(actualImage, browseImage);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                    messageTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                    messageTextBlock.Text = "Error: el logo no ha sido actualizado, verifique que los archivos no se encuentran en uso";
                }
            }
            else
            {
                messageTextBlock.Foreground = System.Windows.Media.Brushes.Red;
                messageTextBlock.Text = "Error: el logo no ha sido actualizado";
            }
        }
        /// <summary>
        /// Browse new logo's iage
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png)";
            //openFileDialog.AddExtension = true;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fullFileName = openFileDialog.FileName;
                fileName = System.IO.Path.GetFileName(openFileDialog.FileName);
                browseImage.Source = WindowHelper.getLogo(fullFileName);
            }
        }
        #endregion
        #region Helpers
        private void displayCurrentLogo(params System.Windows.Controls.Image[] controlsImage)
        {
            controlsImage.ToList().ForEach(a=> a.Source = WindowHelper.getLogo(CompanyHelper.getLogoPath()));
        }
        #endregion
    }
}
