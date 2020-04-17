using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
namespace DimGraph
{
    public partial class Form1 : Form
    {
        public Form1(string apel)
        {
            VirgulaSistem = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            AxesPen = new Pen(Properties.Settings.Default.AxesColor, 2);
            ClearColor = new Color(); ClearColor = Properties.Settings.Default.ClearColor;
            GraphicPen = new Pen(DimGraph.Properties.Settings.Default.GraphicColor, DimGraph.Properties.Settings.Default.GraphicWidth);
            GridPen = new Pen(DimGraph.Properties.Settings.Default.GridColor, 1);
            InitializeComponent();
            darkThemeToolStripMenuItem.Checked = !DimGraph.Properties.Settings.Default.DarkTheme;//! pentru ca ChangeDarkTheme e a doua negare, deci revine
            ChangeDarkTheme(true);
        }
        float x; string a;
        string asave; bool drawcoord;

        int m = 1079, n = 638;
        bool gata = false;
        void InchideParantezeNeinchise()
        {
            string s = richTextBox1.Text;
            int nrpar = 0;
            while (s.IndexOf('(') != -1) { nrpar++; s = s.Remove(s.IndexOf('('), 1); }
            while (s.IndexOf(')') != -1) { nrpar--; s = s.Remove(s.IndexOf(')'), 1); }
            s = null;
            for (int i = 0; i < nrpar; i++) s += ')'; richTextBox1.Text += s;
        }

        private void Window_Shown(object sender, EventArgs e)
        {
            Plot(2);
        }
        
        bool EnableReady = false;
        void ready()
        {
            if (EnableReady)
            {
                InchideParantezeNeinchise();
                if (richTextBox1.Text.Length > 0)
                {
                    gata = true;
                    if (groupBox1.Visible)
                    {
                        SetKeyboardVisibility(0);
                    }
                    else
                    {
                        Function = new Functie(richTextBox1.Text);
                    }
                }
            }
        }
        Functie function;
        bool EnableRichTextBoxAutomation = true;
        Functie Function
        {
            get { return function; }
            set
            {
                function = value;
                if (groupBox1.Visible)
                {
                    SetKeyboardVisibility(0);
                }
                EnableRichTextBoxAutomation = false;
                richTextBox1.Text = function.Corp;
                EnableRichTextBoxAutomation = true;
                gata = true;
                Plot(2);
            }
        }
        bool valoare = true, functie = true, operatie = true, inchidere = true, deschidere = true, xstring = true, scdr = true;
        bool virgula_existenta = false;
        int nmn = 0;
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (EnableRichTextBoxAutomation == false) return;
            gata = false;
            if (richTextBox1.Text.Length != 0)
            {

                richTextBox1.Text = richTextBox1.Text.Replace('.', VirgulaSistem);
                if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 's' && lungime < richTextBox1.Text.Length) richTextBox1.Text += "in(";
                else if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 'c' && lungime < richTextBox1.Text.Length) richTextBox1.Text += "os(";
                else if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 't' && lungime < richTextBox1.Text.Length) richTextBox1.Text += "g(";
                else if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 'g' && lungime < richTextBox1.Text.Length) { richTextBox1.Text = richtextboxsave; richTextBox1.Text += "ctg("; }
                else if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 'l' && lungime < richTextBox1.Text.Length) richTextBox1.Text += "n(";
                else if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 'S' && lungime < richTextBox1.Text.Length) { richTextBox1.Text = richtextboxsave; richTextBox1.Text += "arcsin("; }
                else if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 'C' && lungime < richTextBox1.Text.Length) { richTextBox1.Text = richtextboxsave; richTextBox1.Text += "arccos("; }
                else if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 'T' && lungime < richTextBox1.Text.Length) { richTextBox1.Text = richtextboxsave; richTextBox1.Text += "arctg("; }
                else if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 'G' && lungime < richTextBox1.Text.Length) { richTextBox1.Text = richtextboxsave; richTextBox1.Text += "arcctg("; }
                else if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 'r' && lungime < richTextBox1.Text.Length) { richTextBox1.Text = richtextboxsave; richTextBox1.Text += "sqrt("; }
                else if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 'p' && pi.Enabled == true && lungime < richTextBox1.Text.Length) { richTextBox1.Text = richtextboxsave; richTextBox1.Text += "π"; }
                while (richTextBox1.SelectionStart > 0 && (richTextBox1.Text[richTextBox1.Text.Length - 1] != '*' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '-' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '+' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '/' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '^' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '0' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '1' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '2' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '3' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '4' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '5' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '6' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '7' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '8' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '9' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != VirgulaSistem &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != '(' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != ')' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != 'e' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != 'π' &&
                     richTextBox1.Text[richTextBox1.Text.Length - 1] != 'x')) richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.Text.Length - 1);
            }
            if (richTextBox1.Text.Length > 0)
            {
                nrparanteze = 0;
                for (int i = 0; i < richTextBox1.Text.Length; i++) if (richTextBox1.Text[i] == '(') nrparanteze++; else if (richTextBox1.Text[i] == ')') nrparanteze--;
                

                if (richTextBox1.Text[richTextBox1.Text.Length - 1] >= '0' &&
                      richTextBox1.Text[richTextBox1.Text.Length - 1] <= '9' )
                {
                    nmn = 0;
                    if (lungime < richTextBox1.Text.Length)
                    {
                        if(richTextBox1.Text.Length > 1 && (inchidere || xstring) )
                        {
                            char c = richTextBox1.Text[richTextBox1.Text.Length - 1];
                            richTextBox1.Text = richtextboxsave;
                            richTextBox1.Text += '*';
                            richTextBox1.Text += c;
                        }
                        if (valoare || operatie || deschidere || functie || scdr) ;
                        else { richTextBox1.Text = richtextboxsave; goto Underline; }
                    }
                    /*if (virgula_existenta == false)
                    { virgula_existenta = true; virgula.Enabled = true; }
                    else { virgula.Enabled = false; }
                    operatii_aritmetice(true);
                    if (nrparanteze > 0) Parantezeu.Enabled = true;
                    else Parantezeu.Enabled = false;
                    x_functii_pi_e(false);
                    constante(true);
                    scadere.Enabled = true;
                    //if (nrparanteze > 0) EnableReady = false;
                     else*/
                    EnableReady = true;
                    valoare = true; functie = false; operatie = false; inchidere = false; deschidere = false; xstring = false; scdr = false;
                }
                if (richTextBox1.Text[richTextBox1.Text.Length - 1] == '+' ||
                   richTextBox1.Text[richTextBox1.Text.Length - 1] == '/' ||
                   richTextBox1.Text[richTextBox1.Text.Length - 1] == '*' ||
                   richTextBox1.Text[richTextBox1.Text.Length - 1] == '^')
                {
                    nmn = 0;
                    if (lungime < richTextBox1.Text.Length)
                    {
                        if(richTextBox1.Text.Length > 1 && (operatie || scdr || richtextboxsave[richtextboxsave.Length-1]==VirgulaSistem))
                        {
                            char c = richTextBox1.Text[richTextBox1.Text.Length - 1];
                            richTextBox1.Text = richtextboxsave.Remove(richtextboxsave.Length - 1);
                            richTextBox1.Text += c;
                        }
                        else if(richTextBox1.Text[richTextBox1.Text.Length - 1] == '/' && (richTextBox1.Text.Length>1 &&deschidere || richTextBox1.Text.Length==1))
                        {
                            richTextBox1.Text = richtextboxsave;
                            richTextBox1.Text += '1';
                            richTextBox1.Text += '/';
                        }
                        if ((xstring || valoare || inchidere) && richTextBox1.Text.Length > 1) ;
                        else { richTextBox1.Text = richtextboxsave; goto Underline; }
                    }
                   /* virgula_existenta = false;
                    virgula.Enabled = false;
                    operatii_aritmetice(false);
                    Parantezeu.Enabled = true;
                    x_functii_pi_e(true);
                    constante(true);
                    scadere.Enabled = true;*/
                    EnableReady = false;
                    valoare = false; functie = false; operatie = true; inchidere = false; deschidere = false; xstring = false; scdr = false;
                }
                if (richTextBox1.Text[richTextBox1.Text.Length - 1] == '-')
                {
                    if (lungime < richTextBox1.Text.Length)
                    {
                        nmn++;
                        if(richTextBox1.Text.Length > 1 && (richtextboxsave[richtextboxsave.Length - 1] == VirgulaSistem || richtextboxsave[richtextboxsave.Length - 1] == '+') )
                        {
                            richTextBox1.Text = richtextboxsave.Remove(richtextboxsave.Length - 1);
                            richTextBox1.Text += '-';
                        }
                        if ((xstring || deschidere || valoare || inchidere || (operatie && nmn < 3))) scadere.Enabled = true;
                        else { richTextBox1.Text = richtextboxsave; goto Underline; scadere.Enabled = false; }
                    }
                    /*virgula_existenta = false;
                    virgula.Enabled = false;
                    operatii_aritmetice(false);
                    Parantezeu.Enabled = true;
                    x_functii_pi_e(true);
                    constante(true);*/
                    EnableReady = false;
                    valoare = false; functie = false; operatie = false; inchidere = false; deschidere = false; xstring = false; scdr = true;
                }
                if (richTextBox1.Text[richTextBox1.Text.Length - 1] == '(')
                {
                    virgula_existenta = false;
                    //virgula.Enabled = false;
                    nmn = 0;
                    if (lungime < richTextBox1.Text.Length)
                    {
                        if(richTextBox1.Text.Length > 1 && (valoare || xstring || inchidere))
                        {
                            if (richTextBox1.Text.Length > 1 &&
                        (richTextBox1.Text[richTextBox1.Text.Length - 2] == 'n' ||
                        richTextBox1.Text[richTextBox1.Text.Length - 2] == 's' ||
                        richTextBox1.Text[richTextBox1.Text.Length - 2] == 'g' ||
                        richTextBox1.Text[richTextBox1.Text.Length - 2] == 't'))
                            {
                                string copy = richTextBox1.Text;
                                copy = copy.Remove(copy.Length - 1);
                                int count = 0; string f; int i = copy.Length - 1;
                                while (i >= 0 && (copy[i] == 'a' || copy[i] == 'q' || copy[i] == 'r' || copy[i] == 'c' || copy[i] == 's' || copy[i] == 'i' || copy[i] == 'n' || copy[i] == 'l' || copy[i] == 'g' || copy[i] == 't' || copy[i] == 'o'))
                                {
                                    i--;
                                    count++;
                                }
                                if (i >= 0)
                                {
                                    f = copy.Substring(copy.Length - count, count);
                                    richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.Text.Length - count - 1);
                                    richTextBox1.Text += '*';
                                    richTextBox1.Text += f + '(';
                                }
                            }
                            else
                            {
                                richTextBox1.Text = richtextboxsave;
                                    richTextBox1.Text += '*';
                                    richTextBox1.Text += '(';
                            } 
                        }
                        if (functie || operatie || deschidere || scdr) ;
                        else { richTextBox1.Text = richtextboxsave; goto Underline; }
                    }
                    EnableReady = false;
                    if (richTextBox1.Text.Length > 1 &&
                        (richTextBox1.Text[richTextBox1.Text.Length - 1] == 'n' ||//-2
                        richTextBox1.Text[richTextBox1.Text.Length - 1] == 's' ||//-2
                        richTextBox1.Text[richTextBox1.Text.Length - 1] == 'g' ||//-2
                        richTextBox1.Text[richTextBox1.Text.Length - 1] == 't'))//-2
                    {
                        /*operatii_aritmetice(false);
                        x_functii_pi_e(true);
                        Parantezeu.Enabled = true;
                        constante(true);
                        scadere.Enabled = true;*/
                        valoare = false; functie = true; operatie = false; inchidere = false; deschidere = false; xstring = false; scdr = false;
                    }
                    else
                    {
                        /*operatii_aritmetice(false);
                        x_functii_pi_e(true);
                        constante(true);
                        scadere.Enabled = true;*/
                        valoare = false; functie = false; operatie = false; inchidere = false; deschidere = true; xstring = false; scdr = false;
                    }
                }
                if (richTextBox1.Text[richTextBox1.Text.Length - 1] == ')')
                {
                    virgula_existenta = false;
                    //virgula.Enabled = false;
                    nmn = 0;
                    if (lungime < richTextBox1.Text.Length)
                    {
                        if (richTextBox1.Text.Length > 1 && richtextboxsave != null &&(richtextboxsave[richtextboxsave.Length - 1] == VirgulaSistem && nrparanteze>=0) )
                        {

                            richTextBox1.Text = richtextboxsave.Remove(richtextboxsave.Length - 1);
                            richTextBox1.Text += ')';
                        }
                            if ((xstring && nrparanteze >= 0) || (valoare && nrparanteze >= 0) || (inchidere && nrparanteze >= 0)) ;
                        else { richTextBox1.Text = richtextboxsave; goto Underline; }
                    }
                    /*operatii_aritmetice(true);
                    //if (nrparanteze <= 0) Parantezeu.Enabled = false;
                    x_functii_pi_e(false);
                    constante(false);
                    scadere.Enabled = true;
                    // if (nrparanteze > 0) EnableReady = false;
                    //else */
                    EnableReady = true;
                    valoare = false; functie = false; operatie = false; inchidere = true; deschidere = false; xstring = false; scdr = false;
                }
                if (richTextBox1.Text[richTextBox1.Text.Length - 1] == 'x' ||
                      richTextBox1.Text[richTextBox1.Text.Length - 1] == 'e' ||
                      richTextBox1.Text[richTextBox1.Text.Length - 1] == 'π')
                {
                    virgula_existenta = false;
                   // virgula.Enabled = false;
                    if (lungime < richTextBox1.Text.Length)
                    {
                        if (richTextBox1.Text.Length > 1 && (xstring || valoare || inchidere))
                        {
                            char c = richTextBox1.Text[richTextBox1.Text.Length - 1];
                            richTextBox1.Text = richtextboxsave;
                            richTextBox1.Text += '*';
                            richTextBox1.Text += c;
                        }
                        if (functie || operatie || deschidere || scdr) ;
                        else { richTextBox1.Text = richtextboxsave; goto Underline; }
                    }
                    /*x_functii_pi_e(false);
                    operatii_aritmetice(true);
                    nmn = 0;
                    if (nrparanteze > 0) Parantezeu.Enabled = true;
                    constante(false);
                    scadere.Enabled = true;*/
                    //if (nrparanteze > 0) EnableReady = false;
                    //else
                    EnableReady = true;
                    xstring = true; valoare = false; functie = false; operatie = false; inchidere = false; deschidere = false; scdr = false;
                }
                if (richTextBox1.Text[richTextBox1.Text.Length - 1] == VirgulaSistem)
                {
                    if (lungime < richTextBox1.Text.Length)
                    {
                        if(richTextBox1.Text.Length==1 || deschidere || xstring || inchidere)
                        {
                            richTextBox1.Text=richtextboxsave;
                            richTextBox1.Text += '0';richTextBox1.Text += VirgulaSistem;
                        }
                        if (valoare && !LastNumberContainsDot(richtextboxsave))
                        {
                            virgula_existenta = true;
                        }
                        else
                        {
                            richTextBox1.Text = richtextboxsave;
                        }
                    }
                }
            }
            else
            {
                clearButton1.PerformClick();
            }
            Underline:;

            richtextboxsave = richTextBox1.Text;
            lungime = richTextBox1.Text.Length;
            if (lungime > 0) lng = richTextBox1.Text[richTextBox1.Text.Length - 1];
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }
        string richtextboxsave = null;
        int lungime = 0;
        char lng;
        int Quality = 100;
        Pen oy = new Pen(Color.Red, 5);
        bool i_e_0 = false;
        bool previous_drawcoord = false;
        Point[] unnull_points;
        bool derivating = false;
        Point[] points;
        float[] ValuesForZoom;
        int PozitiaMijloc;
        int trackBar1Value;
        void initialize_points(Point[] points)
        {
            Point p = new Point(0, 0);
            for (int i = 0; i < points.Length; i++) points[i] = p;
        }
        /*
         * Valarile scop(parametru al UpdateGraphic)
         * 0-salveaza graficul in locatia selectata in cadrul functiei(default-optional);
         * 1-salvarea intr-o variabila bmp a graficului cu Quality,floatax,floaty respective, pt Width,Height = max(ecran) // Folosit de Resize_Begin
         * 2-deseneaza pe ecran graficul tocmai obtinut prin calcularea succesiva a puncteor // Folosit de majoritatea
         * 3-salvarea in lista vectorilor de valori valorile fiecarui punct al graficului pt intervalul(Hd=minim, m,n=maxime,floatx,floaty) // Folosit de ScrollBar1_Click
         * 4-deseneaza valorile anterior salvate (de apelarea cu valoarea 3)
         * 5-salveaza intr-o variabila bmp graficul cu Width,Height=2*max(ecran), Hd,floatx,floaty respective // Folosit de Form1_MouseDown
        */
        void Plot(int scop = 0,int Manual = 0)
        {
            {
                trackBar1Value = trackBar1.Value;
                if (scop == 0 || scop == 2 || scop == 3 || scop == 4)
                {
                    m = this.Size.Width;
                    n = this.Size.Height;
                }
                else// scop = 1 | 5
                {
                    m = System.Windows.Forms.Screen.GetBounds(Location).Width;//;
                    n = System.Windows.Forms.Screen.GetBounds(Location).Height;
                    this.GraphicPicture.Size = new System.Drawing.Size(m,n);
                    //MessageBox.Show(m.ToString());
                    //MessageBox.Show(System.Windows.Forms.Screen.GetBounds(Location).Width.ToString());
                    //n = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                    if (scop == 5)
                    {
                        m *= 2;
                        n *= 2;
                        finalx -= this.Width / 2 - Cursor.Position.X;
                        finaly -= this.Height / 2 - Cursor.Position.Y;
                    }
                }
                if (scop == 3)
                {
                    //if(Quality>50) Quality = 50;
                    Quality = 10;
                    trackBar1Value = trackBar1.Minimum;
                }
                Bitmap bmp = new Bitmap(m, n);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    if (scop != 3)
                    {
                        int interval = trackBar1.Value;
                        g.Clear(ClearColor);
                        int dubluint = 0;
                        if (interval > 100) dubluint = 1;
                        float intrad = interval;
                        float pi = (float)System.Math.PI;
                        if (XScale == 'd')
                        {
                            for (float i = m / 2 - finalx % trackBar1.Value; i < m; i += interval) { g.DrawLine(GridPen, i, 0, i, n); if (dubluint == 1) g.DrawLine(GridPen, i + interval / 2, 0, i + interval / 2, n); }
                            for (float i = m / 2 - finalx % trackBar1.Value; i > 0; i -= interval) { g.DrawLine(GridPen, i, 0, i, n); if (dubluint == 1) g.DrawLine(GridPen, i - interval / 2, 0, i - interval / 2, n); }
                        }
                        else
                        {
                            intrad *= pi / 2;
                            for (float i = m / 2 - finalx; i < m; i += intrad) if (i > -intrad) { g.DrawLine(GridPen, i, 0, i, n); if (dubluint == 1) g.DrawLine(GridPen, i + intrad / 2, 0, i + intrad / 2, n); }
                            for (float i = m / 2 - finalx; i > 0; i -= intrad) if (i < 2 * n) { g.DrawLine(GridPen, i, 0, i, n); if (dubluint == 1) g.DrawLine(GridPen, i - intrad / 2, 0, i - intrad / 2, n); }
                        }
                        if (YScale == 'd')
                        {
                            for (float i = n / 2 - finaly % trackBar1.Value; i < n; i += interval) { g.DrawLine(GridPen, 0, i, m, i); if (dubluint == 1) g.DrawLine(GridPen, 0, i + interval / 2, m, i + interval / 2); }
                            for (float i = n / 2 - finaly % trackBar1.Value; i > 0; i -= interval) { g.DrawLine(GridPen, 0, i, m, i); if (dubluint == 1) g.DrawLine(GridPen, 0, i - interval / 2, m, i - interval / 2); }
                        }
                        else
                        {
                            intrad = interval;
                            intrad *= pi / 2;
                            for (float i = n / 2 - finaly; i < n; i += intrad) if (i > 0) { g.DrawLine(GridPen, 1, i, m, i); if (dubluint == 1) g.DrawLine(GridPen, 0, i + intrad / 2, m, i + intrad / 2); }
                            for (float i = n / 2 - finaly; i > 0; i -= intrad) if (i < m) { g.DrawLine(GridPen, 1, i, m, i); if (dubluint == 1) g.DrawLine(GridPen, 0, i - intrad / 2, m, i - intrad / 2); }
                        }
                        if (finaly < n / 2 && finaly > -n / 2) g.DrawLine(AxesPen, 0, n / 2 - finaly, m, n / 2 - finaly);
                        if (finalx < m / 2 && finalx > -m / 2) g.DrawLine(AxesPen, m / 2 - finalx, 0, m / 2 - finalx, n);
                        System.Drawing.Font arial = new System.Drawing.Font("Arial", System.Convert.ToSingle(System.Math.Log(trackBar1.Value) * 2.5));
                        SolidBrush drawBrush = new SolidBrush(AxesPen.Color);
                        int j = -n / (2 * interval) - 2 - finaly / trackBar1.Value;
                        PointF drawPoint1 = new PointF(m / 2 + 1 - finalx, n / 2 - trackBar1.Value * (j * (float)System.Math.PI / 2) - finaly);
                        string radian; int j1;
                        if (YScale == 'd')
                        {
                            for (float i = 0; i <= n + 2 * interval; i += interval)
                            {
                                j++;
                                drawPoint1.Y = n / 2 - trackBar1.Value * j - finaly;
                                if (m / 2 + 1 - finalx < 0) drawPoint1.X = 5;
                                else if (m / 2 + 1 - finalx > m) drawPoint1.X = m - 40;
                                g.DrawString(System.Convert.ToString(j), arial, drawBrush, drawPoint1);
                            }
                        }
                        else
                        {
                            j = (int)(-n / (2 * intrad) - 2 - finaly / intrad);
                            for (float i = 0; i <= n + 2 * interval; i += interval)
                            {
                                j++;
                                drawPoint1.Y = n / 2 - intrad * j - finaly;
                                if (m / 2 + 1 - finalx < 0) drawPoint1.X = 5;
                                else if (m / 2 + 1 - finalx > m) drawPoint1.X = m - 40;
                                radian = null;
                                if (j % 2 == 0)
                                { j1 = j / 2; radian += System.Convert.ToString(j1) + 'π'; }
                                else
                                { j1 = j; radian += System.Convert.ToString(j1) + "π/2"; }
                                g.DrawString(radian, arial, drawBrush, drawPoint1);
                            }
                        }
                        if (XScale == 'd')
                        {

                            j = -m / (2 * interval) - 2 + finalx / trackBar1.Value;
                            PointF drawPoint3 = new PointF(m / 2 + trackBar1.Value * j - finalx, n / 2 + 1 - finaly);
                            int alternare, alt = 0;
                            for (float i = 0; i <= m + 2 * interval; i += interval)
                            {
                                j++;
                                alternare = (j.ToString().Length * 6) / interval + 1;
                                alt++; if (alt == alternare) alt = 0;
                                drawPoint3.X = m / 2 + trackBar1.Value * j - finalx;
                                if (n / 2 + 1 - finaly - 100 < 0) drawPoint3.Y = 100;
                                else if (n / 2 + 1 - finaly + 50 > n) drawPoint3.Y = n - 55;
                                if (alt == 0 || j.ToString().Length * 4 < interval)
                                    g.DrawString(System.Convert.ToString(j), arial, drawBrush, drawPoint3);

                            }
                        }
                        else
                        {
                            j = (int)(-m / (2 * intrad) - 2 + finalx / intrad);
                            PointF drawPoint3 = new PointF(m / 2 + trackBar1.Value * j - finalx, n / 2 + 1 - finaly);
                            int alternare, alt = 0;
                            for (float i = 0; i <= m + 2 * interval; i += interval)
                            {
                                j++;
                                alternare = (j.ToString().Length * 6) / (2 * interval) + 1;
                                alt++; if (alt == alternare) alt = 0;
                                drawPoint3.X = m / 2 + intrad * j - finalx;
                                if (n / 2 + 1 - finaly - 100 < 0) drawPoint3.Y = 100;
                                else if (n / 2 + 1 - finaly + 50 > n) drawPoint3.Y = n - 55;
                                radian = null;
                                if (j % 2 == 0)
                                { j1 = j / 2; radian += System.Convert.ToString(j1) + 'π'; }
                                else
                                { j1 = j; radian += System.Convert.ToString(j1) + "π/2"; }
                                if (alt == 0 || j.ToString().Length * 6 < interval) g.DrawString(radian, arial, drawBrush, drawPoint3);
                            }
                        }
                    }
                    try
                    {
                        if (gata == true)
                        {

                            int IndexVal = 0;
                            GraphicPen.Width = DimGraph.Properties.Settings.Default.GraphicWidth;
                            asave = richTextBox1.Text;
                            float prima = 1, ultima = 1, intersectie;
                            System.Drawing.Font valoy = new System.Drawing.Font("Arial", System.Convert.ToSingle(System.Math.Log(trackBar1Value) * 2.5) + 1);
                            //SolidBrush drawBrush = new SolidBrush(oy.Color); Point p = new Point();
                            float coordonata_in = 0, coordonata_ul = 0, x_in = 0, x_ul = 0;
                            int nr = 1 + (((5) * (Quality / 10) * m / trackBar1Value + finalx * Quality / (trackBar1Value)) - ((-5) * (Quality / 10) * m / trackBar1Value + (finalx - trackBar1Value / 2 - trackBar1Value / 2) * Quality / (trackBar1Value)));
                            points = new Point[nr];
                            if (scop == 3) ValuesForZoom = new float[nr];
                            if (scop == 4) IndexVal = System.Convert.ToInt32((PozitiaMijloc - (nr - 1) / 2 - 5) + 10 * finalx / trackBar1.Value);
                            initialize_points(points);
                            int i = 0; coordonata_in = 0; x_in = 0;
                            //try
                            {
                                float FunctionVal = Function.Executa(x);
                                if (FunctionVal.Equals(float.NaN)) drawcoord = true;
                                else drawcoord = false;
                                if (Function.Terminal && !Function.XTerminal && !drawcoord)
                                {
                                    coordonata_in = (n / 2 - trackBar1Value * (Function.Executa(0))) - finaly;
                                    g.DrawLine(GraphicPen, 0, coordonata_in, m, coordonata_in);
                                }
                                else
                                {//x*e^((2*ln(sqrt(x^2))-(1))/(2*ln(sqrt(x^2))+1))
                                    x = (-5) * (Quality / 10) * m / trackBar1Value + (finalx - trackBar1Value / 2 - trackBar1Value / 2) * Quality / (trackBar1Value);
                                    ultima = Function.Executa(x);
                                    if (ultima.Equals(float.NaN))
                                        drawcoord = true;
                                    else drawcoord = false;
                                    for (; x <= (5) * (Quality / 10) * m / trackBar1Value + finalx * Quality / (trackBar1Value); x += 1)
                                    {
                                        float k = x; x /= Quality;
                                        if (scop == 4) ultima = ValuesForZoom[IndexVal++];
                                        else ultima = Function.Executa(x);
                                        if (ultima.Equals(float.NaN))
                                            drawcoord = true;
                                        else if (((ultima - prima) / (x_ul - x_in)).Equals(float.PositiveInfinity) || ((ultima - prima) / (x_ul - x_in)).Equals(float.NegativeInfinity))
                                            drawcoord = true;
                                        else drawcoord = false;
                                        if (scop == 3)
                                        {
                                            ValuesForZoom[IndexVal++] = ultima;
                                            if (x == 0)
                                                PozitiaMijloc = IndexVal - 1;
                                        }
                                        if (scop != 3)
                                        {
                                            if (prima.Equals(float.NaN)) drawcoord = true;
                                            // else drawcoord = false;
                                            coordonata_in = coordonata_ul;
                                            if (coordonata_in.Equals(float.NaN)) drawcoord = true;
                                            //else drawcoord = false;
                                            coordonata_ul = (n / 2 - trackBar1Value * (ultima)) - finaly;
                                            if (coordonata_ul < -10741500)
                                                coordonata_ul = -10741500;
                                            if (coordonata_ul > 1041500)
                                                coordonata_ul = 1041500;
                                            x_in = x_ul;
                                            x_ul = m - (m / 2 - trackBar1Value * (x)) - finalx;
                                            points[i++].X = System.Convert.ToInt32(x_in);
                                            if (drawcoord && !previous_drawcoord && i > 1)
                                            {
                                                unnull_points = new Point[i];
                                                for (int j = 0; j < i; j++)
                                                    unnull_points[j] = points[j];
                                                g.DrawLines(GraphicPen, unnull_points);
                                                i = 0;
                                                points = new Point[nr]; drawcoord = false;
                                            }
                                            if (!drawcoord && previous_drawcoord)
                                                i_e_0 = true;
                                            if (i_e_0 && !previous_drawcoord && !drawcoord)
                                            { i = 0; i_e_0 = false; }
                                            if (!drawcoord && !previous_drawcoord && ((coordonata_in > 0 && coordonata_in < n) || (coordonata_ul > 0 && coordonata_ul < n)))
                                            {

                                                try
                                                {
                                                    if(!float.IsNaN(coordonata_ul))
                                                     points[i].Y = System.Convert.ToInt32(coordonata_ul);
                                                }
                                                catch
                                                {
                                                } //g.DrawLine(GraphicPen, x_ul, coordonata_ul, x_in, coordonata_in);
                                                  //System.Threading.Thread.Sleep(1000);
                                            }
                                            else if (i < 2 || ((coordonata_ul - coordonata_in) * (m - x_in) / (x_ul - x_in) + coordonata_in) / (1 - (m * (coordonata_in - coordonata_ul)) / (n * (x_ul - x_in))) > 0 && ((coordonata_ul - coordonata_in) * (m - x_in) / (x_ul - x_in) + coordonata_in) / (1 - (m * (coordonata_in - coordonata_ul)) / (n * (x_ul - x_in))) < n)
                                            {
                                                intersectie = ((coordonata_ul - coordonata_in) * (m - x_in) / (x_ul - x_in) + coordonata_in) / (1 - (m * (coordonata_in - coordonata_ul)) / (n * (x_ul - x_in)));
                                                if (!drawcoord && !previous_drawcoord && ((intersectie > 0 && intersectie < n)))
                                                {

                                                    try
                                                    {
                                                        points[i].Y = System.Convert.ToInt32(coordonata_ul);
                                                    }
                                                    catch
                                                    {
                                                    }
                                                    //g.DrawLine(GraphicPen, x_ul, coordonata_ul, x_in, coordonata_in);
                                                    //System.Threading.Thread.Sleep(1000);
                                                }
                                                else if (coordonata_ul > n)
                                                {
                                                    try
                                                    {
                                                        points[i].Y = System.Convert.ToInt32(n);
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }
                                            }
                                            else if (i != 1)
                                            {
                                                try
                                                {
                                                    points[i].Y = points[i - 1].Y;
                                                }
                                                catch { }
                                            }
                                        }
                                        x = k;
                                        if (drawcoord)
                                        { previous_drawcoord = true; i--; }
                                        else { previous_drawcoord = false; }
                                    }
                                }
                            }
                            //              catch { }
                            if(Manual == 0)
                            if (!derivating)
                            {
                                if (!drawcoord && numericUpDown1.Visible == true)
                                {
                                    System.Drawing.Graphics graphics = this.CreateGraphics();
                                    x = (float)numericUpDown1.Value;
                                    if (YScale == 'd') a = System.Convert.ToString(Function.Executa(x));
                                    else a = System.Convert.ToString(RadtoDeg(Function.Executa(x))) + "*π";
                                    if (a != "NaN")
                                    {
                                        if (scop == 2) richTextBox2.Text = a;
                                        if(drawcoord) { richTextBox2.Text = "Infinity"; }
                                    }
                                }
                                else if (!drawcoord && numericUpDown2.Visible == true)
                                {
                                    if (gata)
                                    {
                                        string str;
                                        x = (((float)numericUpDown2.Value * (float)System.Math.PI / (float)numericUpDown3.Value));// * (Quality / 10) * this.Width / trackBar1.Value + finalx * Quality / (trackBar1.Value));
                                        if (YScale == 'd') str = System.Convert.ToString(Function.Executa(x));
                                        else str = System.Convert.ToString(RadtoDeg(Function.Executa(x))) + "*π";
                                        if (str != "NaN*π" && YScale == 'd' || str != "NaN" && YScale == 'r')
                                        {
                                            richTextBox2.Text = str;
                                            if (drawcoord && YScale == 'd')
                                                richTextBox2.Text = "Infinity";
                                            else if (drawcoord && YScale == 'r')
                                                richTextBox2.Text = "Infinity";
                                            else
                                            {
                                                //  System.Drawing.Graphics graphics = this.CreateGraphics();
                                                //  Pen aa = new Pen(Color.Magenta, 2); Pen bb = new Pen(Color.YellowGreen, 2);
                                                //  g.DrawLine(aa, this.Width / 2 - finalx, (this.Height / 2 - trackBar1.Value * (Function.Executa(x))) - finaly, this.Width / 2 + x * trackBar1.Value - finalx, (this.Height / 2 - trackBar1.Value * (Function.Executa(x))) - finaly);
                                                //  g.DrawLine(aa, this.Width / 2 + x * trackBar1.Value - finalx, this.Height / 2 - finaly, this.Width / 2 + x * trackBar1.Value - finalx, (this.Height / 2 - trackBar1.Value * (Function.Executa(x))) - finaly);
                                                //  g.DrawLine(bb, this.Width - (this.Width / 2 - trackBar1.Value * (x)) - finalx - 2, (this.Height / 2 - trackBar1.Value * (Function.Executa(x))) - finaly - 2, this.Width - (this.Width / 2 - trackBar1.Value * (x)) - finalx + 2, (this.Height / 2 - trackBar1.Value * (Function.Executa(x))) - finaly + 2);
                                            }
                                        }
                                        else richTextBox2.Text = "NaN";
                                    }
                                }
                            }
                            if (scop != 3)
                            {
                                unnull_points = new Point[i];
                                for (int j = 0; j < i; j++)
                                    unnull_points[j] = points[j];
                                if (i > 1)
                                {
                                    // for (int j = 0; j < i; j++) unnull_points[j].X += 300;
                                    g.DrawLines(GraphicPen, unnull_points);
                                }
                            }
                        }
                    }
                    catch { }
                }
                if (scop == 0) bmp.Save(saveFileDialog1.FileName);
                else if (scop == 1) bmpForResize = bmp;
                else if (scop == 2 || scop == 4 || scop == 6)
                {
                    //Graphics g = GraphicPicture.CreateGraphics();
                    //g.DrawImage(bmp, 0, 0);
                    GraphicPicture.Image = bmp;
                }
                else if (scop == 5)
                {
                    bmpForMove = bmp;
                }
            }
        }
        Pen AxesPen;
        Pen GraphicPen;
        Pen GridPen ;
        Color ClearColor = Color.White;


        private void Window_Resize(object sender, EventArgs e)
        {
            //if (bmpForResize == null) 
            Plot(1);
            Bitmap ResizeBMP = new Bitmap(Width, Height);
            using (resizeGraphics = System.Drawing.Graphics.FromImage(ResizeBMP))
            {
                resizeGraphics.DrawImage(bmpForResize, new Point(this.Width / 2 - bmpForResize.Width / 2, this.Height / 2 - bmpForResize.Height / 2));
                GraphicPicture.Image = ResizeBMP;
            }
                
        }
        private void Window_ResizeBegin(object sender, EventArgs e)
        {
            Quality = 10;
            //resizeGraphics = this.CreateGraphics();
            Plot(1); 
        }

        Graphics resizeGraphics;
        Bitmap bmpForResize;
        Bitmap bmpForMove;
        private void Sin_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "sin(";
        }
        private void Cos_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "cos(";

        }
        private void Tg_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "tg(";

        }
        private void Ctg_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "ctg(";
        }
        private void Lga_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "lg(";

        }
        private void Ln_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "ln(";

        }
        private void sqrt2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "sqrt(";

        }
        private void pi_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "π";
        }
        private void e_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "e";
        }
        int nrparanteze = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "x";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += ")";
        }
        float copyx, copyy;
        private void ZoomBar_Scroll(object sender, EventArgs e)
        {
            finalx = (int)(copyx * trackBar1.Value);//|
            finaly = (int)(copyy * trackBar1.Value);//|Pentru ca graficul sa pastreze centrul de zoom in centrul ecranului, nu in O(0,0)
            Plot(4);
        }
        private void ZoomBar_MouseDown(object sender, MouseEventArgs e)
        {
            finalx = (int)(copyx * trackBar1.Value);
            finaly = (int)(copyy * trackBar1.Value);
            Plot(3);
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            SetHD();
            drawinrad();
        }

        private void ZoomBar_KeyDown(object sender, KeyEventArgs e)
        {
            finalx = (int)(copyx * trackBar1.Value);
            finaly = (int)(copyy * trackBar1.Value);
            Plot(3);
        }

        private void ZoomBar_KeyUp(object sender, KeyEventArgs e)
        {
            SetHD();
            drawinrad();
        }

        private void b1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "1";

        }
        private void b2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "2";

        }
        private void b3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "3";

        }
        private void b4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "4";

        }
        private void b5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "5";
        }
        private void b6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "6";

        }
        private void b7_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "7";

        }
        private void b8_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "8";

        }
        private void b9_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "9";

        }
        private void b0_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "0";

        }
        private void bp_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "+";
        }
        private void bm_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "-";
        }
        private void bo_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "*";
        }
        private void bi_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "/";
        }
        private void bpt_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "^";
        }
        private void Arcsin_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "arcsin(";
        }

        private void Arccos_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "arccos(";
        }

        private void Arctg_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "arctg(";
        }

        private void Arcctg_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "arcctg(";
        }
        private void ClearInput(object sender, EventArgs e)
        {
            virgula_existenta = false;
            //virgula.Enabled = false;
            valoare = true;
            functie = true;
            operatie = true;
            deschidere = true;
            inchidere = true;
            nrparanteze = 0;
            /*constante(true);
            x_functii_pi_e(true);
            operatii_aritmetice(false);
            scadere.Enabled = true;
            Parantezeu.Enabled = true;*/
            richTextBox1.Text = null;
            SetKeyboardVisibility(1);
            groupBox1.Visible = true;
            Plot(2);
        }
        private void xp_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "x"; richTextBox1.Text += "^"; richTextBox1.Text += "2";
        }
        private void Window_ResizeEnd(object sender, EventArgs e)
        {
            SetHD();    
            drawinrad();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "x"; richTextBox1.Text += "^"; richTextBox1.Text += "3";
        }
        private void richTextBox1_Click_1(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            SetKeyboardVisibility(1);
        }
        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;

        }
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }
        char VirgulaSistem;
        bool LastNumberContainsDot(string s)
        {
            int i = s.Length-1;
            int res;
            while(i>-1)
            {
                if (!int.TryParse(s[i].ToString(), out res))
                {
                    if (s[i] == VirgulaSistem) return true;
                    else return false;
                }
                else i--;
            }
            return false;
        }
        private void button2_Click_2(object sender, EventArgs e)
        {
            if(!LastNumberContainsDot(richTextBox1.Text))
                richTextBox1.Text += VirgulaSistem;
        }
        private void GraphicSize1_Click(object sender, EventArgs e)
        {
            DimGraph.Properties.Settings.Default.GraphicWidth = 1;
            drawinrad();
        }
        private void GraphicSize2_Click(object sender, EventArgs e)
        {
            DimGraph.Properties.Settings.Default.GraphicWidth = 2;
            drawinrad();
        }
        private void ParantezaLR_Click(object sender, EventArgs e)
        {
            if (valoare && nrparanteze > 0) { valoare = false; inchidere = true; richTextBox1.Text += ')'; }
            else if (operatie) { operatie = false; deschidere = true; richTextBox1.Text += '('; }
            else if (xstring && nrparanteze > 0) { xstring = false; inchidere = true; richTextBox1.Text += ')'; }
            else if (deschidere) { richTextBox1.Text += '('; }
            else if (inchidere && nrparanteze > 0) { richTextBox1.Text += ')'; }
            else if(inchidere && nrparanteze == 0) { richTextBox1.Text += '('; }
            else if (scdr) { richTextBox1.Text += '('; }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        int dltx = 0, dlty = 0, finalx = 0, finaly = 0;
        void SetHD()
        {
            Quality = trackBar1.Value;
            if (Quality < 100) Quality = 100;
        }
        //urmatoarele trei functii pot contine operatii reziduale
        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Middle)
            {
                dltx = 0;
                dlty = 0;
                dltx = Cursor.Position.X + finalx;
                dlty = Cursor.Position.Y + finaly;
                AbsolutReference.X = dltx;
                AbsolutReference.Y = dlty;
                //if(Quality>50) Quality = 50;
                FirstTick = true;
                Plot(5);
                //SetHD();
                moveGraphics = this.CreateGraphics();
                numericCancelEventCall = true;
                //GraphicPicture.Visible = true;
                timer2.Start();
            }
        }
        Graphics moveGraphics;
        Point AbsolutReference;
        DateTime StartMoment, StopMoment;
        bool FirstTick = false;
        private void MouseDownTimer_Tick(object sender, EventArgs e)
        {
            if (Cursor.Position != AbsolutReference)
            {

                if (FirstTick) StartMoment = DateTime.Now;
                finalx = AbsolutReference.X - Cursor.Position.X;
                finaly = AbsolutReference.Y - Cursor.Position.Y;
                //if (finalx > System.Convert.ToInt32(numericUpDown2.Maximum)) finalx = System.Convert.ToInt32(numericUpDown2.Maximum);
                //if (finaly > System.Convert.ToInt32(numericUpDownfloaty.Maximum)) finaly = System.Convert.ToInt32(numericUpDownfloaty.Maximum);
                //if (finalx < System.Convert.ToInt32(numericUpDown2.Minimum)) finalx = System.Convert.ToInt32(numericUpDown2.Minimum);
                //if (finaly < System.Convert.ToInt32(numericUpDownfloaty.Minimum)) finaly = System.Convert.ToInt32(numericUpDownfloaty.Minimum);
                copyx = (float)finalx / trackBar1.Value;
                copyy = (float)finaly / trackBar1.Value;
                numericUpDown1.Value = finalx / trackBar1.Value;
                numericUpDown2.Value = finalx / trackBar1.Value * numericUpDown3.Value / System.Convert.ToDecimal(System.Math.PI);
                richTextBox2.Text = (-finaly / trackBar1.Value).ToString();
                if (FirstTick)
                {
                    StopMoment = DateTime.Now;
                    timer2.Interval = 1;
                    FirstTick = false;
                }
                //moveGraphics.DrawImage(bmpForMove, new Point(Cursor.Position.X - bmpForMove.Size.Width / 2, Cursor.Position.Y - bmpForMove.Size.Height / 2));
                //richTextBox2.Text = GraphicPicture.Image.Size.Width.ToString() + '-' + bmpForMove.Size.Width.ToString();
                //Rectangle rect = new Rectangle(new Point(-Cursor.Position.X + bmpForMove.Size.Width / 2, -Cursor.Position.Y + bmpForMove.Size.Height / 2), GraphicPicture.Size);
               // richTextBox2.Text += '/' + rect.X.ToString() + ',' + rect.Y.ToString() + '*' + rect.Width.ToString() + ',' + rect.Height.ToString();
                GraphicPicture.Image = bmpForMove.Clone(new Rectangle(new Point(-Cursor.Position.X + bmpForMove.Size.Width / 2, -Cursor.Position.Y + bmpForMove.Size.Height / 2),GraphicPicture.Size),bmpForMove.PixelFormat);
                //MessageBox.Show((bmpForMove.Width).ToString());
                // ^
                // | Eroarea ar trebui sa se rezolve daca bmpForMove ar fi dublul dimensiunii ecranuui si nu cu cca 100 pixeli mai putin de atat;
            }//69 53
        }
        private void Window_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Middle)
            {
                timer2.Stop();
                //GraphicPicture.Visible = false;
                SetHD();
                finalx = dltx - Cursor.Position.X;
                finaly = dlty - Cursor.Position.Y;
                //if (finalx > System.Convert.ToInt32(numericUpDown2.Maximum)) finalx = System.Convert.ToInt32(numericUpDown2.Maximum);
                //if (finaly > System.Convert.ToInt32(numericUpDownfloaty.Maximum)) finaly = System.Convert.ToInt32(numericUpDownfloaty.Maximum);
                //if (finalx < System.Convert.ToInt32(numericUpDown2.Minimum)) finalx = System.Convert.ToInt32(numericUpDown2.Minimum);
                //if (finaly < System.Convert.ToInt32(numericUpDownfloaty.Minimum)) finaly = System.Convert.ToInt32(numericUpDownfloaty.Minimum);
                copyx = (float)finalx / trackBar1.Value;
                copyy = (float)finaly / trackBar1.Value;
                if (dltx != 0 && dlty != 0)
                {
                    Plot(2);
                }
                numericUpDown1.Value = finalx / trackBar1.Value;
                numericUpDown2.Value = finalx / trackBar1.Value * numericUpDown3.Value / System.Convert.ToDecimal(System.Math.PI);
                richTextBox2.Text = (-finaly / trackBar1.Value).ToString();
                numericCancelEventCall = false;
            }
        }
        bool numericCancelEventCall = false;//Daca timer actualizeaza numericUpdown 1 si 2, acestea nu trebuie sa deplaseze grficul, deci anuleaza corpul evenimentelor value_changed
        string apelare = null;
        void Goto(int Manual = 0)
        {
            if (XScale == 'd') finalx = (int)numericUpDown1.Value * trackBar1.Value;
            else finalx = System.Convert.ToInt32((float)numericUpDown2.Value / (float)numericUpDown3.Value * trackBar1.Value * (float)System.Math.PI);
            try
            {
                if (YScale == 'd') finaly = -(int)System.Convert.ToSingle(richTextBox2.Text) * trackBar1.Value;
                else finaly = -System.Convert.ToInt32(System.Convert.ToInt16(richTextBox2.Text) * (float)trackBar1.Value * (float)System.Math.PI);
                

                copyx = (float)finalx / trackBar1.Value;
                copyy = (float)finaly / trackBar1.Value;
                Plot(2,Manual);
            }
            catch { }
        }

        private void ResetGraphic_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            finalx = 0; copyx = 0;
            finaly = 0; copyy = 0;
            numericUpDown2.Value = 0;
            richTextBox2.Text = "0";
            trackBar1.Value = 20;
            SetHD();
            drawinrad();
        }
        private void SaveGraphic_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Plot();
        }

        private void saveGraphicToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save graphic";
            saveFileDialog1.ShowDialog();
        }
        char XScale = 'd', YScale = 'd';
        private void RadX_Click(object sender, EventArgs e)
        {
            XScale = 'r';
            rADToolStripMenuItem.Checked = true;
            dEGToolStripMenuItem.Checked = false;
            drawinrad();
            //26843578
            // numericUpDownfloatx.Maximum = 26843578; if (numericUpDownfloatx.Value > numericUpDownfloatx.Maximum) numericUpDownfloatx.Value = numericUpDownfloatx.Maximum;
            // numericUpDownfloatx.Minimum = -26843578; if (numericUpDownfloatx.Value < numericUpDownfloatx.Minimum) numericUpDownfloatx.Value = numericUpDownfloatx.Minimum;
            // numericUpDownfloaty.Maximum = 26843578; if (numericUpDownfloaty.Value > numericUpDownfloaty.Maximum) numericUpDownfloaty.Value = numericUpDownfloaty.Maximum;
            //numericUpDownfloaty.Minimum = -26843578; if (numericUpDownfloaty.Value < numericUpDownfloaty.Minimum) numericUpDownfloaty.Value = numericUpDownfloaty.Minimum;

            numericUpDown2.Visible = true;
            numericUpDown3.Visible = true;
            numericUpDown1.Visible = false;
        }

        private void DegX_Click(object sender, EventArgs e)
        {
            XScale = 'd';
            rADToolStripMenuItem.Checked = false;
            dEGToolStripMenuItem.Checked = true;
            drawinrad();
            //numericUpDownfloatx.Maximum = 100000000;
            //numericUpDownfloatx.Minimum = -100000000;
            //numericUpDownfloaty.Maximum = 100000000;
            //numericUpDownfloaty.Minimum = -100000000;

            numericUpDown2.Visible = false;
            numericUpDown3.Visible = false;
            numericUpDown1.Visible = true;
        }
        float RadtoDeg(float value)
        {
            return value / (float)System.Math.PI;
        }

        void drawinrad()
        {
            Plot(2);
        }

        private void NumericUpDownDeg_ValueChanged(object sender, EventArgs e)
        {
            if (!numericCancelEventCall)
            {
                drawinrad();
                Goto();
            }
        }

        private void numericUpDownRadNumarator_ValueChanged(object sender, EventArgs e)
        {
            if (!numericCancelEventCall)
            {
                drawinrad();
                Goto();
            }
        }

        private void numericUpDownRadNumitor_ValueChanged(object sender, EventArgs e)
        {
            if (!numericCancelEventCall)
            {
                drawinrad();
                Goto();
            }
        }

        private void RadY_Click(object sender, EventArgs e)
        {
            YScale = 'r';
            rADToolStripMenuItem1.Checked = true;
            dEGToolStripMenuItem1.Checked = false;
            drawinrad();
            //26843578
            //numericUpDownfloatx.Maximum = 26843578; if (numericUpDownfloatx.Value > numericUpDownfloatx.Maximum) numericUpDownfloatx.Value = numericUpDownfloatx.Maximum;
            //numericUpDownfloatx.Minimum = -26843578; if (numericUpDownfloatx.Value < numericUpDownfloatx.Minimum) numericUpDownfloatx.Value = numericUpDownfloatx.Minimum;
            //numericUpDownfloaty.Maximum = 26843578; if (numericUpDownfloaty.Value > numericUpDownfloaty.Maximum) numericUpDownfloaty.Value = numericUpDownfloaty.Maximum;
            //numericUpDownfloaty.Minimum = -26843578; if (numericUpDownfloaty.Value < numericUpDownfloaty.Minimum) numericUpDownfloaty.Value = numericUpDownfloaty.Minimum;

        }

        private void DegY_Click(object sender, EventArgs e)
        {
            YScale = 'd';
            rADToolStripMenuItem1.Checked = false;
            dEGToolStripMenuItem1.Checked = true;
            drawinrad();
            //numericUpDownfloatx.Maximum = 100000000;
            //numericUpDownfloatx.Minimum = -100000000;
            //numericUpDownfloaty.Maximum = 100000000;
            //numericUpDownfloaty.Minimum = -100000000;

        }

        private void TextBoxY_Leave(object sender, EventArgs e)
        {
            Goto();
        }
        bool ShiftPressed = false, ControlPressed = false;

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) ShiftPressed = true;
            else if (e.KeyCode == Keys.ControlKey) ControlPressed = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            ShiftPressed = false;
            ControlPressed = false;
        }

        private void richTextBox1_MouseEnter(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length >= 60)
            {
                richTextBox1.Size = new Size(Width - 27, 25);
                clearButton1.Visible = false;
                keyboardButton1.Visible = false;
                readyButton1.Visible = false;
            }
        }

        private void richTextBox1_MouseLeave(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length >= 60)
            {
                richTextBox1.Size = new Size(517, 25); Plot(2);
                clearButton1.Visible = true;
                keyboardButton1.Visible = true;
                readyButton1.Visible = true;
            }
        }

        private void GridColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                DimGraph.Properties.Settings.Default.GridColor = colorDialog1.Color;
                GridPen.Color = colorDialog1.Color;
                Plot(2);
            }
        }

        private void GraphicColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                DimGraph.Properties.Settings.Default.GraphicColor = colorDialog1.Color;
                GraphicPen.Color = colorDialog1.Color;
                Plot(2);
            }
        }

        private void KeyBoardVisibility_Click(object sender, EventArgs e)
        {
            SetKeyboardVisibility();
        }


        private void richTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            Goto(1);
        }
        void SetButtonTheme(OwnForeColorButton B, bool Dark)
        {
            if(Dark)
            {
                B.DarkTheme = true;
                B.BackColor = Color.Black;
                B.ForeColor = System.Drawing.SystemColors.ControlLight;
                
            }
            else
            {
                B.DarkTheme = false;
                B.BackColor = System.Drawing.SystemColors.ControlLight;
                B.ForeColor = Color.Black;
            }
        }
        void SetOriginalButtonTheme(Button B, bool Dark)
        {
            if (Dark)
            {
                B.BackColor = Color.Black;
                B.ForeColor = System.Drawing.SystemColors.ControlLight;

            }
            else
            {
                B.BackColor = System.Drawing.SystemColors.ControlLight;
                B.ForeColor = Color.Black;
            }
        }
        void ChangeDarkTheme(bool OnOpenForm = false)
        {
            if (darkThemeToolStripMenuItem.Checked)
            {
                Properties.Settings.Default.DarkTheme = darkThemeToolStripMenuItem.Checked = false;
                Properties.Settings.Default.ClearColor = Color.White;
                ClearColor = Color.White;
                Properties.Settings.Default.AxesColor = Color.Black;
                AxesPen = new Pen(Color.Black, 2);
                if(!OnOpenForm)
                {
                    Properties.Settings.Default.GridColor = Color.FromArgb(65, 65, 65);
                    GridPen.Color = Color.FromArgb(65, 65, 65);
                    Properties.Settings.Default.GraphicColor = Color.Blue;
                    GraphicPen.Color = Color.Blue;
                }
                readyButton1.DarkTheme = false;
                keyboardButton1.DarkTheme = false;
                clearButton1.DarkTheme = false;
                BackColor = Color.White;
                richTextBox1.BackColor = Color.White;
                richTextBox1.ForeColor = Color.Black;
                numericUpDown1.BackColor = Color.White;
                numericUpDown1.ForeColor = Color.Black;
                numericUpDown2.BackColor = Color.White;
                numericUpDown2.ForeColor = Color.Black;
                numericUpDown3.BackColor = Color.White;
                numericUpDown3.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
                richTextBox2.BackColor = Color.White;
                richTextBox2.ForeColor = Color.Black;
                menuStrip1.BackColor = Color.White;
                menuStrip1.ForeColor = Color.Black;
                {//Keyboard
                    SetButtonTheme(Sin, false);
                    SetButtonTheme(Cos, false);
                    SetButtonTheme(Tg, false);
                    SetButtonTheme(Ctg, false);
                    SetButtonTheme(Lga, false);
                    SetButtonTheme(Ln, false);
                    SetButtonTheme(sqrtButton1, false);
                    SetButtonTheme(pi, false);
                    SetButtonTheme(be, false);
                    SetButtonTheme(varX, false);
                    SetButtonTheme(xp, false);
                    SetButtonTheme(Parantezeu, false);
                    SetButtonTheme(b0, false);
                    SetButtonTheme(b1, false);
                    SetButtonTheme(b2, false);
                    SetButtonTheme(b3, false);
                    SetButtonTheme(b4, false);
                    SetButtonTheme(b5, false);
                    SetButtonTheme(b6, false);
                    SetButtonTheme(b7, false);
                    SetButtonTheme(b8, false);
                    SetButtonTheme(b9, false);
                    SetButtonTheme(adunare, false);
                    SetButtonTheme(scadere, false);
                    SetButtonTheme(inmultire, false);
                    SetButtonTheme(impartire, false);
                    SetButtonTheme(virgula, false);
                    SetButtonTheme(putere, false);
                    SetButtonTheme(xt, false);
                    SetButtonTheme(Arcsin, false);
                    SetButtonTheme(Arccos, false);
                    SetButtonTheme(Arctg, false);
                    SetButtonTheme(Arcctg, false);
                }
            }
            else
            {
                Properties.Settings.Default.DarkTheme = darkThemeToolStripMenuItem.Checked = true;
                Properties.Settings.Default.ClearColor = Color.Black;
                ClearColor = Color.Black;
                Properties.Settings.Default.AxesColor = Color.White;
                AxesPen = new Pen(Color.White, 2);
                if(!OnOpenForm)
                {
                    Properties.Settings.Default.GridColor = Color.FromArgb(65, 65, 65);
                    GridPen.Color = Color.FromArgb(65, 65, 65);
                    Properties.Settings.Default.GraphicColor = Color.Yellow;
                    GraphicPen.Color = Color.Yellow;
                }
                readyButton1.DarkTheme = true;
                keyboardButton1.DarkTheme = true;
                clearButton1.DarkTheme = true;
                BackColor = Color.Black;
                richTextBox1.BackColor = Color.Black;
                richTextBox1.ForeColor = Color.White;
                numericUpDown1.BackColor = Color.Black;
                numericUpDown1.ForeColor = Color.White;
                numericUpDown2.BackColor = Color.Black;
                numericUpDown2.ForeColor = Color.White;
                numericUpDown3.BackColor = Color.Black;
                numericUpDown3.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                richTextBox2.BackColor = Color.Black;
                richTextBox2.ForeColor = Color.White;
                menuStrip1.BackColor = Color.Black;
                menuStrip1.ForeColor = Color.White;
                {//keyboard
                    SetButtonTheme(Sin, true);
                    SetButtonTheme(Cos, true);
                    SetButtonTheme(Tg, true);
                    SetButtonTheme(Ctg, true);
                    SetButtonTheme(Lga, true);
                    SetButtonTheme(Ln, true);
                    SetButtonTheme(sqrtButton1, true);
                    SetButtonTheme(pi, true);
                    SetButtonTheme(be, true);
                    SetButtonTheme(varX, true);
                    SetButtonTheme(xp, true);
                    SetButtonTheme(Parantezeu, true);
                    SetButtonTheme(b0, true);
                    SetButtonTheme(b1, true);
                    SetButtonTheme(b2, true);
                    SetButtonTheme(b3, true);
                    SetButtonTheme(b4, true);
                    SetButtonTheme(b5, true);
                    SetButtonTheme(b6, true);
                    SetButtonTheme(b7, true);
                    SetButtonTheme(b8, true);
                    SetButtonTheme(b9, true);
                    SetButtonTheme(adunare, true);
                    SetButtonTheme(scadere, true);
                    SetButtonTheme(inmultire, true);
                    SetButtonTheme(impartire, true);
                    SetButtonTheme(virgula, true);
                    SetButtonTheme(putere, true);
                    SetButtonTheme(xt, true);
                    SetButtonTheme(Arcsin, true);
                    SetButtonTheme(Arccos, true);
                    SetButtonTheme(Arctg, true);
                    SetButtonTheme(Arcctg, true);
                }
            }
            Plot(2);
        }
        private void DarkTheme_Click(object sender, EventArgs e)
        {
            ChangeDarkTheme();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        void SetKeyboardVisibility(int Visible=2)
        {
            if(Visible == 0)
            {
                groupBox1.Visible = false; if (gata) Function = new Functie(richTextBox1.Text); Plot(2);
            }
            else if(Visible == 1)
            {
                groupBox1.Visible = true;
            }
            else // if Visible == 2
            {
                if (groupBox1.Visible == false)
                {
                    groupBox1.Visible = true;
                }
                else { groupBox1.Visible = false; if (gata) Function = new Functie(richTextBox1.Text); Plot(2); }
            }
        }

        private void FocusOnRichTextBox(object sender, EventArgs e)
        {
            richTextBox1.Focus();
        }

        private void Window_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ShiftPressed && ControlPressed) ;
            else if (ShiftPressed)
            {
                finalx -= e.Delta / 6;
                copyx = (float)finalx / trackBar1.Value;
                if (XScale == 'd')
                {
                    numericCancelEventCall = true;
                    numericUpDown1.Value = (decimal)copyx;
                    numericCancelEventCall = false;
                }
                else
                {
                    numericCancelEventCall = true;
                    numericUpDown2.Value = (decimal)(copyx * System.Math.PI * (float)numericUpDown3.Value);
                    numericCancelEventCall = false;
                }
            }
            else if (ControlPressed)
            {

                try
                {
                    trackBar1.Value += e.Delta * trackBar1.Value / 1200;
                    finalx = (int)(copyx * trackBar1.Value);
                    finaly = (int)(copyy * trackBar1.Value);
                    SetHD();
                }
                catch { }
            }
            else
            {
                finaly -= e.Delta / 6;
                copyy = (float)finaly / trackBar1.Value;
                if (XScale == 'd')
                {
                    richTextBox2.Text = (-copyy).ToString();
                }
                else
                {
                    richTextBox2.Text = (-copyy * System.Math.PI).ToString();
                }
            }
            Plot(2);
        }       
        Form1 der;
        private void ViewDerivate_Click(object sender, EventArgs e)
        {
            if (gata)
            {
                if (Function.Derivata == null)
                    Function.Deriveaza();
                der = new Form1(null);
                der.Function = Function.Derivata;
                der.richtextboxsave = Function.Derivata.Corp.Remove(Function.Derivata.Corp.Length-1);
                der.ShowDialog();
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) ready();
        }

        private void ReadyButton_Click(object sender, EventArgs e)
        {
            ready();
        }

        private void richTextBox2_Validated(object sender, EventArgs e)
        {
            Goto();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
    }
}
/*
 * ----------------------Probleme posibile solutii---------------------------------
 * x^-1 == (sqrt(1/x))^2
//sqrt(9-x^2) != sqrt(9-x*x)      |
//   X              V             |->puteri pare  => Butnul '-' => richTextBox1.Text+="-(" V
//sqrt(9-x^4) != sqrt(9-x*x*x*x)  |
 * sqrt nu poate fi introus in anumite operatii (nu apare graficul) (=infinity) V
 * introducerea derivatelor in UpdateGraphic()
 * OnMouseDown => Salvam bmp dublu ecranului           |
 * OnMouseStayDown => Desenam Bmp f(cursor.x,cursor.y) |
 * OnMouseUp => Nimic                                  |
 *Unde intervalul nu arata desktopului axele, in movegraphic, coordonatele dispar.(Alte afisari ale gridului afiseaza coordonatele
 *      |-> Apar doar la mouseup
 *Function nu manipuleaza NaN
 * Form1_MouseWheel->(Up-Down) nu e nevoie sa recalculeze punctele calculate. Ele sunt aceleasi cu cele anterioare. Tot ce trebuie facut e de implementat un sistem ce memoreaza vectorul de valori inainte de a fi cerut.
 * Daca exista in vectorul de valori 3 puncte consecutive cu aceeasi valoare, cea din mijloc va fi stearesa || nr--, pas(nu memoram)
 * (V) y richtextbox poate afisa valori negate ex:2018  
 * this.GraphicPicture.Size = new System.Drawing.Size(System.Windows.Forms.Screen.GetBounds(Location).Width, System.Windows.Forms.Screen.GetBounds(Location).Height);//1366, 728 ar trebui sa ilocuiasca actuala initializare a zimensiunii PictureBox-ului
 * Stanga graficului sqrt(9-x^2) ar putea fi solutia pentru discontinuitatea tg(x)
 * -----------------------Variabile inutile ?------------------------------
 * asave y hd
 */
