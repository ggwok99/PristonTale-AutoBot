using AutoClicker.Controls;
using AutoClicker.Enums;
using AutoClicker.Helpers;
using AutoClicker.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
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

                txtScreenResolutionX.Text = data.ScreenResolution.X.ToString();
                txtScreenResolutionY.Text = data.ScreenResolution.Y.ToString();
                Master.Instance.ScreenResolution = data.ScreenResolution;

                foreach (Profile profile in data.Profiles)
                {
                    TabPage tp = new TabPage(profile.Name);
                    UControlContext context = new UControlContext(IntPtr.Zero);
                    context.LoadData(profile.Data);
                    tp.Controls.Add(context);
                    tabUControl.TabPages.Add(tp);
                }

                Master.Instance.PotPositions = data.Attributes;
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
                    ScreenResolution = new ScreenResolution(txtScreenResolutionX.Text, txtScreenResolutionY.Text),
                    Attributes = Master.Instance.PotPositions,
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

        private void BtnAnalyze_Click(object sender, EventArgs e)
        {
            IntPtr intPtr;
            try
            {
                intPtr = GetIntPtr();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (chkFullScreen.Checked)
            {
                User32.ShowWindow(intPtr, AutoClickHandlers.SW_MAXIMIZED);
                Thread.Sleep(500);
            }

            List<CharacterAttributePosition> attributePositions;
            using (Bitmap screenCapture = (Bitmap)ScreenCapture.CaptureWindow(intPtr))
            {
                try
                {
                    attributePositions = GetCharacterAttributePositions(screenCapture);
                }
                catch
                {
                    MessageBox.Show("Error!");
                    UpdateStatusStripLabel("Please analyze again, cannot detect HP/MP/STM bar.");
                    return;
                }
            }

            CharacterAttributePosition hp = attributePositions.Where(x => x.Type == AttributeType.HP).FirstOrDefault();
            if (hp == null)
            {
                MessageBox.Show("Error!");
                UpdateStatusStripLabel("Please analyze again, cannot detect HP bar.");
                return;
            }

            CharacterAttributePosition mp = attributePositions.Where(x => x.Type == AttributeType.MP).FirstOrDefault();
            if (mp == null)
            {
                MessageBox.Show("Error!");
                UpdateStatusStripLabel("Please analyze again, cannot detect MP bar.");
                return;
            }

            CharacterAttributePosition stm = attributePositions.Where(x => x.Type == AttributeType.STM).FirstOrDefault();
            if (stm == null || stm.Position.Left > mp.Position.Left)
            {
                MessageBox.Show("Error!");
                UpdateStatusStripLabel("Please analyze again, cannot detect STM bar.");
                return;
            }

            Master.Instance.PotPositions = attributePositions;
            Master.Instance.ScreenResolution = new ScreenResolution(txtScreenResolutionX.Text, txtScreenResolutionY.Text);

            UpdateStatusStripLabel("Successfully Analyzed.");

            if (chkFullScreen.Checked)
            {
                User32.SetForegroundWindow(Handle);
            }
        }

        private static List<CharacterAttributePosition> GetCharacterAttributePositions(Bitmap bitmap)
        {
            List<CharacterAttributePosition> results = new List<CharacterAttributePosition>();
            using (Bitmap attributeFiltered = ImageProcessing.FilterImageForAttributePositions(bitmap))
            {
                List<CharacterAttributePosition> detectedPositions = ImageProcessing.GetCharacterAttributePositions(attributeFiltered);
                foreach (CharacterAttributePosition attribute in detectedPositions)
                {
                    CharacterAttributePosition duplicate = results.FirstOrDefault(x => x.Equals(attribute));
                    if (duplicate == null)
                    {
                        results.Add(attribute);
                    }
                }
            }

            List<CharacterAttributePosition> stm = results.Where(x => x.Type == AttributeType.STM).ToList();
            if (stm.Count > 1)
            {
                CharacterAttributePosition mp = results.FirstOrDefault(x => x.Type == AttributeType.MP);
                results.Remove(stm.FirstOrDefault(x => x.Position.Left > mp.Position.Left));
            }

            results.ForEach(x =>
            {
                if (x.Type == AttributeType.STM)
                {
                    return;
                }

                x.Position = new RECT((x.Position.Left + x.Position.Right) / 2, x.Position.Top, x.Position.Right, x.Position.Bottom);
            });
            return results;
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
            tp.Controls.Add(new UControlContext(intPtr));
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
            IntPtr intPtr = IntPtr.Zero;
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
