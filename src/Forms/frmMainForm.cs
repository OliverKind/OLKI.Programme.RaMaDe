/*
 * RaMaDe - RawMatchAndDelete
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Application main Form
 * 
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the LGPL General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * LGPL General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not check the GitHub-Repository.
 * 
 * */

using OLKI.Programme.RaMaDe.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OLKI.Programme.RaMaDe
{
    /// <summary>
    /// Application main form
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Default text in Textfield for comapre file extensions
        /// </summary>
        private const string DEFAULT_EXTENSION_COMPARE = "jpg,jpeg";
        /// <summary>
        /// Default text in Textfield for raw-File identification
        /// </summary>
        private const string DEFAULT_EXTENSION_RAW = "3fr,arw,cr2,cr3,crw,cs1,cs16,cs4,dcr,dcs,dng,dng,dng,dng,dng,erf,fff,iiq,iiq,kdc,mdc,mef,mfw,mrw,nef,nrw,orf,ori,pef,raf,raw,raw,raw,rw2,rwl,sr2,srf,srw,tif,x3f";
        /// <summary>
        /// Height offset for the exception log area, to create the right height weilhe show and hide this area and resice the form
        /// </summary>
        private const int EXCEPTION_AREA_HEIGHT_OFFSET = 6;
        /// <summary>
        /// Splitter for extensions
        /// </summary>
        private const char STRING_SPLIT = ',';

        #region Methods
        public MainForm()
        {
            InitializeComponent();
            this.txtFileExtensionCorresponding.Text = DEFAULT_EXTENSION_COMPARE;
            this.txtFileExtensionRaw.Text = DEFAULT_EXTENSION_RAW;

            this.DeleteExceptionAreaHide();
        }

        void MainForm_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //MessageBox.Show("Autor: Oliver Kind");
            new AboutForm().ShowDialog(this);
            //frmAboutForm AboutForm = new frmAboutForm();
            e.Cancel = true;
        }

        void btnDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowser = new FolderBrowserDialog();
            FolderBrowser.Description = MainForm_Mres.btnDirectoryClick__FolderBrowser_Description;
            FolderBrowser.SelectedPath = this.txtDirectroy.Text;
            FolderBrowser.ShowNewFolderButton = false;

            if (FolderBrowser.ShowDialog(this) == DialogResult.OK)
            {
                this.txtDirectroy.Text = FolderBrowser.SelectedPath;
            }
        }

        void btnDeleteRawFile_Click(object sender, EventArgs e)
        {
            List<FileInfo> FilesToDelete = new List<FileInfo>();
            int FilesDeleted = 0;

            if (this.grbDeleteException.Visible == true) this.DeleteExceptionAreaHide();

            // Stop if directroy path is invalid or file did nox exists
            if (string.IsNullOrEmpty(this.txtDirectroy.Text) || !new DirectoryInfo(this.txtDirectroy.Text).Exists)
            {
                MessageBox.Show(MainForm_Mres.btnDeleteRawFileClick__NoDirectroy_Message, MainForm_Mres.btnDeleteRawFileClick__NoDirectroy_Caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FilesToDelete = this.SearchForRawFilesToDelete(this.txtDirectroy.Text, this.txtFileExtensionCorresponding.Text, this.txtFileExtensionRaw.Text);

            // Stop if no files found
            if (FilesToDelete.Count == 0)
            {
                if (MessageBox.Show(MainForm_Mres.btnDeleteRawFileClick__NoFiles_Message, MainForm_Mres.btnDeleteRawFileClick__NoFiles_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Application.Exit();
                }
                return;
            }

            // Delete files withoud corresponding file
            if (MessageBox.Show(string.Format(MainForm_Mres.btnDeleteRawFileClick__DeleteFiles_Message, new object[] { FilesToDelete.Count.ToString() }), string.Format(MainForm_Mres.btnDeleteRawFileClick__DeleteFiles_Caption, new object[] { FilesToDelete.Count }), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                FilesDeleted = this.DeletedFilesInList(FilesToDelete);
                if (MessageBox.Show(string.Format(MainForm_Mres.btnDeleteRawFileClick__Finish_Message, new object[] { FilesDeleted, this.lsvDeleteException.Items.Count }), string.Format(MainForm_Mres.btnDeleteRawFileClick__Finish_Caption, new object[] { FilesToDelete.Count }), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// Search for RAW-Files the dont have an corresponding file and add them to telete list
        /// </summary>
        /// <param name="directroy">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="fileExtensionCorresponding">The extension of files to compare with</param>
        /// <param name="fileExtensionRaw">The extension of raw files</param>
        private List<FileInfo> SearchForRawFilesToDelete(string directroy, string fileExtensionCorresponding, string fileExtensionRaw)
        {
            List<FileInfo> FilesToDelete = new List<FileInfo>();

            try
            {
                // Iterate to all files
                FileInfo TempFileAll;   // FileInfo for all files
                foreach (string FileItem in Directory.GetFiles(directroy))
                {
                    TempFileAll = new FileInfo(FileItem);

                    // If file is an RAW-File, search for cooresponding file
                    if (this.IsRawFile(TempFileAll, fileExtensionRaw))
                    {
                        // If ther is no corresponding file, add RAW-File to delete list
                        if (!this.HasCorespondingFile(FileItem, fileExtensionCorresponding))
                        {
                            FilesToDelete.Add(TempFileAll);
                        }
                    }
                }
                return FilesToDelete;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(MainForm_Mres.btnDirectoryClick__SearchException_Message, new object[] { ex.Message }), MainForm_Mres.btnDirectoryClick__SearchException_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return FilesToDelete;
            }
        }

        /// <summary>
        /// Check if the specified file is an raw file
        /// </summary>
        /// <param name="tempFileAll">File to chek if it is an raw file</param>
        /// <param name="fileExtensionRaw">List of raw file extensions, sperated with comma</param>
        /// <returns></returns>
        private bool IsRawFile(FileInfo tempFileAll, string fileExtensionRaw)
        {
            foreach (string ExtensionItem in fileExtensionRaw.Split(STRING_SPLIT))
            {
                if (tempFileAll.Exists && tempFileAll.Extension == "." + ExtensionItem)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if an raw file has an coorespinding picture file
        /// </summary>
        /// <param name="checkFilePath">FullName path of the raw file to check if it has a corresponding picture file</param>
        /// <param name="fileExtensionCorresponding">List of compare file extensions, sperated with comma</param>
        /// <returns></returns>
        private bool HasCorespondingFile(string checkFilePath, string fileExtensionCorresponding)
        {
            //return new FileInfo(System.IO.Path.GetDirectoryName(checkFilePath) + @"\" + System.IO.Path.GetFileNameWithoutExtension(checkFilePath) + "." + fileExtensionCorresponding).Exists;
            foreach (string ExtensionItem in fileExtensionCorresponding.Split(STRING_SPLIT))
            {
                if (new FileInfo(System.IO.Path.GetDirectoryName(checkFilePath) + @"\" + System.IO.Path.GetFileNameWithoutExtension(checkFilePath) + "." + ExtensionItem).Exists)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Deletes the files, specified in a list
        /// </summary>
        /// <param name="fllesToDelete">List with files to delete</param>
        /// <returns>The number of deleted files</returns>
        private int DeletedFilesInList(List<FileInfo> fllesToDelete)
        {
            int FilesDeleted = 0;
            try
            {
                foreach (FileInfo FileItem in fllesToDelete)
                {
                    if (this.DelteFile(FileItem)) FilesDeleted++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(MainForm_Mres.btnDirectoryClick__SearchException_Caption, new object[] { ex.Message }), MainForm_Mres.btnDirectoryClick__SearchException_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return FilesDeleted;
        }

        /// <summary>
        /// Delete the specifies file. Add an new entry to exception lsitview if an exception was thrown
        /// </summary>
        /// <param name="path">File to delte</param>
        /// <returns>True if delete of the file was sucessfull</returns>
        private bool DelteFile(FileInfo file)
        {
            try
            {
                File.Delete(file.FullName);
                return true;
            }
            catch (Exception ex)
            {
                this.DeleteExceptionAreaShow();
                ListViewItem ExceptionItem = new ListViewItem();
                ExceptionItem.Tag = new object[] { file, ex.Message }; // Not used at this time
                ExceptionItem.Text = file.Name;
                ExceptionItem.SubItems.Add(ex.Message);

                this.lsvDeleteException.Items.Add(ExceptionItem);
                return false;
            }
        }

        /// <summary>
        /// Hides the the area, if it is visible, shows exception durting deleting files and clear listview
        /// </summary>
        void DeleteExceptionAreaHide()
        {
            this.grbDeleteException.Visible = false;
            this.lsvDeleteException.Items.Clear();
            this.Height = this.Height - (this.grbDeleteException.Height + EXCEPTION_AREA_HEIGHT_OFFSET);
        }

        /// <summary>
        /// Shows the the area, if it is not visible, shows exception durting deleting files and clear listview
        /// </summary>
        void DeleteExceptionAreaShow()
        {
            this.lsvDeleteException.Items.Clear();
            this.grbDeleteException.Visible = true;
            this.Height = this.Height + (this.grbDeleteException.Height + EXCEPTION_AREA_HEIGHT_OFFSET);
        }
        #endregion
    }
}