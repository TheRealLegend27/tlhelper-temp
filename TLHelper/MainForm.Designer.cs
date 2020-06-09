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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.rootContainer = new System.Windows.Forms.TableLayoutPanel();
            this.sidebarContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lUserName = new System.Windows.Forms.Label();
            this.lLogedInAs = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.sbbOverview = new TLHelper.UI.Controls.SidebarButton();
            this.sbbScripts = new TLHelper.UI.Controls.SidebarButton();
            this.sbbSettings = new TLHelper.UI.Controls.SidebarButton();
            this.sbbActions = new TLHelper.UI.Controls.SidebarButton();
            this.sbbEfficiency = new TLHelper.UI.Controls.SidebarButton();
            this.rootContainer.SuspendLayout();
            this.sidebarContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootContainer
            // 
            this.rootContainer.ColumnCount = 2;
            this.rootContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.rootContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootContainer.Controls.Add(this.sidebarContainer, 0, 0);
            this.rootContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootContainer.Location = new System.Drawing.Point(0, 0);
            this.rootContainer.Margin = new System.Windows.Forms.Padding(0);
            this.rootContainer.Name = "rootContainer";
            this.rootContainer.RowCount = 1;
            this.rootContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootContainer.Size = new System.Drawing.Size(1400, 700);
            this.rootContainer.TabIndex = 0;
            // 
            // sidebarContainer
            // 
            this.sidebarContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(133)))), ((int)(((byte)(168)))));
            this.sidebarContainer.ColumnCount = 1;
            this.sidebarContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sidebarContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.sidebarContainer.Controls.Add(this.lTitle, 0, 0);
            this.sidebarContainer.Controls.Add(this.panel1, 0, 2);
            this.sidebarContainer.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.sidebarContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidebarContainer.Location = new System.Drawing.Point(0, 0);
            this.sidebarContainer.Margin = new System.Windows.Forms.Padding(0);
            this.sidebarContainer.Name = "sidebarContainer";
            this.sidebarContainer.RowCount = 3;
            this.sidebarContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.sidebarContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.sidebarContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.sidebarContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.sidebarContainer.Size = new System.Drawing.Size(250, 700);
            this.sidebarContainer.TabIndex = 0;
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lTitle.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.ForeColor = System.Drawing.Color.White;
            this.lTitle.Location = new System.Drawing.Point(3, 0);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(244, 100);
            this.lTitle.TabIndex = 0;
            this.lTitle.Text = "TLHelper";
            this.lTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lUserName);
            this.panel1.Controls.Add(this.lLogedInAs);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 600);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 100);
            this.panel1.TabIndex = 1;
            // 
            // lUserName
            // 
            this.lUserName.AutoSize = true;
            this.lUserName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lUserName.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lUserName.ForeColor = System.Drawing.Color.White;
            this.lUserName.Location = new System.Drawing.Point(14, 43);
            this.lUserName.Name = "lUserName";
            this.lUserName.Size = new System.Drawing.Size(84, 19);
            this.lUserName.TabIndex = 1;
            this.lUserName.Text = "Ben Fischer";
            this.lUserName.Click += new System.EventHandler(this.Logout);
            // 
            // lLogedInAs
            // 
            this.lLogedInAs.AutoSize = true;
            this.lLogedInAs.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLogedInAs.ForeColor = System.Drawing.Color.White;
            this.lLogedInAs.Location = new System.Drawing.Point(9, 20);
            this.lLogedInAs.Name = "lLogedInAs";
            this.lLogedInAs.Size = new System.Drawing.Size(85, 19);
            this.lLogedInAs.TabIndex = 0;
            this.lLogedInAs.Text = "Loged in as";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.sbbOverview);
            this.flowLayoutPanel1.Controls.Add(this.sbbScripts);
            this.flowLayoutPanel1.Controls.Add(this.sbbSettings);
            this.flowLayoutPanel1.Controls.Add(this.sbbActions);
            this.flowLayoutPanel1.Controls.Add(this.sbbEfficiency);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 100);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(250, 500);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // sbbOverview
            // 
            this.sbbOverview.Active = true;
            this.sbbOverview.ChevronIcon = ((System.Drawing.Image)(resources.GetObject("sbbOverview.ChevronIcon")));
            this.sbbOverview.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbbOverview.Location = new System.Drawing.Point(0, 0);
            this.sbbOverview.Margin = new System.Windows.Forms.Padding(0);
            this.sbbOverview.Name = "sbbOverview";
            this.sbbOverview.Size = new System.Drawing.Size(250, 50);
            this.sbbOverview.SpecialIcon = ((System.Drawing.Image)(resources.GetObject("sbbOverview.SpecialIcon")));
            this.sbbOverview.TabIndex = 3;
            this.sbbOverview.Text = "Overview";
            this.sbbOverview.UseVisualStyleBackColor = true;
            this.sbbOverview.Click += new System.EventHandler(this.OverviewClicked);
            // 
            // sbbScripts
            // 
            this.sbbScripts.Active = false;
            this.sbbScripts.ChevronIcon = ((System.Drawing.Image)(resources.GetObject("sbbScripts.ChevronIcon")));
            this.sbbScripts.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbbScripts.Location = new System.Drawing.Point(0, 50);
            this.sbbScripts.Margin = new System.Windows.Forms.Padding(0);
            this.sbbScripts.Name = "sbbScripts";
            this.sbbScripts.Size = new System.Drawing.Size(250, 50);
            this.sbbScripts.SpecialIcon = ((System.Drawing.Image)(resources.GetObject("sbbScripts.SpecialIcon")));
            this.sbbScripts.TabIndex = 4;
            this.sbbScripts.Text = "Scripts";
            this.sbbScripts.UseVisualStyleBackColor = true;
            this.sbbScripts.Click += new System.EventHandler(this.ScriptsClicked);
            // 
            // sbbSettings
            // 
            this.sbbSettings.Active = false;
            this.sbbSettings.ChevronIcon = ((System.Drawing.Image)(resources.GetObject("sbbSettings.ChevronIcon")));
            this.sbbSettings.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbbSettings.Location = new System.Drawing.Point(0, 100);
            this.sbbSettings.Margin = new System.Windows.Forms.Padding(0);
            this.sbbSettings.Name = "sbbSettings";
            this.sbbSettings.Size = new System.Drawing.Size(250, 50);
            this.sbbSettings.SpecialIcon = ((System.Drawing.Image)(resources.GetObject("sbbSettings.SpecialIcon")));
            this.sbbSettings.TabIndex = 5;
            this.sbbSettings.Text = "Settings";
            this.sbbSettings.UseVisualStyleBackColor = true;
            this.sbbSettings.Click += new System.EventHandler(this.SettingsClicked);
            // 
            // sbbActions
            // 
            this.sbbActions.Active = false;
            this.sbbActions.ChevronIcon = ((System.Drawing.Image)(resources.GetObject("sbbActions.ChevronIcon")));
            this.sbbActions.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbbActions.Location = new System.Drawing.Point(0, 150);
            this.sbbActions.Margin = new System.Windows.Forms.Padding(0);
            this.sbbActions.Name = "sbbActions";
            this.sbbActions.Size = new System.Drawing.Size(250, 50);
            this.sbbActions.SpecialIcon = ((System.Drawing.Image)(resources.GetObject("sbbActions.SpecialIcon")));
            this.sbbActions.TabIndex = 6;
            this.sbbActions.Text = "Actions";
            this.sbbActions.UseVisualStyleBackColor = true;
            this.sbbActions.Click += new System.EventHandler(this.ActionsClicked);
            // 
            // sbbEfficiency
            // 
            this.sbbEfficiency.Active = false;
            this.sbbEfficiency.ChevronIcon = ((System.Drawing.Image)(resources.GetObject("sbbEfficiency.ChevronIcon")));
            this.sbbEfficiency.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbbEfficiency.Location = new System.Drawing.Point(0, 200);
            this.sbbEfficiency.Margin = new System.Windows.Forms.Padding(0);
            this.sbbEfficiency.Name = "sbbEfficiency";
            this.sbbEfficiency.Size = new System.Drawing.Size(250, 50);
            this.sbbEfficiency.SpecialIcon = ((System.Drawing.Image)(resources.GetObject("sbbEfficiency.SpecialIcon")));
            this.sbbEfficiency.TabIndex = 7;
            this.sbbEfficiency.Text = "Efficiency";
            this.sbbEfficiency.UseVisualStyleBackColor = true;
            this.sbbEfficiency.Click += new System.EventHandler(this.EfficiencyClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.rootContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "TL Helper";
            this.rootContainer.ResumeLayout(false);
            this.sidebarContainer.ResumeLayout(false);
            this.sidebarContainer.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootContainer;
        private System.Windows.Forms.TableLayoutPanel sidebarContainer;
        private System.Windows.Forms.Label lTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lUserName;
        private System.Windows.Forms.Label lLogedInAs;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private UI.Controls.SidebarButton sbbOverview;
        private UI.Controls.SidebarButton sbbScripts;
        private UI.Controls.SidebarButton sbbSettings;
        private UI.Controls.SidebarButton sbbActions;
        private UI.Controls.SidebarButton sbbEfficiency;
    }
}

