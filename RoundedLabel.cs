using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MPHospitalRecordsSystem
{
    internal class RoundedLabel : Label
    {
        public int BorderRadius { get; set; } = 20;
        public Color BorderColor { get; set; } = Color.Black;
        public int BorderSize { get; set; } = 0; // default = no border

        protected override void OnPaint(PaintEventArgs e)
        {
            // Smooth edges
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Create rounded path
            GraphicsPath path = new GraphicsPath();
            int radius = BorderRadius;
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(Width - radius, Height - radius, radius, radius, 0, 90);
            path.AddArc(0, Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            // Clip region so image/text are inside rounded corners
            this.Region = new Region(path);

            // Fill background (BackColor)
            using (SolidBrush brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            // Draw Image (if set)
            if (this.Image != null)
            {
                Rectangle imgRect = new Rectangle(0, 0, this.Width, this.Height);
                e.Graphics.SetClip(path); // clip to rounded shape
                e.Graphics.DrawImage(this.Image, imgRect);
                e.Graphics.ResetClip();
            }

            // Draw Text
            TextRenderer.DrawText(
                e.Graphics,
                this.Text,
                this.Font,
                this.ClientRectangle,
                this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );

            // Draw Border (only if BorderSize > 0)
            if (BorderSize > 0)
            {
                using (Pen pen = new Pen(BorderColor, BorderSize))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}
