namespace GitChangelog
{
    partial class SettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.groupBoxGitSettings = new System.Windows.Forms.GroupBox();
            this.buttonGitPathDialog = new System.Windows.Forms.Button();
            this.textBoxGitRepositoryPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxJiraSettings = new System.Windows.Forms.GroupBox();
            this.textBoxJiraMatchPrefix = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxJiraProjects = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxJiraPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxJiraUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxJiraHostname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxGitSettings.SuspendLayout();
            this.groupBoxJiraSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxGitSettings
            // 
            this.groupBoxGitSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGitSettings.Controls.Add(this.buttonGitPathDialog);
            this.groupBoxGitSettings.Controls.Add(this.textBoxGitRepositoryPath);
            this.groupBoxGitSettings.Controls.Add(this.label1);
            this.groupBoxGitSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxGitSettings.Name = "groupBoxGitSettings";
            this.groupBoxGitSettings.Size = new System.Drawing.Size(705, 74);
            this.groupBoxGitSettings.TabIndex = 0;
            this.groupBoxGitSettings.TabStop = false;
            this.groupBoxGitSettings.Text = "Git Settings";
            // 
            // buttonGitPathDialog
            // 
            this.buttonGitPathDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGitPathDialog.Location = new System.Drawing.Point(657, 24);
            this.buttonGitPathDialog.Name = "buttonGitPathDialog";
            this.buttonGitPathDialog.Size = new System.Drawing.Size(26, 24);
            this.buttonGitPathDialog.TabIndex = 2;
            this.buttonGitPathDialog.Text = "...";
            this.buttonGitPathDialog.UseVisualStyleBackColor = true;
            this.buttonGitPathDialog.Click += new System.EventHandler(this.buttonGitPathDialog_Click);
            // 
            // textBoxGitRepositoryPath
            // 
            this.textBoxGitRepositoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGitRepositoryPath.Location = new System.Drawing.Point(143, 25);
            this.textBoxGitRepositoryPath.Name = "textBoxGitRepositoryPath";
            this.textBoxGitRepositoryPath.Size = new System.Drawing.Size(510, 22);
            this.textBoxGitRepositoryPath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Git Repository Path";
            // 
            // groupBoxJiraSettings
            // 
            this.groupBoxJiraSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxJiraSettings.Controls.Add(this.textBoxJiraMatchPrefix);
            this.groupBoxJiraSettings.Controls.Add(this.label6);
            this.groupBoxJiraSettings.Controls.Add(this.comboBoxJiraProjects);
            this.groupBoxJiraSettings.Controls.Add(this.label5);
            this.groupBoxJiraSettings.Controls.Add(this.textBoxJiraPassword);
            this.groupBoxJiraSettings.Controls.Add(this.label4);
            this.groupBoxJiraSettings.Controls.Add(this.textBoxJiraUsername);
            this.groupBoxJiraSettings.Controls.Add(this.label3);
            this.groupBoxJiraSettings.Controls.Add(this.textBoxJiraHostname);
            this.groupBoxJiraSettings.Controls.Add(this.label2);
            this.groupBoxJiraSettings.Location = new System.Drawing.Point(12, 105);
            this.groupBoxJiraSettings.Name = "groupBoxJiraSettings";
            this.groupBoxJiraSettings.Size = new System.Drawing.Size(705, 173);
            this.groupBoxJiraSettings.TabIndex = 1;
            this.groupBoxJiraSettings.TabStop = false;
            this.groupBoxJiraSettings.Text = "Jira Settings";
            // 
            // textBoxJiraMatchPrefix
            // 
            this.textBoxJiraMatchPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJiraMatchPrefix.Location = new System.Drawing.Point(143, 135);
            this.textBoxJiraMatchPrefix.Name = "textBoxJiraMatchPrefix";
            this.textBoxJiraMatchPrefix.Size = new System.Drawing.Size(540, 22);
            this.textBoxJiraMatchPrefix.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Match Prefix";
            // 
            // comboBoxJiraProjects
            // 
            this.comboBoxJiraProjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxJiraProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxJiraProjects.FormattingEnabled = true;
            this.comboBoxJiraProjects.Location = new System.Drawing.Point(143, 105);
            this.comboBoxJiraProjects.Name = "comboBoxJiraProjects";
            this.comboBoxJiraProjects.Size = new System.Drawing.Size(540, 24);
            this.comboBoxJiraProjects.TabIndex = 10;
            this.comboBoxJiraProjects.DropDown += new System.EventHandler(this.comboBoxJiraProjects_DropDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Project";
            // 
            // textBoxJiraPassword
            // 
            this.textBoxJiraPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJiraPassword.Location = new System.Drawing.Point(143, 77);
            this.textBoxJiraPassword.Name = "textBoxJiraPassword";
            this.textBoxJiraPassword.PasswordChar = '*';
            this.textBoxJiraPassword.Size = new System.Drawing.Size(540, 22);
            this.textBoxJiraPassword.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Password";
            // 
            // textBoxJiraUsername
            // 
            this.textBoxJiraUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJiraUsername.Location = new System.Drawing.Point(143, 49);
            this.textBoxJiraUsername.Name = "textBoxJiraUsername";
            this.textBoxJiraUsername.Size = new System.Drawing.Size(540, 22);
            this.textBoxJiraUsername.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Username";
            // 
            // textBoxJiraHostname
            // 
            this.textBoxJiraHostname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJiraHostname.Location = new System.Drawing.Point(143, 21);
            this.textBoxJiraHostname.Name = "textBoxJiraHostname";
            this.textBoxJiraHostname.Size = new System.Drawing.Size(540, 22);
            this.textBoxJiraHostname.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hostname";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(561, 294);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 25);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(642, 294);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 25);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 331);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxJiraSettings);
            this.Controls.Add(this.groupBoxGitSettings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 378);
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Properties";
            this.Shown += new System.EventHandler(this.SettingsDialog_Shown);
            this.groupBoxGitSettings.ResumeLayout(false);
            this.groupBoxGitSettings.PerformLayout();
            this.groupBoxJiraSettings.ResumeLayout(false);
            this.groupBoxJiraSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxGitSettings;
        private System.Windows.Forms.GroupBox groupBoxJiraSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGitPathDialog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxGitRepositoryPath;
        private System.Windows.Forms.ComboBox comboBoxJiraProjects;
        private System.Windows.Forms.TextBox textBoxJiraPassword;
        private System.Windows.Forms.TextBox textBoxJiraUsername;
        private System.Windows.Forms.TextBox textBoxJiraHostname;
        private System.Windows.Forms.TextBox textBoxJiraMatchPrefix;
    }
}