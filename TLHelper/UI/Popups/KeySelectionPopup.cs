using System;
using System.Windows.Forms;
using TLHelper.Resources;

namespace TLHelper.UI.Popups
{
    public partial class KeySelectionPopup : Form
    {
        public KeySelectionPopup()
        {
            InitializeComponent();
        }

        public Keys SelectedKey = Keys.None;

        private void Label1_Enter(object sender, EventArgs e)
        {
            ActiveControl = this;
        }

        // CREATE WINDOW SHADOW
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void KeySelectionPopup_KeyDown(object sender, KeyEventArgs e)
        {
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
                    SelectedKey = key;
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void KeySelectionPopup_MouseClick(object sender, MouseEventArgs e)
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
                    SelectedKey = key;
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
