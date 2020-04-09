using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TLHelper.Stats.Skills
{
    class PotionSkill
    {
        public delegate bool IsAvailable(bool isMouse, Color pxl);
        public string key { get; set; }
        public bool active { get; set; }
        public IsAvailable CanPress;

        public PotionSkill(PictureBox pb, TextBox tb, CheckBox cb)
        {
            key = tb.Text = Form1.Settings["general_key_potion"];
            active = cb.Checked = bool.Parse(Form1.Settings["general_active_potion"]);
            new ToolTip().SetToolTip(pb, "Potion");
            CanPress = AvailableFunction.Potion;
        }

        public void ProcessSkill()
        {
            if (!ScreenTools.IsDiabloFocused()) return;
            if (!ScreenTools.IsInRift()) return;
            if (ScreenTools.IsPorting()) return;

            if (IsActive && CanPress(false, ScreenTools.GetPixelColor(Coords.Potion50.x, Coords.Potion50.y).Item1))
            {
                (bool isMouse, Keys key, string button) = GetKey();
                SendKeys.SendWait((char)key + "");
            }
        }

        public bool IsActive => active && key != "";

        public void SaveSettings()
        {
            Form1.Settings["general_key_potion"] = key;
            Form1.Settings["general_active_potion"] = active.ToString();
        }

        public (bool isMouse, Keys key, string button) GetKey()
        {
            string kString = key;
            bool success = char.TryParse(s: kString, out char c);
            if (!success)
            {
                switch (kString.ToLower())
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
