using System;
using System.Windows.Forms;

namespace TLHelper.UI.Popups
{
    public partial class LoginPopup : Form
    {

        public string license = "";

        public LoginPopup()
        {
            InitializeComponent();
        }

        private void BLoginClicked(object sender, EventArgs e) => TryLogin();

        private void TryLogin()
        {
            if (tbLicense.Text.Length <= 0)
            {
                DisplayError("License");
            }
            else
            {
                license = tbLicense.Text;
                DialogResult = DialogResult.OK;
            }
        }

        private void DisplayError(string field) => MessageBox.Show(string.Format("The Field {0} can not be empty!", field), "Error!");

        private void TextField_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                TryLogin();
        }

        private void LLGetALicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(EnvironmentVariables.WEBSITE_LINK);
        }
    }
}
