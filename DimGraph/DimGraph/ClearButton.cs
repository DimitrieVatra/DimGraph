using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DimGraph
{
    class ClearButton:System.Windows.Forms.Button
    {
        System.Drawing.Color ClearColor;
        System.Drawing.SolidBrush Brush;
        bool darktheme;
        public bool DarkTheme { get { return darktheme; } set { darktheme = value; this.Refresh(); } }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            DarkTheme = Properties.Settings.Default.DarkTheme;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            //MessageBox.Show("aaa");
            //base.OnPaint(pevent);
            if (DarkTheme)
            {
                ClearColor = System.Drawing.Color.Black;
                if (MouseIn)
                {
                    Brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255 - 51, 255 - 51, 255 - 51));
                }
                else
                {
                    Brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255 - 102, 255 - 102, 255 - 102));
                }
            }
            else
            {
                ClearColor = System.Drawing.Color.White;
                if (MouseIn)
                {
                    Brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(51, 51, 51));
                }
                else
                {
                    Brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(102, 102, 102));
                }
            }/*
            System.Drawing.Brush Brush;
            if (MouseIn)
            {
                Brush = System.Drawing.Brushes.Black;
            }
            else Brush = System.Drawing.Brushes.Gray;
            */
          //  pevent.ClipRectangle.Inflate(new System.Drawing.Size(23,23));
            pevent.Graphics.Clear(ClearColor);
             System.Drawing.Point[] pt = new System.Drawing.Point[4];
            int coboram=1, translatam = 14,departare=2;
            pt[0] = new System.Drawing.Point(0 + translatam + departare, 2 + coboram + departare);
            pt[1] = new System.Drawing.Point(2 + translatam + departare, 0 + coboram + departare);
             pt[3] = new System.Drawing.Point(17 + translatam - departare, 19 + coboram - departare);
             pt[2] = new System.Drawing.Point(19 + translatam - departare, 17 + coboram - departare);
            pevent.Graphics.FillPolygon(Brush, pt);
            pt[0] = new System.Drawing.Point(0 + translatam + departare, 17 + coboram - departare);
            pt[1] = new System.Drawing.Point(2 + translatam + departare, 19 + coboram - departare);
            pt[3] = new System.Drawing.Point(17 + translatam - departare, 0 + coboram + departare);
            pt[2] = new System.Drawing.Point(19 + translatam - departare, 2 + coboram + departare);
            pevent.Graphics.FillPolygon(Brush, pt);
        }
        bool MouseIn = false;
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
