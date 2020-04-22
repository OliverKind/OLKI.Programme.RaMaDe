namespace OLKI.Programme.RaMaDe.src.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
		private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblDirectroy = new System.Windows.Forms.Label();
            this.txtDirectroy = new System.Windows.Forms.TextBox();
            this.btnDirectory = new System.Windows.Forms.Button();
            this.lblPicture = new System.Windows.Forms.Label();
            this.txtFileExtensionCorresponding = new System.Windows.Forms.TextBox();
            this.txtFileExtensionRaw = new System.Windows.Forms.TextBox();
            this.lblRawFile = new System.Windows.Forms.Label();
            this.btnDeleteRawFile = new System.Windows.Forms.Button();
            this.grbDeleteException = new System.Windows.Forms.GroupBox();
            this.lsvDeleteException = new System.Windows.Forms.ListView();
            this.clhFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhException = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblPictureAdvice = new System.Windows.Forms.Label();
            this.lblRawFileAdvice = new System.Windows.Forms.Label();
            this.btnFileExtensionCorrespondingRestore = new System.Windows.Forms.Button();
            this.btnFileExtensionRawRestore = new System.Windows.Forms.Button();
            this.grbDeleteException.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDirectroy
            // 
            this.lblDirectroy.AutoSize = true;
            this.lblDirectroy.Location = new System.Drawing.Point(12, 15);
            this.lblDirectroy.Name = "lblDirectroy";
            this.lblDirectroy.Size = new System.Drawing.Size(42, 13);
            this.lblDirectroy.TabIndex = 0;
            this.lblDirectroy.Text = "Ordner:";
            // 
            // txtDirectroy
            // 
            this.txtDirectroy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtDirectroy.Location = new System.Drawing.Point(170, 12);
            this.txtDirectroy.Name = "txtDirectroy";
            this.txtDirectroy.Size = new System.Drawing.Size(353, 20);
            this.txtDirectroy.TabIndex = 1;
            // 
            // btnDirectory
            // 
            this.btnDirectory.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectory.Image")));
            this.btnDirectory.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDirectory.Location = new System.Drawing.Point(529, 13);
            this.btnDirectory.Name = "btnDirectory";
            this.btnDirectory.Size = new System.Drawing.Size(136, 23);
            this.btnDirectory.TabIndex = 2;
            this.btnDirectory.Text = "Durchsuchen";
            this.btnDirectory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDirectory.UseVisualStyleBackColor = true;
            this.btnDirectory.Click += new System.EventHandler(this.btnDirectory_Click);
            // 
            // lblPicture
            // 
            this.lblPicture.AutoSize = true;
            this.lblPicture.Location = new System.Drawing.Point(12, 41);
            this.lblPicture.Name = "lblPicture";
            this.lblPicture.Size = new System.Drawing.Size(152, 13);
            this.lblPicture.TabIndex = 3;
            this.lblPicture.Text = "Dateiendung Vergleichsdatein:";
            // 
            // txtFileExtensionCorresponding
            // 
            this.txtFileExtensionCorresponding.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtFileExtensionCorresponding.Location = new System.Drawing.Point(170, 38);
            this.txtFileExtensionCorresponding.Name = "txtFileExtensionCorresponding";
            this.txtFileExtensionCorresponding.Size = new System.Drawing.Size(257, 20);
            this.txtFileExtensionCorresponding.TabIndex = 4;
            this.txtFileExtensionCorresponding.TextChanged += new System.EventHandler(this.txtFileExtensionCorresponding_TextChanged);
            // 
            // txtFileExtensionRaw
            // 
            this.txtFileExtensionRaw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtFileExtensionRaw.Location = new System.Drawing.Point(170, 64);
            this.txtFileExtensionRaw.Name = "txtFileExtensionRaw";
            this.txtFileExtensionRaw.Size = new System.Drawing.Size(257, 20);
            this.txtFileExtensionRaw.TabIndex = 7;
            this.txtFileExtensionRaw.TextChanged += new System.EventHandler(this.txtFileExtensionRaw_TextChanged);
            // 
            // lblRawFile
            // 
            this.lblRawFile.AutoSize = true;
            this.lblRawFile.Location = new System.Drawing.Point(12, 67);
            this.lblRawFile.Name = "lblRawFile";
            this.lblRawFile.Size = new System.Drawing.Size(140, 13);
            this.lblRawFile.TabIndex = 6;
            this.lblRawFile.Text = "Dateiendung RAW-Dateien:";
            // 
            // btnDeleteRawFile
            // 
            this.btnDeleteRawFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteRawFile.Location = new System.Drawing.Point(12, 90);
            this.btnDeleteRawFile.Name = "btnDeleteRawFile";
            this.btnDeleteRawFile.Size = new System.Drawing.Size(511, 46);
            this.btnDeleteRawFile.TabIndex = 9;
            this.btnDeleteRawFile.Text = "RAW-Dateien ohne zugehörige Vergleichsdatei löschen";
            this.btnDeleteRawFile.UseVisualStyleBackColor = true;
            this.btnDeleteRawFile.Click += new System.EventHandler(this.btnDeleteRawFile_Click);
            // 
            // grbDeleteException
            // 
            this.grbDeleteException.Controls.Add(this.lsvDeleteException);
            this.grbDeleteException.Location = new System.Drawing.Point(12, 142);
            this.grbDeleteException.Name = "grbDeleteException";
            this.grbDeleteException.Size = new System.Drawing.Size(653, 194);
            this.grbDeleteException.TabIndex = 11;
            this.grbDeleteException.TabStop = false;
            this.grbDeleteException.Text = "Fehler beim Löschen von Dateien";
            // 
            // lsvDeleteException
            // 
            this.lsvDeleteException.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lsvDeleteException.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhFile,
            this.clhException});
            this.lsvDeleteException.FullRowSelect = true;
            this.lsvDeleteException.GridLines = true;
            this.lsvDeleteException.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvDeleteException.HideSelection = false;
            this.lsvDeleteException.Location = new System.Drawing.Point(6, 19);
            this.lsvDeleteException.Name = "lsvDeleteException";
            this.lsvDeleteException.ShowItemToolTips = true;
            this.lsvDeleteException.Size = new System.Drawing.Size(641, 169);
            this.lsvDeleteException.TabIndex = 0;
            this.lsvDeleteException.UseCompatibleStateImageBehavior = false;
            this.lsvDeleteException.View = System.Windows.Forms.View.Details;
            // 
            // clhFile
            // 
            this.clhFile.Text = "Datei";
            this.clhFile.Width = 150;
            // 
            // clhException
            // 
            this.clhException.Text = "Fehler";
            this.clhException.Width = 450;
            // 
            // lblPictureAdvice
            // 
            this.lblPictureAdvice.AutoSize = true;
            this.lblPictureAdvice.Location = new System.Drawing.Point(462, 41);
            this.lblPictureAdvice.Name = "lblPictureAdvice";
            this.lblPictureAdvice.Size = new System.Drawing.Size(203, 13);
            this.lblPictureAdvice.TabIndex = 5;
            this.lblPictureAdvice.Text = "Mehrfacheingaben durch Komma trennen";
            // 
            // lblRawFileAdvice
            // 
            this.lblRawFileAdvice.AutoSize = true;
            this.lblRawFileAdvice.Location = new System.Drawing.Point(462, 67);
            this.lblRawFileAdvice.Name = "lblRawFileAdvice";
            this.lblRawFileAdvice.Size = new System.Drawing.Size(203, 13);
            this.lblRawFileAdvice.TabIndex = 8;
            this.lblRawFileAdvice.Text = "Mehrfacheingaben durch Komma trennen";
            // 
            // btnFileExtensionCorrespondingRestore
            // 
            this.btnFileExtensionCorrespondingRestore.Image = ((System.Drawing.Image)(resources.GetObject("btnFileExtensionCorrespondingRestore.Image")));
            this.btnFileExtensionCorrespondingRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFileExtensionCorrespondingRestore.Location = new System.Drawing.Point(433, 36);
            this.btnFileExtensionCorrespondingRestore.Name = "btnFileExtensionCorrespondingRestore";
            this.btnFileExtensionCorrespondingRestore.Size = new System.Drawing.Size(23, 23);
            this.btnFileExtensionCorrespondingRestore.TabIndex = 10;
            this.btnFileExtensionCorrespondingRestore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFileExtensionCorrespondingRestore.UseVisualStyleBackColor = true;
            this.btnFileExtensionCorrespondingRestore.Click += new System.EventHandler(this.btnFileExtensionCorrespondingRestore_Click);
            // 
            // btnFileExtensionRawRestore
            // 
            this.btnFileExtensionRawRestore.Image = ((System.Drawing.Image)(resources.GetObject("btnFileExtensionRawRestore.Image")));
            this.btnFileExtensionRawRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFileExtensionRawRestore.Location = new System.Drawing.Point(433, 62);
            this.btnFileExtensionRawRestore.Name = "btnFileExtensionRawRestore";
            this.btnFileExtensionRawRestore.Size = new System.Drawing.Size(23, 23);
            this.btnFileExtensionRawRestore.TabIndex = 12;
            this.btnFileExtensionRawRestore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFileExtensionRawRestore.UseVisualStyleBackColor = true;
            this.btnFileExtensionRawRestore.Click += new System.EventHandler(this.btnFileExtensionRawRestore_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 348);
            this.Controls.Add(this.btnFileExtensionRawRestore);
            this.Controls.Add(this.btnFileExtensionCorrespondingRestore);
            this.Controls.Add(this.lblRawFileAdvice);
            this.Controls.Add(this.lblPictureAdvice);
            this.Controls.Add(this.grbDeleteException);
            this.Controls.Add(this.btnDeleteRawFile);
            this.Controls.Add(this.lblRawFile);
            this.Controls.Add(this.txtFileExtensionRaw);
            this.Controls.Add(this.txtFileExtensionCorresponding);
            this.Controls.Add(this.lblPicture);
            this.Controls.Add(this.btnDirectory);
            this.Controls.Add(this.txtDirectroy);
            this.Controls.Add(this.lblDirectroy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RaMaDe";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.MainForm_HelpButtonClicked);
            this.grbDeleteException.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.ColumnHeader clhException;
        private System.Windows.Forms.ColumnHeader clhFile;
        private System.Windows.Forms.ListView lsvDeleteException;
        private System.Windows.Forms.GroupBox grbDeleteException;
        private System.Windows.Forms.Label lblDirectroy;
        private System.Windows.Forms.TextBox txtDirectroy;
        private System.Windows.Forms.Button btnDirectory;
        private System.Windows.Forms.Label lblPicture;
        private System.Windows.Forms.TextBox txtFileExtensionCorresponding;
        private System.Windows.Forms.TextBox txtFileExtensionRaw;
        private System.Windows.Forms.Label lblRawFile;
        private System.Windows.Forms.Button btnDeleteRawFile;

        #endregion

        private System.Windows.Forms.Label lblPictureAdvice;
        private System.Windows.Forms.Label lblRawFileAdvice;
        private System.Windows.Forms.Button btnFileExtensionCorrespondingRestore;
        private System.Windows.Forms.Button btnFileExtensionRawRestore;
    }
}

