namespace TLHelper.HotKeys
{
    public class HotKey
    {
        public Key CurrentKey { get; set; }

        public bool IsCtrl { get; set; }
        public bool IsShift { get; set; }
        public bool IsAlt { get; set; }

        public HotKey(Key key, bool ctrl, bool shift, bool alt)
        {
            CurrentKey = key;
            IsCtrl = ctrl;
            IsShift = shift;
            IsAlt = alt;
        }

        public bool IsMouse => CurrentKey.IsMouse;

        public int AddonKeys => (IsAlt ? 1 : 0) + (IsCtrl ? 2 : 0) + (IsShift ? 4 : 0);

        public string GetString()
        {
            string s = "";
            if (IsCtrl) s += "Ctrl + ";
            if (IsShift) s += "Shift + ";
            if (IsAlt) s += "Alt + ";
            s += CurrentKey.GetString();

            return s;
        }

    }
}
