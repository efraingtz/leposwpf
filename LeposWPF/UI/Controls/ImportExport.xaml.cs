using LeposWPF.Helpers;
using LeposWPF.Helpers.Clases;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ImportExport.xaml
    /// </summary>
    public partial class ImportExport : UserControl
    {
        #region Constructors
        /// <summary>
        /// Initialize current instance
        /// </summary>
        public ImportExport()
        {
            InitializeComponent();
        }
        #endregion
        #region UI Events
        /// <summary>
        /// Importing event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event object</param>
        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Al proceder con la importación, toda la información actual será eliminada. ¿Esta seguro de continuar?", "Confirmar importación", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Title = "Importar archivo LEPOS";
                openFileDialog.Filter = "Lepos |*.lps";
                openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    String copyPath = Directory.GetCurrentDirectory() + "/DB/";
                    if (Directory.Exists(copyPath))
                    {
                        String copyFile = copyPath + "lepos_DB";
                        if (CompanyHelper.validateImportDataBase(openFileDialog.FileName))
                        {
                            System.IO.File.Copy(openFileDialog.FileName, copyFile, true);
                            System.Windows.Forms.MessageBox.Show("La información ha sido importada, el sistema se reiniciará", "Importación Completa", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            System.Windows.Forms.Application.Restart();
                            System.Windows.Application.Current.Shutdown();
                        }
                        else
                        {
                            messageTextBlock.Foreground = Brushes.Red;
                            messageTextBlock.Text = "Error: el archivo a importar es inválido";
                        }
                    }
                    else
                    {
                        messageTextBlock.Foreground = Brushes.Red;
                        messageTextBlock.Text = "Error: no existe el directorio: " + copyPath;
                    }
                }
            }
        }
        /// <summary>
        /// Exporting event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event object</param>
        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            messageTextBlock.Text = String.Empty;
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Title = "Exportar toda la información";
            saveFileDialog.Filter = "Lepos |*.lps";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String pathDB = Directory.GetCurrentDirectory() + "/DB/lepos_DB";
                if (File.Exists(pathDB))
                {
                    System.IO.File.Copy(pathDB, saveFileDialog.FileName, true);
                    messageTextBlock.Foreground = Brushes.Green;
                    messageTextBlock.Text = "La información ha sido guardada en : "+ saveFileDialog.FileName;
                }
                else
                {
                    messageTextBlock.Foreground = Brushes.Red;
                    messageTextBlock.Text = "Error: la base de datos no fué encontrada";
                }
            }
        }
        /// <summary>
        /// Loaded event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event of sender object</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            logoImage.Source = WindowHelper.getLogo(CompanyHelper.getLogoPath());
        }
        #endregion
    }
}
