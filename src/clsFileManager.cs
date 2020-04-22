/*
 * RaMaDe - RawMatchAndDelete
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Class to manage/delete RAW-Files they don't have an corresponding low(er) quality picture file
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

using OLKI.Programme.RaMaDe.src.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OLKI.Programme.RaMaDe
{
    /// <summary>
    /// Class to manage/delete RAW-Files they don't have an corresponding low(er) quality picture file
    /// </summary>
    public class FileManager
    {
        #region Constants
        /// <summary>
        /// Splitter for extensions
        /// </summary>
        private const char STRING_SPLIT = ',';
        #endregion

        #region Events
        public class ExceptionEventArgs
        {
            /// <summary>
            /// Path to the file, that can't be deleted
            /// </summary>
            public FileInfo File { get; }
            /// <summary>
            /// Exception whiel deleting the file
            /// </summary>
            public Exception Exception { get; }
            /// <summary>
            /// Creates an new exception while deleting a file object
            /// </summary>
            /// <param name="file">FileInfo of the file, that can't be deleted</param>
            /// <param name="exception">Exception whiel deleting the file</param>
            public ExceptionEventArgs(FileInfo file, Exception exception)
            {
                File = file;
                Exception = exception;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DeleteExceptionEventHandler(object sender, ExceptionEventArgs e);

        /// <summary>
        /// Will thrown if there is an exception while deleting a file
        /// </summary>
        public event DeleteExceptionEventHandler DeleteException;
        #endregion

        #region Members
        /// <summary>
        /// Number of deleted files
        /// </summary>
        private int _deletedFiles = 0;
        /// <summary>
        /// Get the number of deleted files
        /// </summary>
        public int DeltedFiles
        {
            get
            {
                return this._deletedFiles;
            }
        }

        /// <summary>
        /// List with file extens, they specifiy pcicutres to compare with raw files
        /// </summary>
        private List<string> _extensionsCompare = new List<string>();
        /// <summary>
        /// Set the list with file extens, they specifiy pcicutres to compare with raw files, by a string, seperated with an char
        /// </summary>
        public string ExtensionsCompare
        {
            set
            {
                this._extensionsCompare = value.ToLower().Split(STRING_SPLIT).ToList();
            }
        }

        /// <summary>
        /// List with file extens, they specify RAW-Files
        /// </summary>
        private List<string> _extensionsRaw = new List<string>();
        /// <summary>
        /// Set the list with file extens, the specify RAW-Files, by a string, seperated with an char
        /// </summary>
        public string ExtensionsRaw
        {
            set
            {
                this._extensionsRaw = value.ToLower().Split(STRING_SPLIT).ToList();
            }
        }

        /// <summary>
        /// List with files, to compare with RAW-Files
        /// </summary>
        private readonly List<string> _filesCompare = new List<string>();

        /// <summary>
        /// List with all RAW-Files
        /// </summary>
        private readonly List<string> _filesRaw = new List<string>();
        /// <summary>
        /// List with RAW-Files to delte
        /// </summary>
        private List<string> _filesRawToDelete = new List<string>();
        /// <summary>
        /// Get the list with RAW-Files to delte
        /// </summary>
        public List<string> FilesRawToDelete
        {
            get
            {
                return this._filesRawToDelete;
            }
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Search for RAW-Files the dont have an corresponding low(er) quality pictre file and add them to delete list
        /// </summary>
        /// <param name="directroy">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        public void SearchForRawFilesToDelete(string directroy)
        {
            List<FileInfo> FilesToDelete = new List<FileInfo>();

            try
            {
                // Iterate to all files and chek if it ist an RAW-File or an file to compare with
                foreach (FileInfo FileItem in new DirectoryInfo(directroy).GetFiles().OrderBy(f => f.Name))
                {
                    // Create file list of compare and RAW-Files.
                    // Witoud exception an in lower case chars to make it esasyer to compare
                    if (this.IsCompareFile(FileItem))
                    {
                        this._filesCompare.Add(FileItem.FullName.ToLower());
                    }
                    else if (this.IsRawFile(FileItem))
                    {
                        this._filesRaw.Add(FileItem.FullName.ToLower());
                    }
                }
                // Remove every file from RAW-file list, that has an coresponding file
                this._filesRawToDelete = this.GetNonComparedFiles(this._filesRaw, this._filesCompare);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(frmMain_Stringtable._0x0006m, new object[] { ex.Message }), frmMain_Stringtable._0x0006c, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Check if the specified file is an low(er) quality picture file to compare wit RAW-Files
        /// </summary>
        /// <param name="fileToCheck">File to chek if it is an file to compare wit RAW-Files</param>
        /// <returns>True if the file is file to compare a RAW-file with</returns>
        private bool IsCompareFile(FileInfo fileToCheck)
        {
            return fileToCheck.Exists && this._extensionsCompare.Contains(fileToCheck.Extension.ToLower().Substring(1));
        }

        /// <summary>
        /// Check if the specified file is an RAW-file
        /// </summary>
        /// <param name="fileToCheck">File to chek if it is an RAW-file</param>
        /// <returns>True if the file is an RAW-file</returns>
        private bool IsRawFile(FileInfo fileToCheck)
        {
            return fileToCheck.Exists && this._extensionsRaw.Contains(fileToCheck.Extension.ToLower().Substring(1));
        }

        /// <summary>
        /// Get a list of raw files they don't have a corresponding low(er) quality picture file
        /// </summary>
        /// <param name="filesRaw">Raw-File check if there is an corresponding low(er) quality picture file</param>
        /// <param name="filesCompare">List with low(er) quality files to compare with Raw-file</param>
        /// <returns></returns>
        private List<string> GetNonComparedFiles(List<string> filesRaw, List<string> filesCompare)
        {
            List<string> FileNotCompare = new List<string>();
            foreach (string RawFile in filesRaw)
            {
                if (!this.HasCompareFile(RawFile, filesCompare))
                {
                    FileNotCompare.Add(RawFile);
                }
            }
            return FileNotCompare;
        }

        /// <summary>
        /// Check if an given Raw-file has an corresponding low(er) quality picture file
        /// </summary>
        /// <param name="fileRaw"></param>
        /// <param name="filesCompare"></param>
        /// <returns></returns>
        private bool HasCompareFile(string fileRaw, List<string> filesCompare)
        {
            foreach (string FileCompare in filesCompare)
            {
                if (System.IO.Path.GetFileNameWithoutExtension(FileCompare).ToLower() == System.IO.Path.GetFileNameWithoutExtension(fileRaw).ToLower()) return true;
            }
            return false;
        }

        /// <summary>
        /// Delete RAW-Files, the dont have a corresponding low(er) quality picture file
        /// </summary>
        public void DelteNonCompareFiles()
        {
            this._deletedFiles = 0;
            try
            {
                foreach (string FileItem in this._filesRawToDelete)
                {
                    if (this.DelteFile(new FileInfo(FileItem))) this._deletedFiles++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(frmMain_Stringtable._0x0006c, new object[] { ex.Message }), frmMain_Stringtable._0x0006c, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Delete the specified file
        /// </summary>
        /// <param name="file">File to delte</param>
        /// <returns>True if delete was sucessfull</returns>
        private bool DelteFile(FileInfo file)
        {
            try
            {
                File.Delete(file.FullName);
                return true;
            }
            catch (Exception ex)
            {
                if (this.DeleteException != null)
                {
                    DeleteException(this, new ExceptionEventArgs(file, ex));
                }
                return false;
            }
        }
        #endregion
    }
}