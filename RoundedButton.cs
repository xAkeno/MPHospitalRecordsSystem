using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPHospitalRecordsSystem
{
    public class RoundedButton : Button
    {
        public int BorderRadius { get; set; } = 20;
        public Color BorderColor { get; set; } = Color.Black;
        public int BorderSize { get; set; } = 2;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            GraphicsPath path = new GraphicsPath();
            int radius = BorderRadius;

            // Rounded rectangle path
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            // Fill background
            using (SolidBrush brush = new SolidBrush(this.BackColor))
                g.FillPath(brush, path);

            // Draw border
            using (Pen pen = new Pen(BorderColor, BorderSize))
                g.DrawPath(pen, path);

            // Draw text
            TextRenderer.DrawText(
                g,
                this.Text,
                this.Font,
                rect,
                this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );

            // Set clickable region
            this.Region = new Region(path);
        }
    }
}
