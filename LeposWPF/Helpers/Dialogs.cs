using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace LeposWPF.Helpers
{
    /// <summary>
    /// Dialog Class.<br />
    /// This class contains predefined dialogs that can be reused for all application.
    /// </summary>
    class Dialogs
    {
        #region FileFilters

        /// <summary>
        /// Standard define file filters.<br />
        /// This enumeration supports file types usage for <see cref="getFiles">Open File Dialog</see>, 
        /// Save File Dialogs and Saving Procedures.
        /// </summary>
        public enum FileFilters
        {
            /// <summary>
            /// All files.<br />
            /// This file filter allows the user to select any file (*.*).
            /// </summary>
            All,

            /// <summary>
            /// Comma Separated Values.<br />
            /// This file filter allows the user to select comma separated values only (*.csv).
            /// </summary>
            CSV,

            /// <summary>
            /// Text files.<br />
            /// This file filter allows the user to select text files only (*.txt).
            /// </summary>
            Text,

            /// <summary>
            /// Excel file.<br />
            /// This file filter allows the user to select Excel files (*.xl*).
            /// </summary>
            XLAll
        }

        #endregion FileFilters

        #region ErrorMessages

        /// <summary>
        /// Error Message.<br />
        /// This is the standard definition for Error messages.
        /// </summary>
        /// <param name="message">This parameter contains the message that must be displayed when the message box be displayed.</param>
        public void msgError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void msgErrorAddingNotAllowed()
        {
            msgError("Adding rows directly in the grid is not allowed.\n"
                + "Add it using New Button and Text Boxes, Save, then come to grid to update data.");
        }

        /// <summary>
        /// Import Not Defined Error Message.<br />
        /// This message box will always display the error message "Import not defined".
        /// </summary>
        public void msgErrorImportNotDefined()
        {
            msgError("Import not defined");
        }

        /// <summary>
        /// Invalid Password Error Message.<br />
        /// This message box will always display the error message "Invalid Password".
        /// </summary>
        public void msgErrorIvalidPassword()
        {
            msgError("Invalid Password");
        }

        #endregion ErrorMessages

        #region InformationMessages

        /// <summary>
        /// Standard Information Message.<br />
        /// This is the standard definition for Information messages.
        /// </summary>
        /// <param name="message">This parameter contains the message that must be displayed when the message box be displayed.</param>
        public void msgInformation(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Data Deleted Information Message.<br />
        /// This message box will always display the information message "Data Deleted".
        /// </summary>
        public void msgInformationDataDeleted()
        {
            msgInformation("Data Deleted");
        }

        /// <summary>
        /// Data Imported Information Message.<br />
        /// This message box will always display the information message "Data Imported".
        /// </summary>
        public void msgInformationDataImported()
        {
            msgInformation("Data Imported");
        }

        /// <summary>
        /// Data Saved Information Message.<br />
        /// This message box will always display the information message "Data Saved".
        /// </summary>
        public void msgInformationDataSaved()
        {
            msgInformation("Data Saved");
        }

        /// <summary>
        /// Invalid Employee Type Information Message.<br />
        /// This message box will always display the information message "Invalid Employee Type".
        /// </summary>
        public void msgInformationInvalidEmployeeType()
        {
            msgInformation("Invalid Employee Type");
        }

        /// <summary>
        /// Not Implemented Yet Information Message.<br />
        /// This message box will always display the information message "Not implemented yet".
        /// </summary>
        public void msgInformationNotImplementedYet()
        {
            msgInformation("Not implemented yet");
        }

        /// <summary>
        /// Total Time Information Message.<br />
        /// This message box will always display the information message "Finished in HH:MM:SS.mmmm" total containing execution time.
        /// </summary>
        /// <param name="startedAt">This parameter must contain the time that execution started.</param>
        public void msgInformationTotalTime(DateTime startedAt)
        {
            msgInformation("Finished in " + DateTime.Now.Subtract(startedAt).ToString());
        }

        #endregion InformationMessages

        #region QuestionMessages

        /// <summary>
        /// Standard Question Message.<br />
        /// This message box shows counter question and wait for Yes or No option to proceed with execution.
        /// </summary>
        /// <param name="message">This parameter contains the question message to be displayed to user.</param>
        /// <returns>Boolean result where Yes button is True and No button is False.</returns>
        public bool msgQuestion(string message)
        {
            return (MessageBox.Show(message, "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);
        }

        /// <summary>
        /// Can't be undone Question Message.<br />
        /// This message box show counter confirmation request and inform that if answer is Yes, after the execution cannot be undone.
        /// </summary>
        /// <returns>Boolean result where Yes button is True and No button is False.</returns>
        public bool msgQuestionCantBeUndone(string message)
        {
            return (MessageBox.Show(message + "\n\nWARNING: This action can't be undone.", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes);
        }

        #endregion QuestionMessages

        #region WarningMessages

        public void msgWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        #endregion WarningMessages

        #region OpenDialog
        /// <summary>
        /// Standard Open File Dialog.<br />
        /// This dialog allows the user to select one or more files.
        /// </summary>
        /// <param name="filterType">This parameter define which file type user will be allowed to select.</param>
        /// <param name="multiSelect">This parameter define if user will be allowed to select one or multiple files.</param>
        /// <returns>String array containing the name of selected file(storyBoard).</returns>
        //public string[] getFiles(FileFilters filterType = FileFilters.All, bool multiSelect = false)
        public string[] getFiles(string filterType = "", bool multiSelect = false)
        {
            string[] tmpFiles;
            string filter;

            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Multiselect = multiSelect;

            switch (filterType)
            {
                //case FileFilters.XLAll: //DayForce:
                case ".XL*": //DayForce:
                    filter = "Excel Files (*.xl*)|*.xl*";
                    break;

                //case FileFilters.Text: //Kronos,PickStat:
                case ".TXT": //Kronos,PickStat:
                    filter = "Text Files (*.txt)|*.txt";
                    break;

                //case FileFilters.CSV: //Ranker:
                case ".CSV": //Ranker:
                    filter = "Comma Separated Values Files (*.csv)|*.csv";
                    break;

                default:
                    filter = "All Files (*.*)|*.*";
                    break;

            }

            openDlg.Filter = filter;
            openDlg.ShowDialog();

            if (openDlg.FileNames.Count() > 0)
            {
                tmpFiles = openDlg.FileNames;
            }
            else
            {
                tmpFiles = null;
            }

            return tmpFiles;
        }
        #endregion OpenDialog 
    }
}
