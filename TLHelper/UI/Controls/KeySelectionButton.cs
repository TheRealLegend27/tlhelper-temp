using System;
using System.Windows.Forms;
using TLHelper.HotKeys;
using TLHelper.UI.Popups;

namespace TLHelper.UI.Controls
{
    public class KeySelectionButton : Button
    {
        public delegate void SelectedKeyChange(Key newKey);

        public Key Key { get; private set; }
        public SelectedKeyChange KeyChange { get; private set; }
        public KeySelectionButton(Key key, SelectedKeyChange change)
        {
            Key = key;
            Text = key.GetString();
            Click += SelectKey;
            KeyChange = change;
        }

        public void SetKey(Key key)
        {
            Key = key;
            Text = key.GetString();
        }

        public void SetKey(Keys key) => SetKey(new Key(key));
        private void SelectKey(object sender, EventArgs e)
        {
            KeySelectionPopup ksp = new KeySelectionPopup();
            if (ksp.ShowDialog() == DialogResult.OK)
            {
                var Key = ksp.SelectedKey;
                SetKey(Key);
                KeyChange(this.Key);
            }
        }

    }
}
