using System;
using System.Drawing;
using System.Windows.Forms;
using TLHelper.HotKeys;
using TLHelper.UI.Popups;

namespace TLHelper.UI.Controls
{
    public class HotkeySelectionButton : Button
    {
        public delegate void SelectedKeyChange(HotKey newKey);

        private bool _isHovering;

        public HotKey Key { get; private set; }
        public SelectedKeyChange KeyChange { get; private set; }
        public HotkeySelectionButton(HotKey key, SelectedKeyChange change)
        {
            Key = key;
            Text = key.GetString();
            Click += SelectKey;
            KeyChange = change;

            Font = Theme.Fonts.H4;

            Margin = new Padding(0);
            DoubleBuffered = true;
            MouseEnter += (sender, e) =>
            {
                _isHovering = true;
                Invalidate();
            };
            MouseLeave += (sender, e) =>
            {
                _isHovering = false;
                Invalidate();
            };
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

        private Color _buttonColor = Theme.Accent;
        private Color _hoverButtonColor = Theme.AccentLight;
        private Color _borderColor = Theme.Accent;
        private Color _textColor = Theme.Background;
        private Color _hoverTextColor = Theme.Background;
        private int _borderWidth = 1;

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Brush brush = new SolidBrush(_borderColor);
            g.FillRectangle(brush, new Rectangle(0, 0, Width, Height));
            brush.Dispose();

            brush = new SolidBrush(_isHovering ? _hoverButtonColor : _buttonColor);
            g.FillRectangle(brush, new Rectangle(_borderWidth, _borderWidth, Width - _borderWidth * 2, Height - _borderWidth * 2));
            brush.Dispose();

            brush = new SolidBrush(_isHovering ? _hoverTextColor : _textColor);

            //Button Text
            SizeF stringSize = g.MeasureString(Text, Font);
            g.DrawString(Text, Font, brush, (Width - stringSize.Width) / 2, (Height - stringSize.Height) / 2);
        }

    }
}
