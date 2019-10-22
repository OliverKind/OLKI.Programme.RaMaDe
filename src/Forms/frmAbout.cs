/*
 * RaMaDe - RawMatchAndDelete
 * 
 * Copyright:   Oliver Kind - 2019
 * License:     LGPL
 * 
 * Desctiption:
 * Form to show informations about the application
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
using System.Reflection;
using System.Windows.Forms;

namespace OLKI.Programme.RaMaDe.src.Forms
{
    /// <summary>
    /// A form to show informations about the application
    /// </summary>
    public partial class About : Form
    {
        #region Constants
        /// <summary>
        /// Specifies the name of the application version
        /// </summary>
        private const string VERSION_NAME = "Lens";
        #endregion

        #region Properties
        #region Assemblyattributaccessoren
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
        #endregion

        #region Methods
        internal About()
        {
            InitializeComponent();
            this.Text = String.Format(this.Text, this.AssemblyTitle);
            this.lblProductName.Text = string.Format(this.lblProductName.Text, this.AssemblyProduct);
            this.lblVersion.Text = String.Format(this.lblVersion.Text, new object[] { this.AssemblyVersion, VERSION_NAME });
            this.lblCopyright.Text = string.Format(this.lblCopyright.Text, this.AssemblyCopyright);
            this.lblCompanyName.Text = string.Format(this.lblCompanyName.Text, this.AssemblyCompany);
            this.textBoxDescription.Text = this.AssemblyDescription;
        }

        private void btnGoToLicenses_Click(object sender, EventArgs e)
        {
            License LicenseForm = new License();
            LicenseForm.ShowDialog(this);
            LicenseForm.Dispose();
        }
        #endregion    
     }
}