using System;
using System.Drawing;
using System.Windows.Forms;
using TLHelper.HotKeys;
using TLHelper.UI.Popups;

namespace TLHelper.UI.Controls
{
    public class KeySelectionButton : Button
    {
        public delegate void SelectedKeyChange(Key newKey);

        private bool _isHovering;

        public Key Key { get; private set; }
        public SelectedKeyChange KeyChange { get; private set; }
        public KeySelectionButton(Key key, SelectedKeyChange change)
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

        private readonly Color _buttonColor = Theme.Accent;
        private readonly Color _hoverButtonColor = Theme.AccentLight;
        private readonly Color _borderColor = Theme.Accent;
        private readonly Color _textColor = Theme.Background;
        private readonly Color _hoverTextColor = Theme.Background;
        private readonly int _borderWidth = 1;

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
