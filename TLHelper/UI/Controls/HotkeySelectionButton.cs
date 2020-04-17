using System;
using System.Windows.Forms;
using TLHelper.HotKeys;
using TLHelper.UI.Popups;

namespace TLHelper.UI.Controls
{
    public class HotkeySelectionButton : Button
    {
        public delegate void SelectedKeyChange(HotKey newKey);

        public HotKey Key { get; private set; }
        public SelectedKeyChange KeyChange { get; private set; }
        public HotkeySelectionButton(HotKey key, SelectedKeyChange change)
        {
            Key = key;
            Text = key.GetString();
            Click += SelectKey;
            KeyChange = change;
        }

        public void SetKey(HotKey key) => Key = key;
        private void SelectKey(object sender, EventArgs e)
        {
            HotkeySelectionPopup hksp = new HotkeySelectionPopup();
            if (hksp.ShowDialog() == DialogResult.OK)
            {
                var Key = hksp.SelectedHotKey;
                this.Key = Key;
                Text = Key.GetString();
                KeyChange(this.Key);
            }
        }

    }
}
