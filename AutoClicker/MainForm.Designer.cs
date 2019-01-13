namespace AutoClicker
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.helpPanel = new System.Windows.Forms.Panel();
            this.helperViewer = new System.Windows.Forms.WebBrowser();
            this.systemPanel = new System.Windows.Forms.Panel();
            this.tabUControl = new System.Windows.Forms.TabControl();
            this.grpData = new System.Windows.Forms.GroupBox();
            this.btnPairProcess = new System.Windows.Forms.Button();
            this.btnNewUControl = new System.Windows.Forms.Button();
            this.lbxUControl = new System.Windows.Forms.ListBox();
            this.btnRefreshProcess = new System.Windows.Forms.Button();
            this.lbxProcess = new System.Windows.Forms.ListBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.lblClientName = new System.Windows.Forms.Label();
            this.txtScreenResolutionY = new System.Windows.Forms.TextBox();
            this.txtScreenResolutionX = new System.Windows.Forms.TextBox();
            this.lblResolutionDivider = new System.Windows.Forms.Label();
            this.lblResolution = new System.Windows.Forms.Label();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.chkFullScreen = new System.Windows.Forms.CheckBox();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.helpPanel.SuspendLayout();
            this.systemPanel.SuspendLayout();
            this.grpData.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusStrip});
            this.statusStrip.Location = new System.Drawing.Point(0, 642);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(929, 25);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblStatusStrip
            // 
            this.lblStatusStrip.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblStatusStrip.Name = "lblStatusStrip";
            this.lblStatusStrip.Size = new System.Drawing.Size(139, 20);
            this.lblStatusStrip.Text = "PristonTale AutoBot";
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSystem,
            this.menuHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(929, 36);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuSystem
            // 
            this.menuSystem.BackColor = System.Drawing.Color.White;
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(86, 32);
            this.menuSystem.Text = "System";
            this.menuSystem.Click += new System.EventHandler(this.MenuSystem_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(65, 32);
            this.menuHelp.Text = "Help";
            this.menuHelp.Click += new System.EventHandler(this.MenuHelp_Click);
            // 
            // helpPanel
            // 
            this.helpPanel.Controls.Add(this.helperViewer);
            this.helpPanel.Location = new System.Drawing.Point(0, 31);
            this.helpPanel.Name = "helpPanel";
            this.helpPanel.Size = new System.Drawing.Size(926, 608);
            this.helpPanel.TabIndex = 2;
            this.helpPanel.Visible = false;
            // 
            // helperViewer
            // 
            this.helperViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helperViewer.Location = new System.Drawing.Point(0, 0);
            this.helperViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.helperViewer.Name = "helperViewer";
            this.helperViewer.Size = new System.Drawing.Size(926, 608);
            this.helperViewer.TabIndex = 0;
            // 
            // systemPanel
            // 
            this.systemPanel.Controls.Add(this.tabUControl);
            this.systemPanel.Controls.Add(this.grpData);
            this.systemPanel.Controls.Add(this.btnAnalyze);
            this.systemPanel.Controls.Add(this.btnSave);
            this.systemPanel.Controls.Add(this.grpGeneral);
            this.systemPanel.Location = new System.Drawing.Point(0, 39);
            this.systemPanel.Name = "systemPanel";
            this.systemPanel.Size = new System.Drawing.Size(926, 603);
            this.systemPanel.TabIndex = 1;
            // 
            // tabUControl
            // 
            this.tabUControl.Location = new System.Drawing.Point(12, 128);
            this.tabUControl.Name = "tabUControl";
            this.tabUControl.SelectedIndex = 0;
            this.tabUControl.Size = new System.Drawing.Size(905, 412);
            this.tabUControl.TabIndex = 2;
            this.tabUControl.SelectedIndexChanged += new System.EventHandler(this.TabUControl_SelectedIndexChanged);
            this.tabUControl.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.TabUControl_ControlChanged);
            this.tabUControl.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.TabUControl_ControlChanged);
            // 
            // grpData
            // 
            this.grpData.Controls.Add(this.btnPairProcess);
            this.grpData.Controls.Add(this.btnNewUControl);
            this.grpData.Controls.Add(this.lbxUControl);
            this.grpData.Controls.Add(this.btnRefreshProcess);
            this.grpData.Controls.Add(this.lbxProcess);
            this.grpData.Font = new System.Drawing.Font("Century", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpData.Location = new System.Drawing.Point(365, 5);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(552, 117);
            this.grpData.TabIndex = 1;
            this.grpData.TabStop = false;
            this.grpData.Text = "Link Process";
            // 
            // btnPairProcess
            // 
            this.btnPairProcess.Location = new System.Drawing.Point(468, 67);
            this.btnPairProcess.Name = "btnPairProcess";
            this.btnPairProcess.Size = new System.Drawing.Size(60, 30);
            this.btnPairProcess.TabIndex = 4;
            this.btnPairProcess.Text = "Pair";
            this.btnPairProcess.UseVisualStyleBackColor = true;
            this.btnPairProcess.Click += new System.EventHandler(this.BtnPairProcess_Click);
            // 
            // btnNewUControl
            // 
            this.btnNewUControl.Location = new System.Drawing.Point(468, 31);
            this.btnNewUControl.Name = "btnNewUControl";
            this.btnNewUControl.Size = new System.Drawing.Size(60, 30);
            this.btnNewUControl.TabIndex = 3;
            this.btnNewUControl.Text = "New";
            this.btnNewUControl.UseVisualStyleBackColor = true;
            this.btnNewUControl.Click += new System.EventHandler(this.BtnNewUControl_Click);
            // 
            // lbxUControl
            // 
            this.lbxUControl.FormattingEnabled = true;
            this.lbxUControl.ItemHeight = 18;
            this.lbxUControl.Location = new System.Drawing.Point(263, 31);
            this.lbxUControl.Name = "lbxUControl";
            this.lbxUControl.Size = new System.Drawing.Size(178, 76);
            this.lbxUControl.TabIndex = 2;
            // 
            // btnRefreshProcess
            // 
            this.btnRefreshProcess.Location = new System.Drawing.Point(23, 45);
            this.btnRefreshProcess.Name = "btnRefreshProcess";
            this.btnRefreshProcess.Size = new System.Drawing.Size(92, 30);
            this.btnRefreshProcess.TabIndex = 0;
            this.btnRefreshProcess.Text = "Refresh";
            this.btnRefreshProcess.UseVisualStyleBackColor = true;
            this.btnRefreshProcess.Click += new System.EventHandler(this.BtnRefreshProcess_Click);
            // 
            // lbxProcess
            // 
            this.lbxProcess.FormattingEnabled = true;
            this.lbxProcess.ItemHeight = 18;
            this.lbxProcess.Location = new System.Drawing.Point(141, 31);
            this.lbxProcess.Name = "lbxProcess";
            this.lbxProcess.Size = new System.Drawing.Size(116, 76);
            this.lbxProcess.TabIndex = 1;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(12, 546);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(150, 46);
            this.btnAnalyze.TabIndex = 3;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.BtnAnalyze_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(767, 546);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 46);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // grpGeneral
            // 
            this.grpGeneral.Controls.Add(this.chkFullScreen);
            this.grpGeneral.Controls.Add(this.lblClientName);
            this.grpGeneral.Controls.Add(this.txtScreenResolutionY);
            this.grpGeneral.Controls.Add(this.txtScreenResolutionX);
            this.grpGeneral.Controls.Add(this.lblResolutionDivider);
            this.grpGeneral.Controls.Add(this.lblResolution);
            this.grpGeneral.Controls.Add(this.txtClientName);
            this.grpGeneral.Font = new System.Drawing.Font("Century", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGeneral.Location = new System.Drawing.Point(12, 5);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(347, 117);
            this.grpGeneral.TabIndex = 0;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.Location = new System.Drawing.Point(17, 22);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(114, 18);
            this.lblClientName.TabIndex = 11;
            this.lblClientName.Text = "Client Window";
            // 
            // txtScreenResolutionY
            // 
            this.txtScreenResolutionY.Location = new System.Drawing.Point(270, 51);
            this.txtScreenResolutionY.MaxLength = 4;
            this.txtScreenResolutionY.Name = "txtScreenResolutionY";
            this.txtScreenResolutionY.Size = new System.Drawing.Size(60, 26);
            this.txtScreenResolutionY.TabIndex = 10;
            this.txtScreenResolutionY.TabStop = false;
            this.txtScreenResolutionY.Text = "1080";
            this.txtScreenResolutionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtScreenResolutionX
            // 
            this.txtScreenResolutionX.Location = new System.Drawing.Point(181, 51);
            this.txtScreenResolutionX.MaxLength = 4;
            this.txtScreenResolutionX.Name = "txtScreenResolutionX";
            this.txtScreenResolutionX.Size = new System.Drawing.Size(60, 26);
            this.txtScreenResolutionX.TabIndex = 9;
            this.txtScreenResolutionX.TabStop = false;
            this.txtScreenResolutionX.Text = "1920";
            this.txtScreenResolutionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblResolutionDivider
            // 
            this.lblResolutionDivider.AutoSize = true;
            this.lblResolutionDivider.Location = new System.Drawing.Point(247, 54);
            this.lblResolutionDivider.Name = "lblResolutionDivider";
            this.lblResolutionDivider.Size = new System.Drawing.Size(16, 18);
            this.lblResolutionDivider.TabIndex = 8;
            this.lblResolutionDivider.Text = "x";
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(17, 56);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(136, 18);
            this.lblResolution.TabIndex = 7;
            this.lblResolution.Text = "Screen Resolution";
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(181, 19);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(149, 26);
            this.txtClientName.TabIndex = 1;
            this.txtClientName.TabStop = false;
            this.txtClientName.Text = "Priston Tale";
            this.txtClientName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkFullScreen
            // 
            this.chkFullScreen.AutoSize = true;
            this.chkFullScreen.Checked = true;
            this.chkFullScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFullScreen.Location = new System.Drawing.Point(20, 85);
            this.chkFullScreen.Name = "chkFullScreen";
            this.chkFullScreen.Size = new System.Drawing.Size(238, 22);
            this.chkFullScreen.TabIndex = 5;
            this.chkFullScreen.Text = "Fullscreen when use AutoBot";
            this.chkFullScreen.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(929, 667);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.systemPanel);
            this.Controls.Add(this.helpPanel);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PristonTale AutoBot";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.helpPanel.ResumeLayout(false);
            this.systemPanel.ResumeLayout(false);
            this.grpData.ResumeLayout(false);
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuSystem;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.Panel helpPanel;
        private System.Windows.Forms.Panel systemPanel;
        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.TextBox txtClientName;
        private System.Windows.Forms.TextBox txtScreenResolutionY;
        private System.Windows.Forms.TextBox txtScreenResolutionX;
        private System.Windows.Forms.Label lblResolutionDivider;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.GroupBox grpData;
        private System.Windows.Forms.ListBox lbxProcess;
        private System.Windows.Forms.Button btnRefreshProcess;
        private System.Windows.Forms.Button btnNewUControl;
        private System.Windows.Forms.ListBox lbxUControl;
        private System.Windows.Forms.TabControl tabUControl;
        private System.Windows.Forms.Button btnPairProcess;
        private System.Windows.Forms.WebBrowser helperViewer;
        private System.Windows.Forms.CheckBox chkFullScreen;

        public System.Windows.Forms.TabControl TabUControl => tabUControl;
        public System.Windows.Forms.ToolStripStatusLabel StatusLabel => lblStatusStrip;
    }
}