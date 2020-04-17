using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DimGraph
{
    class KeyboardButton : System.Windows.Forms.Button
    {
        bool darktheme;
        public bool DarkTheme { get { return darktheme; } set { darktheme = value; this.Refresh(); } }
        System.Drawing.SolidBrush BackgroundBrush,MarginBrush,KeyBrush;
        System.Drawing.Color ClearColor;
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
                    KeyBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                    BackgroundBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255 - 51, 255 - 51, 255 - 51));
                    MarginBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255 - 65, 255 - 65, 255 - 65));
                }
                else
                {
                    KeyBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                    BackgroundBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255 - 102, 255 - 102, 255 - 102));
                    MarginBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255 - 113, 255 - 113, 255 - 113));
                }
            }
            else
            {
                ClearColor = System.Drawing.Color.White;
                if (MouseIn)
                {
                    KeyBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                    BackgroundBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(51, 51, 51));
                    MarginBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(65, 65, 65));
                }
                else
                {
                    KeyBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                    BackgroundBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(102, 102, 102));
                    MarginBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(113, 113, 113));
                }
            }
            //base.OnPaint(pevent);
            pevent.ClipRectangle.Inflate(new System.Drawing.Size(20, 21));
            pevent.Graphics.Clear(ClearColor);
            int transX = 10, transY = -1;
            pevent.Graphics.FillRectangle(BackgroundBrush, new System.Drawing.Rectangle(new System.Drawing.Point(0 + transX, 6 + transY), new System.Drawing.Size(19, 11)));
            System.Drawing.Rectangle rectangle;
            rectangle = new System.Drawing.Rectangle(transX, 6 + transY, 1, 1);
            pevent.Graphics.FillRectangle(MarginBrush,rectangle);
            rectangle = new System.Drawing.Rectangle(transX, 6 + 10 + transY, 1, 1);
            pevent.Graphics.FillRectangle(MarginBrush, rectangle);
            rectangle = new System.Drawing.Rectangle(18 + transX, 6 + transY, 1, 1);
            pevent.Graphics.FillRectangle(MarginBrush, rectangle);
            rectangle = new System.Drawing.Rectangle(18 + transX, 6 + 10 + transY, 1, 1);
            pevent.Graphics.FillRectangle(MarginBrush, rectangle);
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<8;j++)
                {
                    rectangle = new System.Drawing.Rectangle(2+ 2 * j + transX, 8 + 2 * i + transY , 1, 1);
                    pevent.Graphics.FillRectangle(KeyBrush, rectangle);
                }
            }
            rectangle = new System.Drawing.Rectangle(4+transX,14+transY,11,1);
            pevent.Graphics.FillRectangle(KeyBrush, rectangle);
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
