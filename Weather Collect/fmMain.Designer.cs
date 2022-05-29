namespace Weather_Collect
{
    partial class fmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmMain));
            this.btnDownload = new System.Windows.Forms.Button();
            this.chkHighDef = new System.Windows.Forms.CheckBox();
            this.pgrDownloadPerc = new System.Windows.Forms.ProgressBar();
            this.btnSaveLocationSel = new System.Windows.Forms.Button();
            this.txtSaveLocation = new System.Windows.Forms.TextBox();
            this.lblFileDownloading = new System.Windows.Forms.Label();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.chkMetNOTAMs = new System.Windows.Forms.CheckBox();
            this.chkEmail = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Location = new System.Drawing.Point(188, 118);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(90, 19);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "DOWNLOAD";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // chkHighDef
            // 
            this.chkHighDef.AutoSize = true;
            this.chkHighDef.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkHighDef.Checked = true;
            this.chkHighDef.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHighDef.Location = new System.Drawing.Point(214, 96);
            this.chkHighDef.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkHighDef.Name = "chkHighDef";
            this.chkHighDef.Size = new System.Drawing.Size(68, 17);
            this.chkHighDef.TabIndex = 1;
            this.chkHighDef.Text = "High Def";
            this.chkHighDef.UseVisualStyleBackColor = true;
            // 
            // pgrDownloadPerc
            // 
            this.pgrDownloadPerc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgrDownloadPerc.Location = new System.Drawing.Point(9, 118);
            this.pgrDownloadPerc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pgrDownloadPerc.Name = "pgrDownloadPerc";
            this.pgrDownloadPerc.Size = new System.Drawing.Size(174, 19);
            this.pgrDownloadPerc.Step = 1;
            this.pgrDownloadPerc.TabIndex = 2;
            // 
            // btnSaveLocationSel
            // 
            this.btnSaveLocationSel.BackgroundImage = global::Weather_Collect.Properties.Resources.DownloadFolder;
            this.btnSaveLocationSel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSaveLocationSel.Location = new System.Drawing.Point(253, 10);
            this.btnSaveLocationSel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSaveLocationSel.Name = "btnSaveLocationSel";
            this.btnSaveLocationSel.Size = new System.Drawing.Size(26, 28);
            this.btnSaveLocationSel.TabIndex = 3;
            this.btnSaveLocationSel.UseVisualStyleBackColor = true;
            this.btnSaveLocationSel.Click += new System.EventHandler(this.btnSaveLocationSel_Click);
            // 
            // txtSaveLocation
            // 
            this.txtSaveLocation.Enabled = false;
            this.txtSaveLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaveLocation.Location = new System.Drawing.Point(9, 11);
            this.txtSaveLocation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSaveLocation.Name = "txtSaveLocation";
            this.txtSaveLocation.Size = new System.Drawing.Size(240, 26);
            this.txtSaveLocation.TabIndex = 4;
            this.txtSaveLocation.Text = "Default Save Location";
            // 
            // lblFileDownloading
            // 
            this.lblFileDownloading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileDownloading.AutoSize = true;
            this.lblFileDownloading.Location = new System.Drawing.Point(9, 96);
            this.lblFileDownloading.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFileDownloading.Name = "lblFileDownloading";
            this.lblFileDownloading.Size = new System.Drawing.Size(96, 13);
            this.lblFileDownloading.TabIndex = 5;
            this.lblFileDownloading.Text = "Download Inactive";
            // 
            // cmbArea
            // 
            this.cmbArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.Items.AddRange(new object[] {
            "SAXA",
            "Resolute",
            "Gateway",
            "East Coast",
            "Hawaii",
            "Hawaii Transit"});
            this.cmbArea.Location = new System.Drawing.Point(9, 40);
            this.cmbArea.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(240, 28);
            this.cmbArea.TabIndex = 6;
            // 
            // chkMetNOTAMs
            // 
            this.chkMetNOTAMs.AutoSize = true;
            this.chkMetNOTAMs.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMetNOTAMs.Enabled = false;
            this.chkMetNOTAMs.Location = new System.Drawing.Point(183, 74);
            this.chkMetNOTAMs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkMetNOTAMs.Name = "chkMetNOTAMs";
            this.chkMetNOTAMs.Size = new System.Drawing.Size(100, 17);
            this.chkMetNOTAMs.TabIndex = 7;
            this.chkMetNOTAMs.Text = "Met + NOTAMs";
            this.chkMetNOTAMs.UseVisualStyleBackColor = true;
            // 
            // chkEmail
            // 
            this.chkEmail.AutoSize = true;
            this.chkEmail.Enabled = false;
            this.chkEmail.Location = new System.Drawing.Point(9, 74);
            this.chkEmail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Size = new System.Drawing.Size(79, 17);
            this.chkEmail.TabIndex = 8;
            this.chkEmail.Text = "Send Email";
            this.chkEmail.UseVisualStyleBackColor = true;
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 146);
            this.Controls.Add(this.chkEmail);
            this.Controls.Add(this.chkMetNOTAMs);
            this.Controls.Add(this.cmbArea);
            this.Controls.Add(this.lblFileDownloading);
            this.Controls.Add(this.txtSaveLocation);
            this.Controls.Add(this.btnSaveLocationSel);
            this.Controls.Add(this.pgrDownloadPerc);
            this.Controls.Add(this.chkHighDef);
            this.Controls.Add(this.btnDownload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "fmMain";
            this.Text = "Weather Collect";
            this.Load += new System.EventHandler(this.fmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.CheckBox chkHighDef;
        private System.Windows.Forms.ProgressBar pgrDownloadPerc;
        private System.Windows.Forms.Button btnSaveLocationSel;
        private System.Windows.Forms.TextBox txtSaveLocation;
        private System.Windows.Forms.Label lblFileDownloading;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.CheckBox chkMetNOTAMs;
        private System.Windows.Forms.CheckBox chkEmail;
    }
}

