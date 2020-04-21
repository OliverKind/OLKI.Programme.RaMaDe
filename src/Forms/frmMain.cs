/*
 * RaMaDe - RawMatchAndDelete
 * 
 * Copyright:   Oliver Kind - 2020
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

using System;
using System.IO;
using System.Windows.Forms;

namespace OLKI.Programme.RaMaDe.src.Forms
{
    /// <summary>
    /// Application main form
    /// </summary>
    public partial class Main : Form
    {
        #region Constants
        /// <summary>
        /// Default text in Textfield for comapre file extensions
        /// </summary>
        private const string DEFAULT_EXTENSION_COMPARE = "jpg,jpeg";
        /// <summary>
        /// Default text in Textfield for raw-File identification
        /// </summary>
        private const string DEFAULT_EXTENSION_RAW = "3fr,arw,cr2,cr3,crw,cs1,cs16,cs4,dcr,dcs,dng,erf,fff,iiq,kdc,mdc,mef,mfw,mrw,nef,nrw,orf,ori,pef,raf,raw,rw2,rwl,sr2,srf,srw,tif,x3f";
        /// <summary>
        /// Height offset for the exception log area, to create the right height weilhe show and hide this area and resice the form
        /// </summary>
        private const int EXCEPTION_AREA_HEIGHT_OFFSET = 6;
        #endregion

        #region Methods
        public Main()
        {
            InitializeComponent();
            this.txtFileExtensionCorresponding.Text = DEFAULT_EXTENSION_COMPARE;
            this.txtFileExtensionRaw.Text = DEFAULT_EXTENSION_RAW;
            this.DeleteExceptionAreaHide();
        }

        private void FileManger_DelteException(object sender, FileManager.ExceptionEventArgs e)
        {
            if (!this.grbDeleteException.Visible) this.DeleteExceptionAreaShow();

            ListViewItem ExceptionItem = new ListViewItem
            {
                Tag = e, // Not used at this time
                Text = e.File.Name
            };
            ExceptionItem.SubItems.Add(e.Exception.Message);
            this.lsvDeleteException.Items.Add(ExceptionItem);
        }

        /// <summary>
        /// Hides the the area, if it is visible, shows exception durting deleting files and clear listview
        /// </summary>
        private void DeleteExceptionAreaHide()
        {
            this.grbDeleteException.Visible = false;
            this.lsvDeleteException.Items.Clear();
            this.Height -= (this.grbDeleteException.Height + EXCEPTION_AREA_HEIGHT_OFFSET);
        }

        /// <summary>
        /// Shows the the area, if it is not visible, shows exception durting deleting files and clear listview
        /// </summary>
        private void DeleteExceptionAreaShow()
        {
            this.lsvDeleteException.Items.Clear();
            this.grbDeleteException.Visible = true;
            this.Height += (this.grbDeleteException.Height + EXCEPTION_AREA_HEIGHT_OFFSET);
        }

        #region Form Events

        private void MainForm_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            About AboutForm = new About();
            AboutForm.ShowDialog(this);
            AboutForm.Dispose();
            e.Cancel = true;
        }

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FolderBrowser = new FolderBrowserDialog
            {
                Description = frmMain_Stringtable._0x0005,
                SelectedPath = this.txtDirectroy.Text,
                ShowNewFolderButton = false
            };

            if (FolderBrowser.ShowDialog(this) == DialogResult.OK)
            {
                this.txtDirectroy.Text = FolderBrowser.SelectedPath;
            }
            FolderBrowser.Dispose();
        }

        private void btnDeleteRawFile_Click(object sender, EventArgs e)
        {
            src.FileManager FileManger = new FileManager
            {
                ExtensionsCompare = this.txtFileExtensionCorresponding.Text.ToLower(),
                ExtensionsRaw = this.txtFileExtensionRaw.Text.ToLower(),
            };
            FileManger.DeleteException += new FileManager.DeleteExceptionEventHandler(this.FileManger_DelteException);

            //Hide exception area if it is visible. Will be shown again if an exception occurs
            if (this.grbDeleteException.Visible == true) this.DeleteExceptionAreaHide();

            // Stop if directroy path is invalid or file did nox exists
            if (string.IsNullOrEmpty(this.txtDirectroy.Text) || !new DirectoryInfo(this.txtDirectroy.Text).Exists)
            {
                MessageBox.Show(frmMain_Stringtable._0x0003m, frmMain_Stringtable._0x0003c, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FileManger.SearchForRawFilesToDelete(this.txtDirectroy.Text);

            // Stop if no files found
            if (FileManger.FilesRawToDelete.Count == 0)
            {
                if (MessageBox.Show(frmMain_Stringtable._0x0004m, frmMain_Stringtable._0x0004c, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Application.Exit();
                }
                return;
            }

            // Delete files withoud corresponding file
            if (MessageBox.Show(string.Format(frmMain_Stringtable._0x0001m, new object[] { FileManger.FilesRawToDelete.Count.ToString() }), string.Format(frmMain_Stringtable._0x0001c, new object[] { FileManger.FilesRawToDelete.Count }), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                FileManger.DelteNonCompareFiles();
                if (MessageBox.Show(string.Format(frmMain_Stringtable._0c0002m, new object[] { FileManger.DeltedFiles, this.lsvDeleteException.Items.Count }), string.Format(frmMain_Stringtable._0x0002c, new object[] { FileManger.DeltedFiles }), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }
        #endregion
        #endregion
    }
}