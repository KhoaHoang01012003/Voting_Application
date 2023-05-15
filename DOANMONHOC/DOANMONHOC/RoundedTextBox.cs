using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOANMONHOC
{
    public class RoundedTextBox : TextBox
    {
        private const int borderRadius = 120;
        private const int borderWidth = 2;
        public RoundedTextBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Pen borderPen = new Pen(Color.Black); // Màu đường viền, có thể tùy chỉnh theo ý muốn

            // Vẽ góc bo bằng đường cong
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawArc(borderPen, rect.Left, rect.Top, borderRadius * 2, borderRadius * 2, 180, 90);
            g.DrawLine(borderPen, rect.Left + borderRadius, rect.Top, rect.Right - borderRadius, rect.Top);
            g.DrawArc(borderPen, rect.Right - borderRadius * 2, rect.Top, borderRadius * 2, borderRadius * 2, 270, 90);
            g.DrawLine(borderPen, rect.Right, rect.Top + borderRadius, rect.Right, rect.Bottom - borderRadius);
            g.DrawArc(borderPen, rect.Right - borderRadius * 2, rect.Bottom - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            g.DrawLine(borderPen, rect.Left + borderRadius, rect.Bottom, rect.Right - borderRadius, rect.Bottom);
            g.DrawArc(borderPen, rect.Left, rect.Bottom - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            g.DrawLine(borderPen, rect.Left, rect.Top + borderRadius, rect.Left, rect.Bottom - borderRadius);
        }
    }
}
