using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DimGraph
{
    class OwnForeColorButton:System.Windows.Forms.Button
    {
        System.Drawing.Color disableTextColor;
        public System.Drawing.Color DisableTextColor { get{ return disableTextColor; } set{ disableTextColor = value;this.Refresh(); } }
        bool darktheme;
        public bool DarkTheme { get { return darktheme; } set { darktheme = value; DisableTextColor = System.Drawing.Color.FromArgb(System.Drawing.SystemColors.Control.R / 3, System.Drawing.SystemColors.Control.G / 3, System.Drawing.SystemColors.Control.B / 3); this.Refresh(); } }
        bool directly;
        public bool Directly { get { return directly; } set { directly = value; } }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            DisableTextColor = System.Drawing.Color.FromArgb(System.Drawing.SystemColors.Control.R / 3, System.Drawing.SystemColors.Control.G / 3, System.Drawing.SystemColors.Control.B / 3);
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            int XAlign = 0, YAlign = 1;
            if (Text == "π") { YAlign = 4; XAlign = -1; }
            else if (Text == "e") { YAlign = 2; XAlign = -1; }
            else if(Text == "()") { YAlign = 3; }
            if (Enabled || !DarkTheme)
            {
                base.OnPaint(pevent);
            }
            else
            {
                base.OnPaint(pevent);
                System.Drawing.SizeF sf = pevent.Graphics.MeasureString(Text, this.Font, this.Width);
                System.Drawing.Point ThePoint = new System.Drawing.Point();
                ThePoint.X = (int)((this.Width / 2) - (sf.Width / 2)) + XAlign;
                ThePoint.Y = (int)((this.Height / 2) - (sf.Height / 2)) + YAlign;
                System.Drawing.Brush BR = new System.Drawing.SolidBrush(DisableTextColor);
                System.Drawing.Font F = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size);
                pevent.Graphics.DrawString(Text, F,  BR, ThePoint);
            }
        }
    }
}
