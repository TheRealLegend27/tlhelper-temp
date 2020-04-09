using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TLHelper
{
    public partial class HotkeySelector : Form
    {
        public HotkeySelector()
        {
            InitializeComponent();
        }

        public bool ctrl = false, shift = false, alt = false;
        public string character = "";
        public Keys key = Keys.None;

        private void button1_Click(object sender, EventArgs e)
        {
            switch (character)
            {
                case "Space":
                    key = Keys.Space;
                    break;
                case "Enter":
                    key = Keys.Enter;
                    break;
                case "F1":
                    key = Keys.F1;
                    break;
                case "F2":
                    key = Keys.F2;
                    break;
                case "F3":
                    key = Keys.F3;
                    break;
                case "F4":
                    key = Keys.F4;
                    break;
                case "F5":
                    key = Keys.F5;
                    break;
                case "F6":
                    key = Keys.F6;
                    break;
                case "F7":
                    key = Keys.F7;
                    break;
                case "F8":
                    key = Keys.F8;
                    break;
                case "F9":
                    key = Keys.F9;
                    break;
                case "F10":
                    key = Keys.F10;
                    break;
                case "F11":
                    key = Keys.F11;
                    break;

                default:
                    char c;
                    bool success = char.TryParse(character, out c);
                    if (success)
                    {
                        key = (Keys)char.ToUpper(c);
                    }
                    else
                    {
                        MessageBox.Show("Dieser Key kann nicht für Hotkeys verwendet werden!");
                    }
                    break;
            }
            if (key != Keys.None)
                this.DialogResult = DialogResult.OK;
        }

        private void UpdateLabel()
        {
            label1.Text = (ctrl ? "CTRL + " : "") + (shift ? "SHIFT + " : "") + (alt ? "ALT + " : "") + character;
        }

        private void HotkeySelector_KeyDown(object sender, KeyEventArgs e)
        {
            String kc = e.KeyCode.ToString();
            if (kc == "ControlKey")
            {
                this.ctrl = true;
            }
            else if (kc == "ShiftKey")
            {
                this.shift = true;
            }
            else if (kc == "Menu")
            {
                this.alt = true;
            }
            else
            {
                this.character = kc;
            }
            UpdateLabel();
        }
    }
}
