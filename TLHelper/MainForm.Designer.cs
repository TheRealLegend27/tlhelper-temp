namespace TLHelper
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Minimize = new System.Windows.Forms.PictureBox();
            this.PowerOff = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SideBar = new System.Windows.Forms.Panel();
            this.MenuBar = new System.Windows.Forms.Panel();
            this.MenuButtonOverview = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MenuButtonEfficiencie = new System.Windows.Forms.Button();
            this.MenuButtonActions = new System.Windows.Forms.Button();
            this.MenuButtonSettings = new System.Windows.Forms.Button();
            this.MenuButtonScripts = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lUserName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PowerOff)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.panel1.Controls.Add(this.lUserName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Minimize);
            this.panel1.Controls.Add(this.PowerOff);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.SideBar);
            this.panel1.Controls.Add(this.MenuBar);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 50);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FakeWindowMove);
            // 
            // Minimize
            // 
            this.Minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Minimize.Location = new System.Drawing.Point(1112, 17);
            this.Minimize.Name = "Minimize";
            this.Minimize.Size = new System.Drawing.Size(30, 30);
            this.Minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Minimize.TabIndex = 4;
            this.Minimize.TabStop = false;
            this.Minimize.Click += new System.EventHandler(this.Minimize_Click);
            // 
            // PowerOff
            // 
            this.PowerOff.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PowerOff.Location = new System.Drawing.Point(1148, 7);
            this.PowerOff.Name = "PowerOff";
            this.PowerOff.Size = new System.Drawing.Size(40, 40);
            this.PowerOff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PowerOff.TabIndex = 3;
            this.PowerOff.TabStop = false;
            this.PowerOff.Click += new System.EventHandler(this.PowerOff_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(50, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "TL Helper";
            // 
            // SideBar
            // 
            this.SideBar.Location = new System.Drawing.Point(0, 50);
            this.SideBar.Name = "SideBar";
            this.SideBar.Size = new System.Drawing.Size(200, 550);
            this.SideBar.TabIndex = 1;
            // 
            // MenuBar
            // 
            this.MenuBar.Location = new System.Drawing.Point(0, 50);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(200, 550);
            this.MenuBar.TabIndex = 1;
            // 
            // MenuButtonOverview
            // 
            this.MenuButtonOverview.FlatAppearance.BorderSize = 0;
            this.MenuButtonOverview.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(180)))));
            this.MenuButtonOverview.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(110)))), ((int)(((byte)(210)))));
            this.MenuButtonOverview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MenuButtonOverview.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuButtonOverview.ForeColor = System.Drawing.Color.White;
            this.MenuButtonOverview.Location = new System.Drawing.Point(0, 0);
            this.MenuButtonOverview.Name = "MenuButtonOverview";
            this.MenuButtonOverview.Size = new System.Drawing.Size(200, 40);
            this.MenuButtonOverview.TabIndex = 1;
            this.MenuButtonOverview.Text = "Overview";
            this.MenuButtonOverview.UseVisualStyleBackColor = true;
            this.MenuButtonOverview.Click += new System.EventHandler(this.OverviewClicked);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.panel2.Controls.Add(this.MenuButtonEfficiencie);
            this.panel2.Controls.Add(this.MenuButtonActions);
            this.panel2.Controls.Add(this.MenuButtonSettings);
            this.panel2.Controls.Add(this.MenuButtonScripts);
            this.panel2.Controls.Add(this.MenuButtonOverview);
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 650);
            this.panel2.TabIndex = 2;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FakeWindowMove);
            // 
            // MenuButtonEfficiencie
            // 
            this.MenuButtonEfficiencie.FlatAppearance.BorderSize = 0;
            this.MenuButtonEfficiencie.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(180)))));
            this.MenuButtonEfficiencie.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(110)))), ((int)(((byte)(210)))));
            this.MenuButtonEfficiencie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MenuButtonEfficiencie.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuButtonEfficiencie.ForeColor = System.Drawing.Color.White;
            this.MenuButtonEfficiencie.Location = new System.Drawing.Point(0, 184);
            this.MenuButtonEfficiencie.Name = "MenuButtonEfficiencie";
            this.MenuButtonEfficiencie.Size = new System.Drawing.Size(200, 40);
            this.MenuButtonEfficiencie.TabIndex = 6;
            this.MenuButtonEfficiencie.Text = "Efficiencie";
            this.MenuButtonEfficiencie.UseVisualStyleBackColor = true;
            this.MenuButtonEfficiencie.Click += new System.EventHandler(this.EfficiencyClicked);
            // 
            // MenuButtonActions
            // 
            this.MenuButtonActions.FlatAppearance.BorderSize = 0;
            this.MenuButtonActions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(180)))));
            this.MenuButtonActions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(110)))), ((int)(((byte)(210)))));
            this.MenuButtonActions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MenuButtonActions.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuButtonActions.ForeColor = System.Drawing.Color.White;
            this.MenuButtonActions.Location = new System.Drawing.Point(0, 138);
            this.MenuButtonActions.Name = "MenuButtonActions";
            this.MenuButtonActions.Size = new System.Drawing.Size(200, 40);
            this.MenuButtonActions.TabIndex = 5;
            this.MenuButtonActions.Text = "Actions";
            this.MenuButtonActions.UseVisualStyleBackColor = true;
            this.MenuButtonActions.Click += new System.EventHandler(this.ActionsClicked);
            // 
            // MenuButtonSettings
            // 
            this.MenuButtonSettings.FlatAppearance.BorderSize = 0;
            this.MenuButtonSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(180)))));
            this.MenuButtonSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(110)))), ((int)(((byte)(210)))));
            this.MenuButtonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MenuButtonSettings.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuButtonSettings.ForeColor = System.Drawing.Color.White;
            this.MenuButtonSettings.Location = new System.Drawing.Point(0, 92);
            this.MenuButtonSettings.Name = "MenuButtonSettings";
            this.MenuButtonSettings.Size = new System.Drawing.Size(200, 40);
            this.MenuButtonSettings.TabIndex = 4;
            this.MenuButtonSettings.Text = "Settings";
            this.MenuButtonSettings.UseVisualStyleBackColor = true;
            this.MenuButtonSettings.Click += new System.EventHandler(this.SettingsClicked);
            // 
            // MenuButtonScripts
            // 
            this.MenuButtonScripts.FlatAppearance.BorderSize = 0;
            this.MenuButtonScripts.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(180)))));
            this.MenuButtonScripts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(110)))), ((int)(((byte)(210)))));
            this.MenuButtonScripts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MenuButtonScripts.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuButtonScripts.ForeColor = System.Drawing.Color.White;
            this.MenuButtonScripts.Location = new System.Drawing.Point(0, 46);
            this.MenuButtonScripts.Name = "MenuButtonScripts";
            this.MenuButtonScripts.Size = new System.Drawing.Size(200, 40);
            this.MenuButtonScripts.TabIndex = 3;
            this.MenuButtonScripts.Text = "Script";
            this.MenuButtonScripts.UseVisualStyleBackColor = true;
            this.MenuButtonScripts.Click += new System.EventHandler(this.ScriptsClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(201, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Loged in as: ";
            // 
            // lUserName
            // 
            this.lUserName.AutoSize = true;
            this.lUserName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lUserName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lUserName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lUserName.Location = new System.Drawing.Point(290, 17);
            this.lUserName.Name = "lUserName";
            this.lUserName.Size = new System.Drawing.Size(57, 18);
            this.lUserName.TabIndex = 6;
            this.lUserName.Text = "No User";
            this.lUserName.Click += new System.EventHandler(this.Logout);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TL Helper";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PowerOff)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel SideBar;
        private System.Windows.Forms.Panel MenuBar;
        private System.Windows.Forms.Button MenuButtonOverview;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button MenuButtonEfficiencie;
        private System.Windows.Forms.Button MenuButtonActions;
        private System.Windows.Forms.Button MenuButtonSettings;
        private System.Windows.Forms.Button MenuButtonScripts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Minimize;
        private System.Windows.Forms.PictureBox PowerOff;
        private System.Windows.Forms.Label lUserName;
        private System.Windows.Forms.Label label2;
    }
}

