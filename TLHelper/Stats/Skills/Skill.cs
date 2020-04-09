using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TLHelper.Stats.Skills
{
    class Skill
    {

        public delegate bool IsAvailable(bool isMouse, Color pxl);

        public string[] id;
        public string key { get; set; }
        public bool active { get; set; }
        public int slot { get; set; }

        public IsAvailable CanPress;

        public Skill(PictureBox icon, TextBox key, CheckBox active, ComboBox slot, String name, String id, IsAvailable canPress)
        {
            this.id = id.Split(new char[] { '_' }, 2);
            this.key = key.Text = Form1.Settings[this.id[0] + "_key_" + this.id[1]];
            this.active = active.Checked = bool.Parse(Form1.Settings[this.id[0] + "_active_" + this.id[1]]);
            this.slot = slot.SelectedIndex = int.Parse(Form1.Settings[this.id[0] + "_slot_" + this.id[1]]);
            CanPress = canPress;

            new ToolTip().SetToolTip(icon, name);

            SkillBar.RegisterSkill(this, name);
        }

        public void SaveSettings()
        {
            Form1.Settings[this.id[0] + "_key_" + this.id[1]] = this.key;
            Form1.Settings[this.id[0] + "_active_" + this.id[1]] = this.active.ToString();
            Form1.Settings[this.id[0] + "_slot_" + this.id[1]] = this.slot.ToString();
        }

        public bool CompareColor(Color c)
        {
            if (c.A != 255) return false;
            if (c.R < 27 || c.R > 29) return false;
            if (c.G < 29 || c.G > 33) return false;
            if (c.B < 24 || c.B > 28) return false;
            return true;
        }

        public bool IsActive => active && key != "";

        public int SkillSlot => slot;

        public (bool isMouse, Keys key, string button) GetKey()
        {
            string kString = key;
            bool success = char.TryParse(s: kString, out char c);
            if (!success)
            {
                switch(kString.ToLower())
                {
                    case "lmb":
                        return (true, Keys.None, "lmb");
                    case "rmb":
                        return (true, Keys.None, "rmb");
                    case "space":
                        return (false, Keys.Space, "");
                    case "enter":
                        return (false, Keys.Enter, "");
                    case "left":
                        return (false, Keys.Left, "");
                    case "right":
                        return (false, Keys.Right, "");
                    case "up":
                        return (false, Keys.Up, "");
                    case "down":
                        return (false, Keys.Down, "");
                    default:
                        return (false, Keys.None, null);
                }
            }
            else
            {
                return (false, (Keys)char.ToUpper(c), "");
            }
        }

    }
}
