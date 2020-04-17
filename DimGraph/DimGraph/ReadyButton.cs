using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DimGraph
{
    class ReadyButton:System.Windows.Forms.Button
    {
        bool darktheme;
        public bool DarkTheme { get { return darktheme; } set { darktheme = value; this.Refresh(); } }
        bool MouseIn = false;
        System.Drawing.Color ClearColor;
        System.Drawing.SolidBrush BackBrush, MarginBrush;
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            DarkTheme = Properties.Settings.Default.DarkTheme;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            
            if (DarkTheme)
            {
                ClearColor = System.Drawing.Color.Black;
                if (MouseIn)
                {
                    BackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255 - 51, 255 - 51, 255 - 51));
                    MarginBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255 - 65, 255 - 65, 255 - 65));
                }
                else
                {

                    BackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255 - 102, 255 - 102, 255 - 102));
                    MarginBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255 - 113, 255 - 113, 255 - 113));
                }
            }
            else
            {
                ClearColor = System.Drawing.Color.White;
                if (MouseIn)
                {
                    BackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(51, 51, 51));
                    MarginBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(65, 65, 65));
                }
                else
                {
                    BackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(102,102,102));
                    MarginBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(113,113,113));
                }
            }
            //base.OnPaint(pevent);
            pevent.ClipRectangle.Inflate(23, 21);
            pevent.Graphics.Clear(ClearColor);
            int TransX = 6, TransY = 4;
            //15 x 11
            // \0-6 , 5-10, 15,0
            pevent.Graphics.DrawLine(new System.Drawing.Pen(BackBrush.Color, 1), new System.Drawing.Point(1 + TransX, 5 + TransY), new System.Drawing.Point(6 + TransX, 10 + TransY));
            pevent.Graphics.DrawLine(new System.Drawing.Pen(BackBrush.Color, 1), new System.Drawing.Point(1 + TransX, 6 + TransY), new System.Drawing.Point(6 + TransX, 11 + TransY));
            pevent.Graphics.DrawLine(new System.Drawing.Pen(BackBrush.Color, 1), new System.Drawing.Point(0 + TransX, 6 + TransY), new System.Drawing.Point(5 + TransX, 11 + TransY));
            pevent.Graphics.DrawLine(new System.Drawing.Pen(BackBrush.Color, 1), new System.Drawing.Point(0 + TransX, 7 + TransY), new System.Drawing.Point(5 + TransX, 12 + TransY));
            // /
            pevent.Graphics.DrawLine(new System.Drawing.Pen(MarginBrush.Color, 1), new System.Drawing.Point(15 + TransX, 0 + TransY), new System.Drawing.Point(6 + TransX, 9 + TransY));
            pevent.Graphics.DrawLine(new System.Drawing.Pen(BackBrush.Color, 1), new System.Drawing.Point(16 + TransX, 0 + TransY), new System.Drawing.Point(6 + TransX, 10 + TransY));
            pevent.Graphics.DrawLine(new System.Drawing.Pen(BackBrush.Color, 1), new System.Drawing.Point(16 + TransX, 1 + TransY), new System.Drawing.Point(6 + TransX, 11 + TransY));
            pevent.Graphics.DrawLine(new System.Drawing.Pen(BackBrush.Color, 1), new System.Drawing.Point(15 + TransX, 1 + TransY), new System.Drawing.Point(5 + TransX, 11 + TransY));
            pevent.Graphics.DrawLine(new System.Drawing.Pen(BackBrush.Color, 1), new System.Drawing.Point(17 + TransX, 1 + TransY), new System.Drawing.Point(6 + TransX, 12 + TransY));
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            MouseIn = true;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseIn = false;
        }
    }
}
