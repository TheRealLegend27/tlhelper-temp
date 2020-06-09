using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TLHelper.UI.Controls
{
    class SidebarButton : Button
    {
        private Color _buttonColor = Theme.Accent;
        private Color _onHoverButtonColor = Theme.AccentLight;
        private Color _textColor = Theme.Background;

        private Image _ChevronIcon;
        private Image _SpecialIcon;
        public Image ChevronIcon { get => _ChevronIcon; set { _ChevronIcon = ScaleImage(value, new Size(20, 20)); } }
        public Image SpecialIcon { get => _SpecialIcon; set { _SpecialIcon = ScaleImage(value, new Size(20, 20)); } }

        private bool _isHovering;
        private bool _Active;
        public bool Active { get => _Active; set { _Active = value; Invalidate(); } }

        public SidebarButton()
        {
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

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Brush brush = new SolidBrush(_isHovering || _Active ? _onHoverButtonColor : _buttonColor);
            g.FillRectangle(brush, new Rectangle(-1, -1, Width+1, Height+1));

            brush.Dispose();
            brush = new SolidBrush(_textColor);

            SizeF stringSize = g.MeasureString(Text, Font);
            g.DrawString(Text, Font, brush, 10 + _SpecialIcon.Width + 10, (Height - stringSize.Height) / 2);

            if (_ChevronIcon != null)
            {
                g.DrawImage(_ChevronIcon, new Point(Width -  _ChevronIcon.Width - 10, (Height - _ChevronIcon.Height) / 2));
            }
            if (_SpecialIcon != null)
            {
                g.DrawImage(_SpecialIcon, new Point(10, (Height - _ChevronIcon.Height) / 2));
            }
        }

        private Image ScaleImage(Image img, Size size)
        {
            return new Bitmap(img, size);
        }

    }
}
