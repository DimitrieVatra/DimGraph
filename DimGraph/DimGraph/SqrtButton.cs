using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DimGraph
{
    class SqrtButton:OwnForeColorButton
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            this.Text = null;
            int XAlign=-8, YAlign=-5;
            Size = new System.Drawing.Size(86, 71);
            base.OnPaint(pevent);
            System.Drawing.Point[] Pts = new System.Drawing.Point[4];
            Pts[0] = new System.Drawing.Point(35 + XAlign, 46 + YAlign);
            Pts[1] = new System.Drawing.Point(40 + XAlign, 46 + YAlign);
            Pts[2] = new System.Drawing.Point(47 + XAlign, 60 + YAlign);
            Pts[3] = new System.Drawing.Point(62 + XAlign, 20 + YAlign);
            if(this.Enabled)
            {
                pevent.Graphics.DrawLines(new System.Drawing.Pen(System.Drawing.Color.FromArgb(this.ForeColor.R + 20, this.ForeColor.G + 20, this.ForeColor.B + 20), 4), Pts);
                pevent.Graphics.DrawLines(new System.Drawing.Pen(this.ForeColor,4), Pts);
            }
            else
            {
                pevent.Graphics.DrawLines(new System.Drawing.Pen(this.DisableTextColor, 4), Pts);
            }
        }
    }
}
