namespace CarScraping
{
    partial class FormMain
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
            this.buttonScrap = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.textBoxBasePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.comboBoxCountry = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelComment = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonScrap
            // 
            this.buttonScrap.Enabled = false;
            this.buttonScrap.Location = new System.Drawing.Point(167, 150);
            this.buttonScrap.Name = "buttonScrap";
            this.buttonScrap.Size = new System.Drawing.Size(75, 23);
            this.buttonScrap.TabIndex = 0;
            this.buttonScrap.Text = "Start";
            this.buttonScrap.UseVisualStyleBackColor = true;
            this.buttonScrap.Click += new System.EventHandler(this.buttonScrap0_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(15, 151);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(46, 22);
            this.webBrowser.TabIndex = 1;
            this.webBrowser.Visible = false;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser0_DocumentCompleted);
            // 
            // textBoxBasePath
            // 
            this.textBoxBasePath.Location = new System.Drawing.Point(15, 33);
            this.textBoxBasePath.Name = "textBoxBasePath";
            this.textBoxBasePath.Size = new System.Drawing.Size(352, 20);
            this.textBoxBasePath.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Folder to Save:";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(373, 30);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(41, 23);
            this.buttonBrowse.TabIndex = 4;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // comboBoxCountry
            // 
            this.comboBoxCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCountry.FormattingEnabled = true;
            this.comboBoxCountry.Location = new System.Drawing.Point(15, 83);
            this.comboBoxCountry.Name = "comboBoxCountry";
            this.comboBoxCountry.Size = new System.Drawing.Size(399, 21);
            this.comboBoxCountry.TabIndex = 5;
            this.comboBoxCountry.SelectedIndexChanged += new System.EventHandler(this.comboBoxCountry_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select Country:";
            // 
            // labelComment
            // 
            this.labelComment.Location = new System.Drawing.Point(12, 119);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(402, 23);
            this.labelComment.TabIndex = 6;
            this.labelComment.Text = "Waiting for DDoS checking to be completed. Please wait.";
            this.labelComment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 190);
            this.Controls.Add(this.labelComment);
            this.Controls.Add(this.buttonScrap);
            this.Controls.Add(this.comboBoxCountry);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxBasePath);
            this.Controls.Add(this.webBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormMain";
            this.Text = "Scrapper for http://platesmania.com";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonScrap;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TextBox textBoxBasePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.ComboBox comboBoxCountry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelComment;
    }
}

