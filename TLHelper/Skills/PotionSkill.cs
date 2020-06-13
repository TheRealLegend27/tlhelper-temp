using System.Windows.Forms;
using System.Xml;
using TLHelper.HotKeys;
using TLHelper.SysCom;
using TLHelper.UI.Controls;

namespace TLHelper.Skills
{
    class PotionSkill
    {
        private Key _key;
        private Key Key
        {
            get { return _key; }
            set
            {
                _key = value;
                if (KeyControl != null)
                    KeyControl.Text = value.GetString();
            }
        }

        private bool _active;
        private bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                if (ActiveControl != null)
                    ActiveControl.SelectedIndex = value ? 1 : 0;
            }
        }

        private ComboBox ActiveControl = null;
        private KeySelectionButton KeyControl = null;

        public PotionSkill(Key key, bool active)
        {
            this.Key = key;
            this.Active = active;
        }

        public void Process()
        {
            if (IsActive && AvailableFunctions.Potion(0, ScreenTools.GetPixelColor(Coords.Coords.Potion50.x, Coords.Coords.Potion50.y).Item1))
            {
                if (Key.IsMouse)
                {
                    if (Key.CurrentKey == Keys.LButton)
                        HardwareRobot.DoLeftClick();
                    else if (Key.CurrentKey == Keys.RButton)
                        HardwareRobot.DoRightClick();
                }
                else
                {
                    HardwareRobot.PressKey((char)Key.CurrentKey);
                }
            }
        }

        public void SetControls(ComboBox ac, KeySelectionButton kc)
        {
            ActiveControl = ac;
            KeyControl = kc;

            ac.SelectedIndex = Active ? 1 : 0;
            kc.Text = Key.GetString();
        }

        public void SetXmlAttribs(XmlElement e)
        {
            e.SetAttribute("key", ((int)Key.CurrentKey).ToString());
            e.SetAttribute("active", Active.ToString());
        }

        public void SetKey(Key key) => this.Key = key;
        public void SetActive(bool active) => this.Active = active;

        public bool IsActive => Active && Key.IsSet;
    }
}
