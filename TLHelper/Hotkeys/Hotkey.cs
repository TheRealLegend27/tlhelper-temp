using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TLHelper.Hotkeys
{
    class Hotkey
    {
        public bool ctrl, shift, alt;
        public char cKey;
        public Keys kKey;
        public bool isChar;

        public Hotkey(bool _ctrl, bool _shift, bool _alt, char _key)
        {
            ctrl = _ctrl;
            shift = _shift;
            alt = _alt;
            cKey = _key;
            isChar = true;
        }
        public Hotkey(bool _ctrl, bool _shift, bool _alt, Keys _key)
        {
            ctrl = _ctrl;
            shift = _shift;
            alt = _alt;
            kKey = _key;
            isChar = false;
        }

        public int GetAddonKeys()
        {
            return (alt ? 1 : 0) + (ctrl ? 2 : 0) + (shift ? 4 : 0);
        }

        public Keys GetKey()
        {
            return isChar ? (Keys)char.ToUpper(cKey) : kKey;
        }

        public bool IsMouse() => isChar ? false : kKey == Keys.LButton || kKey == Keys.RButton || kKey == Keys.MButton || kKey == Keys.XButton1 || kKey == Keys.XButton2;

    }
}
