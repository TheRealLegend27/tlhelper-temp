using System.Windows.Forms;
using TLHelper.Resources;

namespace TLHelper.HotKeys
{
    public class Key
    {
        public Keys CurrentKey { get; set; }

        public Key(Keys key)
        {
            CurrentKey = key;
        }

        public string GetString()
        {
            return GlobalData.AllowedKeys.GetValue(CurrentKey);
        }

        public bool IsMouse => CurrentKey == Keys.LButton || CurrentKey == Keys.RButton || CurrentKey == Keys.MButton ||
            CurrentKey == Keys.XButton1 || CurrentKey == Keys.XButton2;

        public bool IsKey => !IsMouse;

        public bool IsSet => CurrentKey != Keys.None;

        public bool IsUnset => !IsSet;

        public MouseButtons AsMouseButton()
        {
            if (!IsMouse) return MouseButtons.None;
            if (CurrentKey == Keys.LButton) return MouseButtons.Left;
            if (CurrentKey == Keys.RButton) return MouseButtons.Right;
            if (CurrentKey == Keys.MButton) return MouseButtons.Middle;
            if (CurrentKey == Keys.XButton1) return MouseButtons.XButton1;
            if (CurrentKey == Keys.XButton2) return MouseButtons.XButton2;
            return MouseButtons.None;
        }

    }
}
