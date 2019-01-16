using AutoClicker.Constants;
using AutoClicker.Enums;
using AutoClicker.Helpers;
using AutoClicker.Interfaces;
using AutoClicker.Models;
using AutoClicker.Strategies;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Windows.Forms;

namespace AutoClicker.Controls
{
    public partial class UControlContext : UserControl, IObserver
    {
        private dynamic refillPotData = null;
        public IntPtr HWnd { get; set; }
        public ContextState State { get; set; }

        public UControlContext(IntPtr intPtr)
        {
            InitializeComponent();
            cbxAttackStyle.DataSource = Enum.GetValues(typeof(AttackStyleType));
            cbxAttackStyle.SelectedIndex = 0;
            cbxMainSkill.DataSource = Enum.GetValues(typeof(MainSkillKeys));
            cbxMainSkill.SelectedIndex = 0;

            HWnd = intPtr;

            State = new ContextStoppedState(intPtr);
        }

        private void UControl_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;

            lblStatus.SetSubject(this);
            State.Attach(lblStatus);
            State.Attach(this);

            ChkAutoPot_CheckedChanged(null, null);
            ChkAutoBuff_CheckedChanged(null, null);
            ChkAutoAttack_CheckedChanged(null, null);
            ChkAutoPickItem_CheckedChanged(null, null);

            if (HWnd == IntPtr.Zero)
            {
                IEnumerable<Control> buttons = GetAll(this, typeof(Button));
                foreach (Button button in buttons)
                {
                    button.Enabled = false;
                }
            }
        }

        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            if (HWnd == IntPtr.Zero)
            {
                MessageBox.Show("No process is selected.", "Error");
                return;
            }

            State.SwitchState(this);
            dynamic potData = GetPotDataFromForm();
            dynamic buffData = GetBuffDataFromForm();
            dynamic atkData = GetAttackDataFromForm();
            dynamic pickItemData = GetPickItemDataFromForm();
            State.Execute(potData, buffData, atkData, refillPotData, pickItemData);
        }

        #region Auto Pot
        private void ChkAutoPot_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoPot.Checked)
            {
                State.SetPottingStrategy(new ActivePottingStrategy(HWnd));
                if (refillPotData != null)
                {
                    State.SetRefillPotStrategy(new ActiveRefillPotStrategy(HWnd));
                }
            }
            else
            {
                State.SetPottingStrategy(new DeactivePottingStrategy(HWnd));
                State.SetRefillPotStrategy(new DeactiveRefillPotStrategy(HWnd));
            }
        }
        private dynamic GetPotDataFromForm()
        {
            dynamic data = new ExpandoObject();
            if (!string.IsNullOrWhiteSpace(txtPotHP.Text))
            {
                dynamic potHP = new ExpandoObject();
                potHP.Value = int.Parse(txtPotHP.Text);
                potHP.Key = (Keys)keyPotHP.Value.ToString()[0];
                data.HP = potHP;
            }

            if (!string.IsNullOrWhiteSpace(txtPotMP.Text))
            {
                dynamic potMP = new ExpandoObject();
                potMP.Value = int.Parse(txtPotMP.Text);
                potMP.Key = (Keys)keyPotMP.Value.ToString()[0];
                data.MP = potMP;
            }

            if (!string.IsNullOrWhiteSpace(txtPotSTM.Text))
            {
                dynamic potSTM = new ExpandoObject();
                potSTM.Value = int.Parse(txtPotSTM.Text);
                potSTM.Key = (Keys)keyPotSTM.Value.ToString()[0];
                data.STM = potSTM;
            }

            return data;
        }
        private void LoadPotionData(dynamic data)
        {
            txtPotHP.Text = data.HP.Value;
            Keys key = (Keys)data.HP.Key;
            SetKeyPot(key, keyPotHP);

            txtPotMP.Text = data.MP.Value;
            key = (Keys)data.MP.Key;
            SetKeyPot(key, keyPotMP);

            txtPotSTM.Text = data.STM.Value;
            key = (Keys)data.STM.Key;
            SetKeyPot(key, keyPotSTM);
        }
        private void SetKeyPot(Keys key, NumericUpDown target)
        {
            switch (key)
            {
                case Keys.D1:
                    target.Value = 1;
                    break;
                case Keys.D2:
                    target.Value = 2;
                    break;
                case Keys.D3:
                    target.Value = 3;
                    break;
            }
        }
        #endregion

        #region Auto Buff
        private void ChkAutoBuff_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoBuff.Checked)
            {
                State.SetBuffStrategy(new ActiveBuffStrategy(HWnd));
            }
            else
            {
                State.SetBuffStrategy(new DeactiveBuffStrategy(HWnd));
            }
        }
        private dynamic GetBuffDataFromForm()
        {
            dynamic data = new ExpandoObject();
            List<BuffItem> buffs = new List<BuffItem>();
            if (!string.IsNullOrWhiteSpace(txtBuffF3.Text))
            {
                buffs.Add(new BuffItem(Keys.F3, txtBuffF3.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtBuffF4.Text))
            {
                buffs.Add(new BuffItem(Keys.F4, txtBuffF4.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtBuffF5.Text))
            {
                buffs.Add(new BuffItem(Keys.F5, txtBuffF5.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtBuffF6.Text))
            {
                buffs.Add(new BuffItem(Keys.F6, txtBuffF6.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtBuffF7.Text))
            {
                buffs.Add(new BuffItem(Keys.F7, txtBuffF7.Text));
            }

            if (!string.IsNullOrWhiteSpace(txtBuffF8.Text))
            {
                buffs.Add(new BuffItem(Keys.F8, txtBuffF8.Text));
            }
            data.Buffs = buffs;
            return data;
        }
        private void LoadBuffData(dynamic data)
        {
            List<BuffItem> buffs = JsonConvert.DeserializeObject<List<BuffItem>>(data.Buffs.ToString());
            foreach (BuffItem buff in buffs)
            {
                switch (buff.Key)
                {
                    case Keys.F3:
                        txtBuffF3.Text = buff.Delay.ToString();
                        break;
                    case Keys.F4:
                        txtBuffF4.Text = buff.Delay.ToString();
                        break;
                    case Keys.F5:
                        txtBuffF5.Text = buff.Delay.ToString();
                        break;
                    case Keys.F6:
                        txtBuffF6.Text = buff.Delay.ToString();
                        break;
                    case Keys.F7:
                        txtBuffF7.Text = buff.Delay.ToString();
                        break;
                    case Keys.F8:
                        txtBuffF8.Text = buff.Delay.ToString();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Auto Attack
        private void ChkAutoAttack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoAttack.Checked)
            {
                State.SetAttackingStrategy(new ActiveAttackingStrategy(HWnd));
            }
            else
            {
                State.SetAttackingStrategy(new DeactiveAttackingStrategy(HWnd));
            }
        }
        private dynamic GetAttackDataFromForm()
        {
            dynamic data = new ExpandoObject();
            Enum.TryParse(cbxAttackStyle.SelectedValue.ToString(), out AttackStyleType attackStyle);
            data.Style = attackStyle;

            Enum.TryParse(cbxMainSkill.SelectedValue.ToString(), out Keys mainSkillHotkey);
            data.MainSkillHotkey = mainSkillHotkey;
            return data;
        }
        private void LoadAttackData(dynamic data)
        {
            cbxAttackStyle.SelectedIndex = (int)data.Style.Value;
            cbxMainSkill.SelectedItem = (Keys)data.MainSkillHotkey.Value;
        }
        private void CbxAttackStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            Enum.TryParse(cmb.SelectedValue.ToString(), out AttackStyleType type);
        }
        #endregion

        #region Auto Pot Refill
        private dynamic GetPotRefillData(RECT hp, RECT mp, RECT stm)
        {
            dynamic data = new ExpandoObject();
            Point hpPoint = hp.Center.ScaleDownToClientPoint(HWnd);
            Point mpPoint = mp.Center.ScaleDownToClientPoint(HWnd);
            Point stmPoint = stm.Center.ScaleDownToClientPoint(HWnd);
            data.PotPosition = new PotPosition(hpPoint, mpPoint, stmPoint);
            data.KeyHP = (Keys)keyPotHP.Value.ToString()[0];
            data.KeyMP = (Keys)keyPotMP.Value.ToString()[0];
            data.KeySTM = (Keys)keyPotSTM.Value.ToString()[0];
            int.TryParse(txtPotRefillTimeOut.Text, out int timeOut);
            data.TimeOut = timeOut;
            return data;
        }
        private void BtnPotPositionCapture_Click(object sender, EventArgs e)
        {
            MainForm form = Parent.Parent.Parent.Parent as MainForm;
            form.StatusLabel.Text = Env.ProgramName;

            // Close all open tabs
            AutoClickHandlers.SendKeyPress(HWnd, Keys.Space, 300);

            // Open inventory
            AutoClickHandlers.SendKeyPress(HWnd, Keys.V, 300);

            SnippingTool.AreaSelected += OnAreaSelected;
            SnippingTool.Snip(HWnd);
        }
        private void OnAreaSelected(object sender, EventArgs e)
        {
            Rectangle increasedRect = new Rectangle(SnippingTool.Area.Location, new Size((int)(SnippingTool.Area.Size.Width * 1.1), (int)(SnippingTool.Area.Size.Height * 1.35)));
            using (Bitmap bitmap = (Bitmap)ScreenCapture.CapturePartialWindow(HWnd, increasedRect))
            {
                RECT hpRECT = ImageProcessing.GetMatchingImageLocation(bitmap, AttributeType.HP);
                MainForm form = Parent.Parent.Parent.Parent as MainForm;
                if (hpRECT.Width == 0 || hpRECT.Height == 0)
                {
                    form.StatusLabel.Text = "Status: Error! Cannot find life potion. Please try mystic size.";
                    return;
                }
                RECT absoluteLifeRECT = GetAbsoluteRectBySnippingTool(hpRECT);

                RECT mpRECT = ImageProcessing.GetMatchingImageLocation(bitmap, AttributeType.MP);
                if (mpRECT.Width == 0 || mpRECT.Height == 0)
                {
                    form.StatusLabel.Text = "Status: Error! Cannot find mana potion. Please try mystic size.";
                    return;
                }
                RECT absoluteManaRECT = GetAbsoluteRectBySnippingTool(mpRECT);

                RECT stmRECT = ImageProcessing.GetMatchingImageLocation(bitmap, AttributeType.STM);
                if (stmRECT.Width == 0 || stmRECT.Height == 0)
                {
                    form.StatusLabel.Text = "Status: Error! Cannot find stm potion. Please try mystic size.";
                    return;
                }
                RECT absoluteSTMRECT = GetAbsoluteRectBySnippingTool(stmRECT);

                refillPotData = GetPotRefillData(absoluteLifeRECT, absoluteManaRECT, absoluteSTMRECT);
            }

            User32.SetForegroundWindow(Handle);

            if (chkAutoPot.Checked)
            {
                State.SetRefillPotStrategy(new ActiveRefillPotStrategy(HWnd));
            }
        }
        private static RECT GetAbsoluteRectBySnippingTool(RECT rECT)
        {
            return new Rectangle(SnippingTool.Area.Location.X + rECT.X, SnippingTool.Area.Location.Y + rECT.Y, rECT.Width, rECT.Height);
        }
        #endregion

        #region Auto Pick Items
        private void ChkAutoPickItem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoPickItem.Checked)
            {
                State.SetPickItemStrategy(new ActivePickItemStrategy(HWnd));
            }
            else
            {
                State.SetPickItemStrategy(new DeactivePickItemStrategy(HWnd));
            }
        }
        private dynamic GetPickItemDataFromForm()
        {
            dynamic data = new ExpandoObject();
            data.TargetArea = 35;
            return data;
        }
        #endregion

        private void FilterNumericInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void SubjectUpdated()
        {
            if (State is ContextRunningState)
            {
                IEnumerable<Control> textBoxes = GetAll(this, typeof(TextBox));
                foreach (TextBox textBox in textBoxes)
                {
                    textBox.ReadOnly = true;
                }

                IEnumerable<Control> checkBoxes = GetAll(this, typeof(CheckBox));
                foreach (CheckBox checkBox in checkBoxes)
                {
                    checkBox.Enabled = false;
                }

                IEnumerable<Control> comboBoxes = GetAll(this, typeof(ComboBox));
                foreach (ComboBox comboBox in comboBoxes)
                {
                    comboBox.Enabled = false;
                }

                IEnumerable<Control> numericUpDowns = GetAll(this, typeof(NumericUpDown));
                foreach (NumericUpDown numericUpDown in numericUpDowns)
                {
                    numericUpDown.ReadOnly = true;
                    numericUpDown.Increment = 0;
                }

                IEnumerable<Control> buttons = GetAll(this, typeof(Button));
                foreach (Button button in buttons)
                {
                    if (button.Name != "btnStartStop")
                    {
                        button.Enabled = false;
                    }
                }
            }
            else
            {
                IEnumerable<Control> textBoxes = GetAll(this, typeof(TextBox));
                foreach (TextBox textBox in textBoxes)
                {
                    textBox.ReadOnly = false;
                }

                IEnumerable<Control> checkBoxes = GetAll(this, typeof(CheckBox));
                foreach (CheckBox checkBox in checkBoxes)
                {
                    checkBox.Enabled = true;
                }

                IEnumerable<Control> comboBoxes = GetAll(this, typeof(ComboBox));
                foreach (ComboBox comboBox in comboBoxes)
                {
                    comboBox.Enabled = true;
                }

                IEnumerable<Control> numericUpDowns = GetAll(this, typeof(NumericUpDown));
                foreach (NumericUpDown numericUpDown in numericUpDowns)
                {
                    numericUpDown.ReadOnly = false;
                    numericUpDown.Increment = 1;
                }

                IEnumerable<Control> buttons = GetAll(this, typeof(Button));
                foreach (Button button in buttons)
                {
                    button.Enabled = true;
                }
            }
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            IEnumerable<Control> controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        public void InitIntPtr(IntPtr intPtr)
        {
            HWnd = intPtr;
            State.InitIntPtr(intPtr);

            IEnumerable<Control> buttons = GetAll(this, typeof(Button));
            foreach (Button button in buttons)
            {
                button.Enabled = true;
            }
        }

        private void BtnCloseTab_Click(object sender, EventArgs e)
        {
            MainForm form = Parent.Parent.Parent.Parent as MainForm;
            form.TabUControl.TabPages.Remove(form.TabUControl.SelectedTab);
        }

        public ProfileContextData GetSettingsData()
        {
            ProfileContextData data = new ProfileContextData
            {
                Attack = GetAttackDataFromForm(),
                Buff = GetBuffDataFromForm(),
                Potion = GetPotDataFromForm(),
                RefillPotion = refillPotData,
                PickItem = GetPickItemDataFromForm()
            };
            return data;
        }

        public void LoadData(ProfileContextData data)
        {
            LoadAttackData(data.Attack);
            LoadBuffData(data.Buff);
            LoadPotionData(data.Potion);
            refillPotData = data.RefillPotion;
            if (data.RefillPotion != null)
            {
                txtPotRefillTimeOut.Text = data.RefillPotion.TimeOut;
            }
        }

        public void Final()
        {
            HWnd = IntPtr.Zero;
            State.InitIntPtr(IntPtr.Zero);

            IEnumerable<Control> buttons = GetAll(this, typeof(Button));
            foreach (Button button in buttons)
            {
                button.Enabled = false;
            }
        }
    }
}
