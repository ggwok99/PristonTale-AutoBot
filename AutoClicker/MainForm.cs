using AutoClicker.Controls;
using AutoClicker.Helpers;
using AutoClicker.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class MainForm : Form
    {
        private static readonly Lazy<MainForm> lazy = new Lazy<MainForm>(() => new MainForm());
        public static MainForm Instance => lazy.Value;
        private const string _dataFilename = "data.json";

        private MainForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            if (!File.Exists(_dataFilename))
            {
                UpdateStatusStripLabel("Please setup general settings.");
                return;
            }

            using (StreamReader r = new StreamReader(_dataFilename))
            {
                string json = r.ReadToEnd();
                CustomDataObject data = JsonConvert.DeserializeObject<CustomDataObject>(json);
                txtClientName.Text = data.ClientName;

                chkFullScreen.Checked = data.FullScreenWhenAuto;
                chkFullScreen.CheckState = data.FullScreenWhenAuto ? CheckState.Checked : CheckState.Unchecked;

                txtScreenResolutionX.Text = data.ScreenResolution.X.ToString();
                txtScreenResolutionY.Text = data.ScreenResolution.Y.ToString();
                Master.Instance.ScreenResolution = data.ScreenResolution;

                foreach (Profile profile in data.Profiles)
                {
                    TabPage tp = new TabPage(profile.Name);
                    UControlContext context = new UControlContext(IntPtr.Zero, chkFullScreen.Checked);
                    context.LoadData(profile.Data);
                    tp.Controls.Add(context);
                    tabUControl.TabPages.Add(tp);
                }
            }
        }

        private void UpdateStatusStripLabel(string inputText)
        {
            lblStatusStrip.Text = inputText;
        }

        private void MenuHelp_Click(object sender, EventArgs e)
        {
            helpPanel.Visible = true;
            systemPanel.Visible = !helpPanel.Visible;
        }

        private void MenuSystem_Click(object sender, EventArgs e)
        {
            helpPanel.Visible = false;
            systemPanel.Visible = !helpPanel.Visible;
        }

        private IntPtr GetIntPtr()
        {
            IntPtr result = User32.GetWinHandleByWindowTitle(txtClientName.Text);
            if (result == User32.InvalidHandleValue)
            {
                UpdateStatusStripLabel("Cannot find client window.");
                throw new Exception("Cannot find client window.");
            }

            return result;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using (StringWriter writer = new StringWriter())
            {
                CustomDataObject data = new CustomDataObject
                {
                    ClientName = txtClientName.Text,
                    FullScreenWhenAuto = chkFullScreen.Checked,
                    ScreenResolution = new ScreenResolution(txtScreenResolutionX.Text, txtScreenResolutionY.Text),
                    Profiles = GetTabsData()
                };

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, data);

                File.WriteAllText(_dataFilename, writer.ToString());
            }
            UpdateStatusStripLabel("Data is saved!");
        }

        private List<Profile> GetTabsData()
        {
            List<Profile> allData = new List<Profile>();
            foreach (TabPage tbp in tabUControl.TabPages)
            {
                Profile data = new Profile
                {
                    Name = tbp.Text
                };

                UControlContext context = (UControlContext)tbp.Controls[0];
                data.Data = context.GetSettingsData();

                allData.Add(data);
            }
            return allData;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BtnRefreshProcess_Click(sender, e);
            string localURL = Path.Combine(Directory.GetCurrentDirectory(), "Help.html");
            helperViewer.Url = new Uri(localURL, UriKind.Absolute);
        }

        private void BtnRefreshProcess_Click(object sender, EventArgs e)
        {
            TabUControl_ControlChanged(null, null);
        }

        private void BtnNewUControl_Click(object sender, EventArgs e)
        {
            if (lbxProcess.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a process first!", "Error");
                return;
            }

            string name = Interaction.InputBox("Enter a name for new setup", "Tab name", "Default");
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            IntPtr intPtr = (IntPtr)lbxProcess.SelectedItem;

            if (chkFullScreen.Checked)
            {
                User32.ShowWindow(intPtr, AutoClickHandlers.SW_MAXIMIZED);
            }

            User32.SetForegroundWindow(Handle);

            TabPage tp = new TabPage(name);
            tp.Controls.Add(new UControlContext(intPtr, chkFullScreen.Checked));
            tabUControl.TabPages.Add(tp);
        }

        private void TabUControl_ControlChanged(object sender, ControlEventArgs e)
        {
            lbxProcess.Items.Clear();
            List<IntPtr> hWnds = User32.GetListWinHandleByWindowTitle(txtClientName.Text);
            hWnds.ForEach(hWnd => lbxProcess.Items.Add(hWnd));

            lbxUControl.Items.Clear();
            foreach (TabPage tbp in tabUControl.TabPages)
            {
                UControlContext context = (UControlContext)tbp.Controls[0];
                lbxUControl.Items.Add($"{tbp.Text} ({context.HWnd})");

                if (!hWnds.Contains(context.HWnd))
                {
                    context.Final();
                }
                else
                {
                    lbxProcess.Items.Remove(context.HWnd);
                    lbxProcess.Items.Add($"{context.HWnd} - paired");
                }
            }
        }

        private void BtnPairProcess_Click(object sender, EventArgs e)
        {
            if (lbxProcess.SelectedIndex == -1 || lbxUControl.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a process and control tab first!", "Error");
                return;
            }

            TabPage tbp = tabUControl.TabPages[lbxUControl.SelectedIndex];
            UControlContext context = (UControlContext)tbp.Controls[0];
            IntPtr intPtr;
            if (lbxProcess.SelectedItem.ToString().Contains(" - paired"))
            {
                intPtr = new IntPtr(Convert.ToInt32(lbxProcess.SelectedItem.ToString().Replace(" - paired", string.Empty)));
            }
            else
            {
                intPtr = (IntPtr)lbxProcess.SelectedItem;
            }

            if (chkFullScreen.Checked)
            {
                User32.ShowWindow(intPtr, AutoClickHandlers.SW_MAXIMIZED);
            }
            context.InitIntPtr(intPtr);
            TabUControl_ControlChanged(null, null);
        }

        private void TabUControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabUControl_ControlChanged(null, null);
        }
    }
}
