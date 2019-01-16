namespace AutoClicker.Controls
{
    partial class UControlContext
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbxMainSkill = new System.Windows.Forms.ComboBox();
            this.lblMainSkill = new System.Windows.Forms.Label();
            this.lblAttackStyle = new System.Windows.Forms.Label();
            this.cbxAttackStyle = new System.Windows.Forms.ComboBox();
            this.grpAttributeControls = new System.Windows.Forms.GroupBox();
            this.btnPotPositionCapture = new System.Windows.Forms.Button();
            this.txtPotRefillTimeOut = new System.Windows.Forms.TextBox();
            this.lblPotDescription = new System.Windows.Forms.Label();
            this.keyPotSTM = new System.Windows.Forms.NumericUpDown();
            this.keyPotMP = new System.Windows.Forms.NumericUpDown();
            this.lblPotHotKeyDescription = new System.Windows.Forms.Label();
            this.keyPotHP = new System.Windows.Forms.NumericUpDown();
            this.lblPotSTM = new System.Windows.Forms.Label();
            this.lblPotMP = new System.Windows.Forms.Label();
            this.lblPotHP = new System.Windows.Forms.Label();
            this.txtPotSTM = new System.Windows.Forms.TextBox();
            this.txtPotMP = new System.Windows.Forms.TextBox();
            this.txtPotHP = new System.Windows.Forms.TextBox();
            this.grpBuffs = new System.Windows.Forms.GroupBox();
            this.txtBuffF4 = new System.Windows.Forms.TextBox();
            this.lblBuffF4 = new System.Windows.Forms.Label();
            this.txtBuffF3 = new System.Windows.Forms.TextBox();
            this.lblBuffF3 = new System.Windows.Forms.Label();
            this.txtBuffF8 = new System.Windows.Forms.TextBox();
            this.txtBuffF7 = new System.Windows.Forms.TextBox();
            this.lblBuffF8 = new System.Windows.Forms.Label();
            this.lblBuffF7 = new System.Windows.Forms.Label();
            this.txtBuffF6 = new System.Windows.Forms.TextBox();
            this.txtBuffF5 = new System.Windows.Forms.TextBox();
            this.lblBuffF6 = new System.Windows.Forms.Label();
            this.lblBuffF5 = new System.Windows.Forms.Label();
            this.chkAutoAttack = new System.Windows.Forms.CheckBox();
            this.chkAutoBuff = new System.Windows.Forms.CheckBox();
            this.lblAutoUsage = new System.Windows.Forms.Label();
            this.chkAutoPot = new System.Windows.Forms.CheckBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnCloseTab = new System.Windows.Forms.Button();
            this.chkAutoPickItem = new System.Windows.Forms.CheckBox();
            this.lblStatus = new AutoClicker.Controls.LabelObserver();
            this.groupBox1.SuspendLayout();
            this.grpAttributeControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keyPotSTM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyPotMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyPotHP)).BeginInit();
            this.grpBuffs.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxMainSkill);
            this.groupBox1.Controls.Add(this.lblMainSkill);
            this.groupBox1.Controls.Add(this.lblAttackStyle);
            this.groupBox1.Controls.Add(this.cbxAttackStyle);
            this.groupBox1.Location = new System.Drawing.Point(21, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 228);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto Attack";
            // 
            // cbxMainSkill
            // 
            this.cbxMainSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMainSkill.FormattingEnabled = true;
            this.cbxMainSkill.Location = new System.Drawing.Point(371, 27);
            this.cbxMainSkill.Name = "cbxMainSkill";
            this.cbxMainSkill.Size = new System.Drawing.Size(72, 24);
            this.cbxMainSkill.TabIndex = 1;
            // 
            // lblMainSkill
            // 
            this.lblMainSkill.AutoSize = true;
            this.lblMainSkill.Location = new System.Drawing.Point(245, 30);
            this.lblMainSkill.Name = "lblMainSkill";
            this.lblMainSkill.Size = new System.Drawing.Size(119, 17);
            this.lblMainSkill.TabIndex = 8;
            this.lblMainSkill.Text = "Main Skill Hotkey:";
            // 
            // lblAttackStyle
            // 
            this.lblAttackStyle.AutoSize = true;
            this.lblAttackStyle.Location = new System.Drawing.Point(14, 30);
            this.lblAttackStyle.Name = "lblAttackStyle";
            this.lblAttackStyle.Size = new System.Drawing.Size(86, 17);
            this.lblAttackStyle.TabIndex = 1;
            this.lblAttackStyle.Text = "Attack Style:";
            // 
            // cbxAttackStyle
            // 
            this.cbxAttackStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAttackStyle.FormattingEnabled = true;
            this.cbxAttackStyle.Location = new System.Drawing.Point(106, 26);
            this.cbxAttackStyle.Name = "cbxAttackStyle";
            this.cbxAttackStyle.Size = new System.Drawing.Size(133, 24);
            this.cbxAttackStyle.TabIndex = 0;
            this.cbxAttackStyle.SelectedIndexChanged += new System.EventHandler(this.CbxAttackStyle_SelectedIndexChanged);
            // 
            // grpAttributeControls
            // 
            this.grpAttributeControls.Controls.Add(this.btnPotPositionCapture);
            this.grpAttributeControls.Controls.Add(this.txtPotRefillTimeOut);
            this.grpAttributeControls.Controls.Add(this.lblPotDescription);
            this.grpAttributeControls.Controls.Add(this.keyPotSTM);
            this.grpAttributeControls.Controls.Add(this.keyPotMP);
            this.grpAttributeControls.Controls.Add(this.lblPotHotKeyDescription);
            this.grpAttributeControls.Controls.Add(this.keyPotHP);
            this.grpAttributeControls.Controls.Add(this.lblPotSTM);
            this.grpAttributeControls.Controls.Add(this.lblPotMP);
            this.grpAttributeControls.Controls.Add(this.lblPotHP);
            this.grpAttributeControls.Controls.Add(this.txtPotSTM);
            this.grpAttributeControls.Controls.Add(this.txtPotMP);
            this.grpAttributeControls.Controls.Add(this.txtPotHP);
            this.grpAttributeControls.Location = new System.Drawing.Point(497, 172);
            this.grpAttributeControls.Name = "grpAttributeControls";
            this.grpAttributeControls.Size = new System.Drawing.Size(380, 167);
            this.grpAttributeControls.TabIndex = 6;
            this.grpAttributeControls.TabStop = false;
            this.grpAttributeControls.Text = "Potions (%)";
            // 
            // btnPotPositionCapture
            // 
            this.btnPotPositionCapture.Location = new System.Drawing.Point(237, 124);
            this.btnPotPositionCapture.Name = "btnPotPositionCapture";
            this.btnPotPositionCapture.Size = new System.Drawing.Size(116, 30);
            this.btnPotPositionCapture.TabIndex = 7;
            this.btnPotPositionCapture.Text = "Set Potion Area";
            this.btnPotPositionCapture.UseVisualStyleBackColor = true;
            this.btnPotPositionCapture.Click += new System.EventHandler(this.BtnPotPositionCapture_Click);
            // 
            // txtPotRefillTimeOut
            // 
            this.txtPotRefillTimeOut.Location = new System.Drawing.Point(202, 128);
            this.txtPotRefillTimeOut.MaxLength = 2;
            this.txtPotRefillTimeOut.Name = "txtPotRefillTimeOut";
            this.txtPotRefillTimeOut.Size = new System.Drawing.Size(29, 22);
            this.txtPotRefillTimeOut.TabIndex = 6;
            this.txtPotRefillTimeOut.Text = "25";
            this.txtPotRefillTimeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPotRefillTimeOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterNumericInput);
            // 
            // lblPotDescription
            // 
            this.lblPotDescription.AutoSize = true;
            this.lblPotDescription.Location = new System.Drawing.Point(18, 131);
            this.lblPotDescription.Name = "lblPotDescription";
            this.lblPotDescription.Size = new System.Drawing.Size(183, 17);
            this.lblPotDescription.TabIndex = 10;
            this.lblPotDescription.Text = "Potion Refill Timer (minute):";
            this.lblPotDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // keyPotSTM
            // 
            this.keyPotSTM.Location = new System.Drawing.Point(298, 83);
            this.keyPotSTM.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.keyPotSTM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.keyPotSTM.Name = "keyPotSTM";
            this.keyPotSTM.Size = new System.Drawing.Size(55, 22);
            this.keyPotSTM.TabIndex = 5;
            this.keyPotSTM.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // keyPotMP
            // 
            this.keyPotMP.Location = new System.Drawing.Point(298, 55);
            this.keyPotMP.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.keyPotMP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.keyPotMP.Name = "keyPotMP";
            this.keyPotMP.Size = new System.Drawing.Size(55, 22);
            this.keyPotMP.TabIndex = 3;
            this.keyPotMP.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblPotHotKeyDescription
            // 
            this.lblPotHotKeyDescription.AutoSize = true;
            this.lblPotHotKeyDescription.Location = new System.Drawing.Point(183, 57);
            this.lblPotHotKeyDescription.Name = "lblPotHotKeyDescription";
            this.lblPotHotKeyDescription.Size = new System.Drawing.Size(109, 17);
            this.lblPotHotKeyDescription.TabIndex = 7;
            this.lblPotHotKeyDescription.Text = "HotKey Binding:";
            // 
            // keyPotHP
            // 
            this.keyPotHP.Location = new System.Drawing.Point(298, 27);
            this.keyPotHP.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.keyPotHP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.keyPotHP.Name = "keyPotHP";
            this.keyPotHP.Size = new System.Drawing.Size(55, 22);
            this.keyPotHP.TabIndex = 1;
            this.keyPotHP.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblPotSTM
            // 
            this.lblPotSTM.AutoSize = true;
            this.lblPotSTM.Location = new System.Drawing.Point(32, 86);
            this.lblPotSTM.Name = "lblPotSTM";
            this.lblPotSTM.Size = new System.Drawing.Size(41, 17);
            this.lblPotSTM.TabIndex = 5;
            this.lblPotSTM.Text = "STM:";
            // 
            // lblPotMP
            // 
            this.lblPotMP.AutoSize = true;
            this.lblPotMP.Location = new System.Drawing.Point(32, 58);
            this.lblPotMP.Name = "lblPotMP";
            this.lblPotMP.Size = new System.Drawing.Size(32, 17);
            this.lblPotMP.TabIndex = 4;
            this.lblPotMP.Text = "MP:";
            // 
            // lblPotHP
            // 
            this.lblPotHP.AutoSize = true;
            this.lblPotHP.Location = new System.Drawing.Point(32, 30);
            this.lblPotHP.Name = "lblPotHP";
            this.lblPotHP.Size = new System.Drawing.Size(31, 17);
            this.lblPotHP.TabIndex = 3;
            this.lblPotHP.Text = "HP:";
            // 
            // txtPotSTM
            // 
            this.txtPotSTM.Location = new System.Drawing.Point(79, 83);
            this.txtPotSTM.MaxLength = 2;
            this.txtPotSTM.Name = "txtPotSTM";
            this.txtPotSTM.Size = new System.Drawing.Size(57, 22);
            this.txtPotSTM.TabIndex = 4;
            this.txtPotSTM.Text = "7";
            this.txtPotSTM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPotSTM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterNumericInput);
            // 
            // txtPotMP
            // 
            this.txtPotMP.Location = new System.Drawing.Point(79, 55);
            this.txtPotMP.MaxLength = 2;
            this.txtPotMP.Name = "txtPotMP";
            this.txtPotMP.Size = new System.Drawing.Size(57, 22);
            this.txtPotMP.TabIndex = 2;
            this.txtPotMP.Text = "10";
            this.txtPotMP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPotMP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterNumericInput);
            // 
            // txtPotHP
            // 
            this.txtPotHP.Location = new System.Drawing.Point(79, 27);
            this.txtPotHP.MaxLength = 2;
            this.txtPotHP.Name = "txtPotHP";
            this.txtPotHP.Size = new System.Drawing.Size(57, 22);
            this.txtPotHP.TabIndex = 0;
            this.txtPotHP.Text = "30";
            this.txtPotHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPotHP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterNumericInput);
            // 
            // grpBuffs
            // 
            this.grpBuffs.Controls.Add(this.txtBuffF4);
            this.grpBuffs.Controls.Add(this.lblBuffF4);
            this.grpBuffs.Controls.Add(this.txtBuffF3);
            this.grpBuffs.Controls.Add(this.lblBuffF3);
            this.grpBuffs.Controls.Add(this.txtBuffF8);
            this.grpBuffs.Controls.Add(this.txtBuffF7);
            this.grpBuffs.Controls.Add(this.lblBuffF8);
            this.grpBuffs.Controls.Add(this.lblBuffF7);
            this.grpBuffs.Controls.Add(this.txtBuffF6);
            this.grpBuffs.Controls.Add(this.txtBuffF5);
            this.grpBuffs.Controls.Add(this.lblBuffF6);
            this.grpBuffs.Controls.Add(this.lblBuffF5);
            this.grpBuffs.Location = new System.Drawing.Point(497, 24);
            this.grpBuffs.Name = "grpBuffs";
            this.grpBuffs.Size = new System.Drawing.Size(380, 142);
            this.grpBuffs.TabIndex = 5;
            this.grpBuffs.TabStop = false;
            this.grpBuffs.Text = "Buffs (seconds)";
            // 
            // txtBuffF4
            // 
            this.txtBuffF4.Location = new System.Drawing.Point(273, 31);
            this.txtBuffF4.MaxLength = 9;
            this.txtBuffF4.Name = "txtBuffF4";
            this.txtBuffF4.Size = new System.Drawing.Size(80, 22);
            this.txtBuffF4.TabIndex = 1;
            this.txtBuffF4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuffF4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterNumericInput);
            // 
            // lblBuffF4
            // 
            this.lblBuffF4.AutoSize = true;
            this.lblBuffF4.Location = new System.Drawing.Point(239, 34);
            this.lblBuffF4.Name = "lblBuffF4";
            this.lblBuffF4.Size = new System.Drawing.Size(28, 17);
            this.lblBuffF4.TabIndex = 12;
            this.lblBuffF4.Text = "F4:";
            // 
            // txtBuffF3
            // 
            this.txtBuffF3.Location = new System.Drawing.Point(85, 31);
            this.txtBuffF3.MaxLength = 9;
            this.txtBuffF3.Name = "txtBuffF3";
            this.txtBuffF3.Size = new System.Drawing.Size(80, 22);
            this.txtBuffF3.TabIndex = 0;
            this.txtBuffF3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuffF3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterNumericInput);
            // 
            // lblBuffF3
            // 
            this.lblBuffF3.AutoSize = true;
            this.lblBuffF3.Location = new System.Drawing.Point(51, 34);
            this.lblBuffF3.Name = "lblBuffF3";
            this.lblBuffF3.Size = new System.Drawing.Size(28, 17);
            this.lblBuffF3.TabIndex = 10;
            this.lblBuffF3.Text = "F3:";
            // 
            // txtBuffF8
            // 
            this.txtBuffF8.Location = new System.Drawing.Point(273, 99);
            this.txtBuffF8.MaxLength = 9;
            this.txtBuffF8.Name = "txtBuffF8";
            this.txtBuffF8.Size = new System.Drawing.Size(80, 22);
            this.txtBuffF8.TabIndex = 5;
            this.txtBuffF8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuffF8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterNumericInput);
            // 
            // txtBuffF7
            // 
            this.txtBuffF7.Location = new System.Drawing.Point(85, 99);
            this.txtBuffF7.MaxLength = 9;
            this.txtBuffF7.Name = "txtBuffF7";
            this.txtBuffF7.Size = new System.Drawing.Size(80, 22);
            this.txtBuffF7.TabIndex = 4;
            this.txtBuffF7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuffF7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterNumericInput);
            // 
            // lblBuffF8
            // 
            this.lblBuffF8.AutoSize = true;
            this.lblBuffF8.Location = new System.Drawing.Point(239, 102);
            this.lblBuffF8.Name = "lblBuffF8";
            this.lblBuffF8.Size = new System.Drawing.Size(28, 17);
            this.lblBuffF8.TabIndex = 3;
            this.lblBuffF8.Text = "F8:";
            // 
            // lblBuffF7
            // 
            this.lblBuffF7.AutoSize = true;
            this.lblBuffF7.Location = new System.Drawing.Point(51, 102);
            this.lblBuffF7.Name = "lblBuffF7";
            this.lblBuffF7.Size = new System.Drawing.Size(28, 17);
            this.lblBuffF7.TabIndex = 2;
            this.lblBuffF7.Text = "F7:";
            // 
            // txtBuffF6
            // 
            this.txtBuffF6.Location = new System.Drawing.Point(273, 64);
            this.txtBuffF6.MaxLength = 9;
            this.txtBuffF6.Name = "txtBuffF6";
            this.txtBuffF6.Size = new System.Drawing.Size(80, 22);
            this.txtBuffF6.TabIndex = 3;
            this.txtBuffF6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuffF6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterNumericInput);
            // 
            // txtBuffF5
            // 
            this.txtBuffF5.Location = new System.Drawing.Point(85, 64);
            this.txtBuffF5.MaxLength = 9;
            this.txtBuffF5.Name = "txtBuffF5";
            this.txtBuffF5.Size = new System.Drawing.Size(80, 22);
            this.txtBuffF5.TabIndex = 2;
            this.txtBuffF5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuffF5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterNumericInput);
            // 
            // lblBuffF6
            // 
            this.lblBuffF6.AutoSize = true;
            this.lblBuffF6.Location = new System.Drawing.Point(239, 67);
            this.lblBuffF6.Name = "lblBuffF6";
            this.lblBuffF6.Size = new System.Drawing.Size(28, 17);
            this.lblBuffF6.TabIndex = 1;
            this.lblBuffF6.Text = "F6:";
            // 
            // lblBuffF5
            // 
            this.lblBuffF5.AutoSize = true;
            this.lblBuffF5.Location = new System.Drawing.Point(51, 67);
            this.lblBuffF5.Name = "lblBuffF5";
            this.lblBuffF5.Size = new System.Drawing.Size(28, 17);
            this.lblBuffF5.TabIndex = 0;
            this.lblBuffF5.Text = "F5:";
            // 
            // chkAutoAttack
            // 
            this.chkAutoAttack.AutoSize = true;
            this.chkAutoAttack.Checked = true;
            this.chkAutoAttack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoAttack.Location = new System.Drawing.Point(247, 59);
            this.chkAutoAttack.Name = "chkAutoAttack";
            this.chkAutoAttack.Size = new System.Drawing.Size(102, 21);
            this.chkAutoAttack.TabIndex = 2;
            this.chkAutoAttack.Text = "Auto Attack";
            this.chkAutoAttack.UseVisualStyleBackColor = true;
            this.chkAutoAttack.CheckedChanged += new System.EventHandler(this.ChkAutoAttack_CheckedChanged);
            // 
            // chkAutoBuff
            // 
            this.chkAutoBuff.AutoSize = true;
            this.chkAutoBuff.Checked = true;
            this.chkAutoBuff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoBuff.Location = new System.Drawing.Point(136, 59);
            this.chkAutoBuff.Name = "chkAutoBuff";
            this.chkAutoBuff.Size = new System.Drawing.Size(95, 21);
            this.chkAutoBuff.TabIndex = 1;
            this.chkAutoBuff.Text = "Auto Buffs";
            this.chkAutoBuff.UseVisualStyleBackColor = true;
            this.chkAutoBuff.CheckedChanged += new System.EventHandler(this.ChkAutoBuff_CheckedChanged);
            // 
            // lblAutoUsage
            // 
            this.lblAutoUsage.AutoSize = true;
            this.lblAutoUsage.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoUsage.Location = new System.Drawing.Point(191, 11);
            this.lblAutoUsage.Name = "lblAutoUsage";
            this.lblAutoUsage.Size = new System.Drawing.Size(115, 29);
            this.lblAutoUsage.TabIndex = 14;
            this.lblAutoUsage.Text = "Auto Usage";
            // 
            // chkAutoPot
            // 
            this.chkAutoPot.AutoSize = true;
            this.chkAutoPot.Checked = true;
            this.chkAutoPot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoPot.Location = new System.Drawing.Point(21, 59);
            this.chkAutoPot.Name = "chkAutoPot";
            this.chkAutoPot.Size = new System.Drawing.Size(106, 21);
            this.chkAutoPot.TabIndex = 0;
            this.chkAutoPot.Text = "Use Potions";
            this.chkAutoPot.UseVisualStyleBackColor = true;
            this.chkAutoPot.CheckedChanged += new System.EventHandler(this.ChkAutoPot_CheckedChanged);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(380, 328);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(99, 36);
            this.btnStartStop.TabIndex = 7;
            this.btnStartStop.Text = "Start / Stop";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.BtnStartStop_Click);
            // 
            // btnCloseTab
            // 
            this.btnCloseTab.Location = new System.Drawing.Point(847, 3);
            this.btnCloseTab.Name = "btnCloseTab";
            this.btnCloseTab.Size = new System.Drawing.Size(30, 23);
            this.btnCloseTab.TabIndex = 8;
            this.btnCloseTab.TabStop = false;
            this.btnCloseTab.Text = "X";
            this.btnCloseTab.UseVisualStyleBackColor = true;
            this.btnCloseTab.Click += new System.EventHandler(this.BtnCloseTab_Click);
            // 
            // chkAutoPickItem
            // 
            this.chkAutoPickItem.AutoSize = true;
            this.chkAutoPickItem.Checked = true;
            this.chkAutoPickItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoPickItem.Location = new System.Drawing.Point(366, 59);
            this.chkAutoPickItem.Name = "chkAutoPickItem";
            this.chkAutoPickItem.Size = new System.Drawing.Size(113, 21);
            this.chkAutoPickItem.TabIndex = 3;
            this.chkAutoPickItem.Text = "Pick up Items";
            this.chkAutoPickItem.UseVisualStyleBackColor = true;
            this.chkAutoPickItem.CheckedChanged += new System.EventHandler(this.ChkAutoPickItem_CheckedChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(17, 344);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(57, 20);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "Status";
            // 
            // UControlContext
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.chkAutoPickItem);
            this.Controls.Add(this.btnCloseTab);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.chkAutoAttack);
            this.Controls.Add(this.chkAutoBuff);
            this.Controls.Add(this.lblAutoUsage);
            this.Controls.Add(this.chkAutoPot);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpAttributeControls);
            this.Controls.Add(this.grpBuffs);
            this.Name = "UControlContext";
            this.Size = new System.Drawing.Size(897, 383);
            this.Load += new System.EventHandler(this.UControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpAttributeControls.ResumeLayout(false);
            this.grpAttributeControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keyPotSTM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyPotMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keyPotHP)).EndInit();
            this.grpBuffs.ResumeLayout(false);
            this.grpBuffs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbxMainSkill;
        private System.Windows.Forms.Label lblMainSkill;
        private System.Windows.Forms.Label lblAttackStyle;
        private System.Windows.Forms.ComboBox cbxAttackStyle;
        private System.Windows.Forms.GroupBox grpAttributeControls;
        private System.Windows.Forms.Button btnPotPositionCapture;
        private System.Windows.Forms.TextBox txtPotRefillTimeOut;
        private System.Windows.Forms.Label lblPotDescription;
        private System.Windows.Forms.NumericUpDown keyPotSTM;
        private System.Windows.Forms.NumericUpDown keyPotMP;
        private System.Windows.Forms.Label lblPotHotKeyDescription;
        private System.Windows.Forms.NumericUpDown keyPotHP;
        private System.Windows.Forms.Label lblPotSTM;
        private System.Windows.Forms.Label lblPotMP;
        private System.Windows.Forms.Label lblPotHP;
        private System.Windows.Forms.TextBox txtPotSTM;
        private System.Windows.Forms.TextBox txtPotMP;
        private System.Windows.Forms.TextBox txtPotHP;
        private System.Windows.Forms.GroupBox grpBuffs;
        private System.Windows.Forms.TextBox txtBuffF8;
        private System.Windows.Forms.TextBox txtBuffF7;
        private System.Windows.Forms.Label lblBuffF8;
        private System.Windows.Forms.Label lblBuffF7;
        private System.Windows.Forms.TextBox txtBuffF6;
        private System.Windows.Forms.TextBox txtBuffF5;
        private System.Windows.Forms.CheckBox chkAutoAttack;
        private System.Windows.Forms.CheckBox chkAutoBuff;
        private System.Windows.Forms.Label lblAutoUsage;
        private System.Windows.Forms.CheckBox chkAutoPot;
        private System.Windows.Forms.TextBox txtBuffF4;
        private System.Windows.Forms.Label lblBuffF4;
        private System.Windows.Forms.TextBox txtBuffF3;
        private System.Windows.Forms.Label lblBuffF3;
        private System.Windows.Forms.Label lblBuffF6;
        private System.Windows.Forms.Label lblBuffF5;
        private System.Windows.Forms.Button btnStartStop;
        private LabelObserver lblStatus;
        private System.Windows.Forms.Button btnCloseTab;
        private System.Windows.Forms.CheckBox chkAutoPickItem;

        public LabelObserver LabelStatus
        {
            get { return lblStatus; }
            set { lblStatus = value; }
        }
    }
}
