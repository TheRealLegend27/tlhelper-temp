using System;
using System.Windows.Forms;
using TLHelper.HotKeys;
using TLHelper.Resources;

namespace TLHelper.UI.Popups
{
    public partial class HotkeySelectionPopup : Form
    {
        public HotkeySelectionPopup()
        {
            InitializeComponent();
        }

        public void HotKeyChanged()
        {
            label1.Text = SelectedHotKey.GetString();
        }

        public HotKey SelectedHotKey = new HotKey(new Key(Keys.None), false, false, false);

        private void label1_Enter(object sender, EventArgs e)
        {
            ActiveControl = this;
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            HotkeySelectionPopup_MouseClick(sender, e);
        }

        private void HotkeySelectionPopup_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Modifiers.HasFlag(Keys.Alt)) SelectedHotKey.IsAlt = true;
            else SelectedHotKey.IsAlt = false;
            if (e.Modifiers.HasFlag(Keys.Shift)) SelectedHotKey.IsShift = true;
            else SelectedHotKey.IsShift = false;
            if (e.Modifiers.HasFlag(Keys.Control)) SelectedHotKey.IsCtrl = true;
            else SelectedHotKey.IsCtrl = false;

            if (e.KeyValue == (int)Keys.ControlKey || e.KeyValue == (int)Keys.ShiftKey || e.KeyValue == (int)Keys.Menu)
            {
                HotKeyChanged();
                return;
            }

            Keys key = (Keys)e.KeyValue;
            if (key == Keys.Escape) DialogResult = DialogResult.Cancel;
            else
            {
                if (!GlobalData.AllowedKeys.ContainsKey(key))
                {
                    MessageBox.Show("This Key is not supported. Please choose an other Key", "Key is not Supported",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SelectedHotKey.CurrentKey = new Key(key);
                    HotKeyChanged();
                }
            }
        }

        private void HotkeySelectionPopup_MouseClick(object sender, MouseEventArgs e)
        {

            Keys key = Keys.None;

            if (e.Button == MouseButtons.Left) key = Keys.LButton;
            else if (e.Button == MouseButtons.Right) key = Keys.RButton;
            else if (e.Button == MouseButtons.Middle) key = Keys.MButton;
            else if (e.Button == MouseButtons.XButton1) key = Keys.XButton1;
            else if (e.Button == MouseButtons.XButton2) key = Keys.XButton2;

            if (key == Keys.Escape) DialogResult = DialogResult.Cancel;
            else
            {
                if (!GlobalData.AllowedKeys.ContainsKey(key))
                {
                    MessageBox.Show("This Key is not supported. Please choose an other Key", "Key is not Supported",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SelectedHotKey.CurrentKey = new Key(key);
                    HotKeyChanged();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedHotKey.CurrentKey.CurrentKey != Keys.None)
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.Cancel;
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeySelectionPopup_KeyDown(sender, e);
        }
    }
}
