using System;
using System.Collections.Generic;
using System.Linq;
namespace DimGraph
{
    class Functie
    {
        string corp;
        public string Corp
        {
            get
            {
                return corp;
            }
            set
            {
                corp = value;
                Creeaza(corp);

            }
        }
        //Constructori
        public Functie() { }
        public Functie(string corp)
        {
            corp = CorpulPrincipal(corp);
            // corp = corp.Replace("e", Math.E.ToString());
            //corp = corp.Replace("π", Math.PI.ToString());
            Corp = corp;
            Derivata = null;
        }
        public Functie(char oper, Functie f1, Functie f2 = null)
        {
            string s1 = f1.Corp;
            string s2 = null;
            s1 = CorpulPrincipal(s1);
            Functie1 = f1;
            if (Functie1.Corp.IndexOf("x") == -1)
            {
                Functie1 = new Functie(Functie1.Executa(0));
                s1 = Functie1.Corp;
            }
            if (f2 != null)
            {
                s2 = f2.Corp;
                s2 = CorpulPrincipal(s2);
                Functie2 = f2;
                if (Functie2.Corp.IndexOf("x") == -1)
                {
                    Functie2 = new Functie(Functie2.Executa(0));
                    s2 = Functie2.Corp;
                }
            }
            else if (oper == '-') MinusESemn = true;
            Operatie = new Operatie(oper);
            string UnarOperat = null;
            if (oper == '0')
            {
                XTerminal = true;
                Terminal = true;
                Operatie = new Operatie('X');
                Functie1 = Functie2 = null;
            }
            if (f2 != null)
            {
                UnarOperat = oper.ToString();
                if (oper == '+' || oper == '-')
                {
                    if (Functie1.Terminal && !Functie1.XTerminal && Functie2.Terminal && !Functie2.XTerminal)
                    {
                        if (oper == '+') ValoareTerminal = (float)(Functie1.ValoareTerminal + Functie2.ValoareTerminal);
                        else ValoareTerminal = (float)(Functie1.ValoareTerminal - Functie2.ValoareTerminal);
                        corp = ValoareTerminal.ToString();
                        Terminal = true;
                        Operatie = new Operatie('0');
                        Functie1 = Functie2 = null;
                    }
                    else if (Functie2.Corp == "0") corp = s1;
                    else if (Functie1.Corp == "0")
                    {
                        if (oper == '+') corp = s2;
                        else
                        {
                            if (Functie2.Operatie != '-' && Functie2.Operatie != '+') corp = '-' + s2;
                            else corp = "-(" + s2 + ')';
                        }
                    }
                    else
                    {
                        if (Functie2.Operatie != '-' && Functie2.Operatie != '+') corp = s1 + UnarOperat + s2;
                        else corp = s1 + UnarOperat + '(' + s2 + ')';
                    }
                }
                else if (oper == '*' || oper == '/')
                {
                    if (Functie1.Operatie == '+' || (Functie1.Operatie == '-' && !Functie1.MinusESemn))
                    {
                        s1 = CorpulPrincipal(s1);
                        s1 = "(" + s1 + ")";
                    }
                    if (Functie2.Operatie == '+' || (Functie2.Operatie == '-' && !Functie2.MinusESemn))
                    {
                        s2 = CorpulPrincipal(s2);
                        s2 = "(" + s2 + ")";
                    }
                    if (oper == '/' && (Functie1.Operatie == '+' || Functie1.Operatie == '*' || Functie1.Operatie == '/' || (Functie1.Operatie == '-' && Functie1.MinusESemn)))
                    {
                        if (!Functie1.Terminal)
                        {
                            s1 = CorpulPrincipal(s1);
                            s1 = "(" + s1 + ")";
                        }
                    }
                    if (oper == '/' && (Functie2.Operatie == '+' || Functie2.Operatie == '*' || Functie2.Operatie == '/' || (Functie2.Operatie == '-' && Functie2.MinusESemn)))
                    {
                        if (!Functie2.Terminal)
                        {
                            s2 = CorpulPrincipal(s2);
                            s2 = "(" + s2 + ")";
                        }
                    }
                    if (Functie1.Terminal && !Functie1.XTerminal && Functie2.Terminal && !Functie2.XTerminal)
                    {
                        if (oper == '*') ValoareTerminal = (float)(Functie1.ValoareTerminal * Functie2.ValoareTerminal);
                        else ValoareTerminal = (float)(Functie1.ValoareTerminal / Functie2.ValoareTerminal);
                        corp = ValoareTerminal.ToString();
                        Terminal = true;
                        Operatie = new Operatie('0');
                        Functie1 = Functie2 = null;
                    }
                    else if (Functie1.Corp == "1")
                    { if (oper == '*') corp = s2; else corp = "1/" + s2; if (s2 == "1") corp = "1"; }
                    else if (Functie2.Corp == "-1") corp = '-' + s1;
                    else if (Functie2.Corp == "1")
                    {
                        corp = s1;

                    }
                    else if (Functie1.Corp == "-1") if (oper == '*') corp = '-' + s2; else corp = "-1/" + s2;
                    else if (Functie1.Corp == "0") { corp = "0"; Terminal = true; Operatie = new Operatie('0'); Functie1 = Functie2 = null; }
                    else if (Functie2.Corp == "0") { corp = "0"; Terminal = true; Operatie = new Operatie('0'); Functie1 = Functie2 = null; }
                    else corp = s1 + UnarOperat + s2;
                }
                else if (oper == '^')
                {
                    if (Functie1.Operatie == '+' || Functie1.Operatie == '-' || Functie1.Operatie == '*' || Functie1.Operatie == '/')
                    {
                        s1 = CorpulPrincipal(s1);
                        s1 = "(" + s1 + ")";
                    }
                    if (Functie2.Operatie == '+' || Functie2.Operatie == '-' || Functie2.Operatie == '*' || Functie2.Operatie == '/')
                    {
                        s2 = CorpulPrincipal(s2);
                        s2 = "(" + s2 + ")";
                    }
                    if (Functie1.Terminal && !Functie1.XTerminal && Functie2.Terminal && !Functie2.XTerminal)
                    {
                        if (oper == '^') ValoareTerminal = (float)System.Math.Pow(Functie1.ValoareTerminal, Functie2.ValoareTerminal);
                        corp = ValoareTerminal.ToString();
                        Terminal = true;
                        Operatie = new Operatie('0');
                        Functie1 = Functie2 = null;
                    }
                    else if (Functie2.Corp == "0")
                    { corp = "1"; ValoareTerminal = 1; Operatie = new Operatie('0'); Terminal = true; Functie1 = Functie2 = null; }
                    else if (Functie2.Corp == "1")
                    {
                        corp = s1;
                        Operatie = Functie1.Operatie;
                        if (Functie1.Terminal)
                        {
                            if (Functie1.XTerminal) XTerminal = true;
                            Operatie = new Operatie('X');
                            Terminal = true;
                            Functie1 = Functie2 = null;
                        }
                        else
                        {
                            Functie2 = Functie1.Functie2;
                            Subfunctii = new List<Functie>(Functie1.Subfunctii);
                            Functie1 = Functie1.Functie1;
                            return;
                        }
                    }
                    else if (Functie1.Corp == "0") { corp = "0"; Terminal = true; Operatie = new Operatie('0'); Functie1 = Functie2 = null; }
                    else if (Functie1.Corp == "1") { corp = "1"; Terminal = true; Operatie = new Operatie('0'); Functie1 = Functie2 = null; }
                    else corp = s1 + UnarOperat + s2;
                }

                //  1+1 ... + ^ ... + 2*3 -> 1+(1^2)*3 != (1+1)^(2*3)
            }
            else if (oper != '0')
            {
                if (oper == '-') { UnarOperat = "-"; if (Functie1.Terminal && !Functie1.XTerminal) { Import(new Functie(-Functie1.ValoareTerminal)); } }// Terminal = true; Operatie = new Operatie('0'); ValoareTerminal = -Functie1.ValoareTerminal; corp = ValoareTerminal.ToString();  Functie1 = Functie2 = null;} }
                else if (oper == 's') { UnarOperat = "sin("; if (Functie1.Terminal && !Functie1.XTerminal) { Terminal = true; Operatie = new Operatie('0'); ValoareTerminal = (float)Math.Sin(Functie1.ValoareTerminal); corp = ConstantCorp(ValoareTerminal); Functie1 = Functie2 = null; } }
                else if (oper == 'c') { UnarOperat = "cos("; if (Functie1.Terminal && !Functie1.XTerminal) { Terminal = true; Operatie = new Operatie('0'); ValoareTerminal = (float)Math.Cos(Functie1.ValoareTerminal); corp = ConstantCorp(ValoareTerminal); Functie1 = Functie2 = null; } }
                else if (oper == 't') { UnarOperat = "tg("; if (Functie1.Terminal && !Functie1.XTerminal) { Terminal = true; Operatie = new Operatie('0'); ValoareTerminal = (float)Math.Tan(Functie1.ValoareTerminal); corp = ConstantCorp(ValoareTerminal); Functie1 = Functie2 = null; } }
                else if (oper == 'u') { UnarOperat = "ctg("; if (Functie1.Terminal && !Functie1.XTerminal) { Terminal = true; Operatie = new Operatie('0'); double tg = Math.Tan(Functie1.ValoareTerminal); if (tg != 0) { ValoareTerminal = (float)(1 / Math.Tan(Functie1.ValoareTerminal)); corp = ConstantCorp(ValoareTerminal); } else { ValoareTerminal = float.NaN; corp = "NaN"; } Functie1 = Functie2 = null; } }
                else if (oper == 'S') { UnarOperat = "arcsin("; if (Functie1.Terminal && !Functie1.XTerminal) { Terminal = true; Operatie = new Operatie('0'); ValoareTerminal = (float)Math.Asin(Functie1.ValoareTerminal); corp = ConstantCorp(ValoareTerminal); Functie1 = Functie2 = null; } }
                else if (oper == 'C') { UnarOperat = "arccos("; if (Functie1.Terminal && !Functie1.XTerminal) { Terminal = true; Operatie = new Operatie('0'); ValoareTerminal = (float)Math.Acos(Functie1.ValoareTerminal); corp = ConstantCorp(ValoareTerminal); Functie1 = Functie2 = null; } }
                else if (oper == 'T') { UnarOperat = "arctg("; if (Functie1.Terminal && !Functie1.XTerminal) { Terminal = true; Operatie = new Operatie('0'); ValoareTerminal = (float)Math.Atan(Functie1.ValoareTerminal); corp = ConstantCorp(ValoareTerminal); Functie1 = Functie2 = null; } }
                else if (oper == 'U') { UnarOperat = "arcctg("; if (Functie1.Terminal && !Functie1.XTerminal) { Terminal = true; Operatie = new Operatie('0'); double tg = Math.Atan(Functie1.ValoareTerminal); if (tg != 0) { ValoareTerminal = (float)((float)System.Math.PI / 2 - Math.Atan(Functie1.ValoareTerminal)); corp = ConstantCorp(ValoareTerminal); } else { ValoareTerminal = float.NaN; corp = "NaN"; } Functie1 = Functie2 = null; } }
                else if (oper == 'q') { UnarOperat = "sqrt("; if (Functie1.Terminal && !Functie1.XTerminal) { Terminal = true; Operatie = new Operatie('0'); ValoareTerminal = (float)Math.Sqrt(Functie1.ValoareTerminal); corp = ConstantCorp(ValoareTerminal); Functie1 = Functie2 = null; } }
                else if (oper == 'n') { UnarOperat = "ln("; if (Functie1.Terminal && !Functie1.XTerminal) { Terminal = true; Operatie = new Operatie('0'); ValoareTerminal = (float)Math.Log(Functie1.ValoareTerminal); corp = ConstantCorp(ValoareTerminal); Functie1 = Functie2 = null; } }
                else if (oper == 'g')
                {
                    UnarOperat = "lg("; if (Functie1.Terminal && !Functie1.XTerminal) { Terminal = true; Operatie = new Operatie('0'); ValoareTerminal = (float)Math.Log10(Functie1.ValoareTerminal); corp = ConstantCorp(ValoareTerminal); Functie1 = Functie2 = null; }
                }
                if (Terminal == false) { corp = UnarOperat + s1; if (oper != '-') corp += ")"; }
            }
            InitializeazaListaSubfunctii();
        }

        string ConstantCorp(float ValTerm)
        {
            if (ValTerm == float.Parse(Convert.ToSingle(Math.E).ToString()))
                return "e";
            else if (ValTerm == float.Parse(Convert.ToSingle(Math.PI).ToString()))
                return "π";
            else
                return ValTerm.ToString();
        }
        public Functie(float term)
        {
            XTerminal = false;
            ValoareTerminal = term;
            Operatie = new Operatie('0');
            Functie1 = Functie2 = null;
            Corp = term.ToString();
        }

        public Functie(char oper, List<Functie> Elemente)
        {
            if (Elemente.Count == 1)
            {
                Import(Elemente[0]);
            }
            else
            {
                Subfunctii = new List<Functie>(Elemente);
                Operatie = new Operatie(oper);
            }
            ReconstruiesteCorp();
            SimplifyIdentities();
            ReconstruiesteCorp();
        }
        //Functii Principale
        void Creeaza(string a)
        {
            DefinesteOperatia(a);
            if (Operatie == '0' || Operatie == 'X')
            {
                Terminal = true;
                if (a == "x")
                {
                    XTerminal = true;
                    Operatie = new Operatie('X');
                }
                else if (a == "e")
                {
                    ETerminal = true;
                    Operatie = new Operatie('E');
                }
                else if (a == "π")
                {
                    PITerminal = true;
                    Operatie = new Operatie('P');
                }
                else ValoareTerminal = float.Parse(a);
            }
            else if ((Operatie == '-' && MinusESemn) || Trigonometrica)
            {
                string s = a.Remove(0, PozitieOperatie + 1);
                Functie1 = new Functie(s);
            }
            else
            {
                string s1 = a;
                string s2 = a;
                s1 = s1.Remove(PozitieOperatie);
                s2 = s2.Remove(0, PozitieOperatie + 1);
                Functie1 = new Functie(s1);
                Functie2 = new Functie(s2);
            }
            InitializeazaListaSubfunctii();
            if (a.IndexOf("x") == -1)
            {//DONE Posibil inlocuibila cu Executa(0) fara alte conditii ??
                ValoareTerminal = Executa(0);
                if (ValoareTerminal == float.Parse(Convert.ToSingle(Math.E).ToString()))
                    corp = "e";
                else if (ValoareTerminal == float.Parse(Convert.ToSingle(Math.PI).ToString()))
                    corp = "π";
                else
                    corp = ValoareTerminal.ToString();
                Terminal = true;
                XTerminal = false;
                MinusESemn = false;
                Operatie = new Operatie('0');
            }
            // COMPLETARE: functii inteligente de reducere analoage celorlalti constructori

        }
        public Functie Deriveaza()
        {
            if (Operatie == '0')
            {
                Derivata = new Functie(0);
                return Derivata;
            }
            if (Operatie == 'X')
            {
                Derivata = new Functie(1);
                return Derivata;
            }
            Subderivate = new List<Functie>();
            for (int i = 0; i < Subfunctii.Count; i++)
            {
                Subderivate.Add(Subfunctii[i].Deriveaza());
            }
            switch (Operatie.OPERATIE)
            {
                case '-':
                    if (MinusESemn)
                        Derivata = new Functie('-', Subderivate[0].Fork());
                    else
                        Derivata = new Functie('-', Subderivate[0].Fork(), Subderivate[1].Fork());
                    break;
                case '+':
                    Derivata = new Functie('+', Subderivate);
                    break;
                case '*':
                    List<Functie> Suma = new List<Functie>();
                    for (int i = 0; i < Subderivate.Count; i++)
                    {
                        List<Functie> Produs = new List<Functie>();
                        for (int j = 0; j < Subderivate.Count; j++)
                        {
                            if (i != j) Produs.Add(Subfunctii[j].Fork());
                            else Produs.Add(Subderivate[j].Fork());
                        }
                        Suma.Add(new Functie('*', Produs));
                    }
                    Derivata = new Functie('+', Suma);
                    break;
                case '/':
                    Derivata = new Functie('/', new Functie('-', new Functie('*', Subderivate[0], Subfunctii[1].Fork()), new Functie('*', Subderivate[1].Fork(), Subfunctii[0].Fork())), new Functie('^', Subfunctii[1].Fork(), new Functie(2)));
                    break;
                case '^':
                    if (Subfunctii[0].Operatie == '0' && !(Subfunctii[1].Operatie == '0'))
                        Derivata = new Functie('*', this, new Functie('n', Subfunctii[0].Fork()));
                    else if (!(Subfunctii[0].Operatie == '0') && (Subfunctii[1].Operatie == '0'))
                        Derivata = new Functie('*', Subfunctii[1].Fork(), new Functie('*', new Functie('^', Subfunctii[0].Fork(), new Functie('-', Subfunctii[1].Fork(), new Functie(1))), Subderivate[0].Fork()));
                    else Derivata = new Functie('*', new Functie('^', Subfunctii[0].Fork(), Subfunctii[1].Fork()), new Functie('+', new Functie('/', new Functie('*', Subderivate[0].Fork(), Subfunctii[1].Fork()), Subfunctii[0].Fork()), new Functie('*', Subderivate[1].Fork(), new Functie('n', Subfunctii[0].Fork()))));
                    break;
                case 's':
                    Derivata = new Functie('*', new Functie('c', Subfunctii[0].Fork()), Subderivate[0].Fork());
                    break;
                case 'c':
                    Derivata = new Functie('*', new Functie('-', new Functie('s', Subfunctii[0].Fork())), Subderivate[0].Fork());
                    break;
                case 't':
                    Derivata = new Functie('*', new Functie('/', new Functie(1), new Functie('^', new Functie('c', Subfunctii[0].Fork()), new Functie(2))), Subderivate[0].Fork());
                    break;
                case 'u':
                    Derivata = new Functie('*', new Functie('/', new Functie(-1), new Functie('^', new Functie('s', Subfunctii[0].Fork()), new Functie(2))), Subderivate[0].Fork());
                    break;
                case 'S':
                    Derivata = new Functie('*', new Functie('/', new Functie(1), new Functie('q', new Functie('-', new Functie(1), new Functie('^', Subfunctii[0].Fork(), new Functie(2))))), Subderivate[0].Fork());
                    break;
                case 'C':
                    Derivata = new Functie('*', new Functie('/', new Functie(-1), new Functie('q', new Functie('-', new Functie(1), new Functie('^', Subfunctii[0].Fork(), new Functie(2))))), Subderivate[0].Fork());
                    break;
                case 'T':
                    Derivata = new Functie('*', new Functie('/', new Functie(1), new Functie('+', new Functie(1), new Functie('^', Subfunctii[0].Fork(), new Functie(2)))), Subderivate[0].Fork());
                    break;
                case 'U':
                    Derivata = new Functie('*', new Functie('/', new Functie(-1), new Functie('+', new Functie(1), new Functie('^', Subfunctii[0].Fork(), new Functie(2)))), Subderivate[0].Fork());
                    break;
                case 'q':
                    Derivata = new Functie('*', new Functie('/', new Functie(1), new Functie('*', new Functie('q', Subfunctii[0].Fork()), new Functie(2))), Subderivate[0].Fork());
                    break;
                case 'n':
                    Derivata = new Functie('*', new Functie('/', new Functie(1), Subfunctii[0].Fork()), Subderivate[0].Fork());
                    break;
                case 'g':
                    Derivata = new Functie('*', new Functie('/', new Functie(1), new Functie('*', Subfunctii[0].Fork(), new Functie((float)Math.Log(10)))), Subderivate[0].Fork());
                    break;
            }
            return Derivata;
        }
        public float Executa(float x)
        {
            LastValue = IntermediarValue;
            IntermediarValue = Exec(x);
            return IntermediarValue;
        }
        public float Exec(float x)
        {
            if (Operatie == 'X')
                return x;
            else if (Operatie == '0')
            {
                return ValoareTerminal;
            }
            else if (Operatie == 'E')
            {
                return ValoareTerminal = System.Convert.ToSingle(System.Math.E);
            }
            else if (Operatie == 'P')
            {
                return ValoareTerminal = System.Convert.ToSingle(System.Math.PI);
            }
            ValoriSubfunctii = new List<float>();
            for (int i = 0; i < Subfunctii.Count; i++)
            {
                ValoriSubfunctii.Add(Subfunctii[i].Executa(x));
                if (ValoriSubfunctii[i] == float.NaN) return float.NaN;
            }
            float val1 = 0, val2 = 0, val;
            if (Functie1 != null) { val1 = Functie1.Executa(x); }
            if (Functie2 != null) { val2 = Functie2.Executa(x); }
            switch (Operatie.OPERATIE)
            {
                case '-':
                    if (MinusESemn)
                        return -ValoriSubfunctii[0];
                    return ValoriSubfunctii[0] - ValoriSubfunctii[1];
                case '+':
                    val = 0;
                    for (int i = 0; i < ValoriSubfunctii.Count; i++)
                        val += ValoriSubfunctii[i];
                    return val;
                case '*':
                    val = 1;
                    for (int i = 0; i < ValoriSubfunctii.Count; i++)
                        val *= ValoriSubfunctii[i];
                    return val;
                case '/':
                    if (ValoriSubfunctii[1] == 0) return float.NaN;
                    if (!float.IsNaN(Subfunctii[1].LastValue) && (0 > Subfunctii[1].LastValue) != (0 > ValoriSubfunctii[1]))
                        return float.NaN;
                    else return ValoriSubfunctii[0] / ValoriSubfunctii[1];
                case '^':
                    float CurrentValue = System.Convert.ToSingle(System.Math.Pow(ValoriSubfunctii[0], ValoriSubfunctii[1]));
                    if (!float.IsNaN(Subfunctii[0].LastValue) && ValoriSubfunctii[1] < 0 && (0 > Subfunctii[0].LastValue) != (0 > ValoriSubfunctii[0]))
                        return float.NaN;
                    return CurrentValue;
                case 's':
                    return System.Convert.ToSingle(System.Math.Sin(ValoriSubfunctii[0]));
                case 'c':
                    return System.Convert.ToSingle(System.Math.Cos(ValoriSubfunctii[0]));
                case 't':
                    CurrentValue = System.Convert.ToSingle(System.Math.Tan(ValoriSubfunctii[0]));
                    if (!float.IsNaN(LastValue) && (CurrentValue > LastValue) != (ValoriSubfunctii[0] > Subfunctii[0].LastValue))
                        return float.NaN;
                    return CurrentValue;
                case 'u':
                    float CurrentValueCtg;
                    float tan = System.Convert.ToSingle(System.Math.Tan(ValoriSubfunctii[0]));
                    if (tan != 0) CurrentValueCtg = 1 / tan;
                    else CurrentValueCtg = float.NaN;
                    if (!float.IsNaN(LastValue) && (CurrentValueCtg > LastValue) == (ValoriSubfunctii[0] > Subfunctii[0].LastValue))
                        return float.NaN;
                    return CurrentValueCtg;
                case 'S':
                    return System.Convert.ToSingle(System.Math.Asin(ValoriSubfunctii[0]));
                case 'C':
                    return System.Convert.ToSingle(System.Math.Acos(ValoriSubfunctii[0]));
                case 'T':
                    return System.Convert.ToSingle(System.Math.Atan(ValoriSubfunctii[0]));
                case 'U':
                    float Atan = System.Convert.ToSingle(System.Math.Atan(ValoriSubfunctii[0]));
                    return (float)System.Math.PI / 2 - Atan;
                case 'q':
                    if (ValoriSubfunctii[0] >= 0)
                        return (float)Math.Sqrt(ValoriSubfunctii[0]);
                    else return float.NaN;
                case 'n':
                    if (ValoriSubfunctii[0] > 0) return (float)Math.Log(ValoriSubfunctii[0]);
                    else return float.NaN;
                case 'g':
                    if (ValoriSubfunctii[0] > 0) return (float)Math.Log10(ValoriSubfunctii[0]);
                    else return float.NaN;
            }
            return 0;
        }
        int DefinesteOperatia(string s)
        {
            int i = s.Length - 1;
            Operatie = new Operatie('0');
            // EVENTUALITATE: while: i>s.lenght => nu se mai verifica s[i++]..... deci nu se incrementeaza i => (\n) {X} s[i-1] != s[i] {V}
            // CRITIC: Fiecare Pas este verificat de while peana gaseste o paranteza. Atunci break indiferent daca mai exsta psi dupa;
            while (i > 0 && (s[i] != '^' && InExteriorulParantezelor(i, s) || !InExteriorulParantezelor(i, s))) i--;
            if (s[i] == '^')
            { Operatie = new Operatie('^'); PozitieOperatie = i; }
            i = s.Length - 1;
            while (i > 0 && (s[i] != '/' && InExteriorulParantezelor(i, s) || !InExteriorulParantezelor(i, s))) i--;
            if (s[i] == '/') { Operatie = new Operatie('/'); PozitieOperatie = i; }
            i = 0;
            while (i < s.Length - 1 && (s[i] != '*' && InExteriorulParantezelor(i, s) || !InExteriorulParantezelor(i, s))) i++;
            if (s[i] == '*') { Operatie = new Operatie('*'); PozitieOperatie = i; }
            i = s.Length - 1;
            while (i > 0) if (s[i] == '-' && InExteriorulParantezelor(i, s) && !MinusEsteSemn(s, i)) { Operatie = new Operatie('-'); PozitieOperatie = i; MinusESemn = false; break; } else i--;
            i = 0;
            while (i < s.Length - 1 && (s[i] != '+' && InExteriorulParantezelor(i, s) || !InExteriorulParantezelor(i, s))) i++;
            if (s[i] == '+') { Operatie = new Operatie('+'); PozitieOperatie = i; }
            if (Operatie == '0' || Operatie == '^')//Operatie == '^': -x^2 -> -(x^2) adica daca operatia principala gasita este '^' minus semn are prioritate
            {
                //Posibil ca - sa nu fie identificat din cauza conditiei de operad
                i = 0;
                while (i < s.Length - 1 && (s[i] != '-' && InExteriorulParantezelor(i, s) || !InExteriorulParantezelor(i, s))) i++;
                if (s[i] == '-' && (i > 0 && s[i - 1] != '^' || i == 0))//DOAR DACA PE POS ANTERIOARA NUA AVEM ^ CONTINUAM
                { Operatie = new Operatie('-'); PozitieOperatie = i; MinusESemn = true; }
            }
            if (Operatie == '0')
            {
                //Avem trigonometrica/radical/logaritm
                //CRITIC: Adauga conditia InExteriorulParantezelor() //Sau nu. Daca le ia de la sfarsit la inceput, automat functia ramasa este inafara oricarei paranteze fiinca verificarile sunt facute daca nu exista alte operatii
                for (i = s.Length - 1; i > 0; i--)
                {
                    if (i < s.Length - 1 && s[i] == 't' && i > 1 && s[i - 2] == 'q')
                    {
                        Operatie = new Operatie('q');
                        PozitieOperatie = i;
                        Trigonometrica = true;
                    }
                    if (i < s.Length - 1 && s[i] == 'n')
                    {
                        if (i > 1 && s[i - 2] == 's')
                        {
                            if (i >= 5 && s[i - 3] == 'c')
                            { Operatie = new Operatie('S'); PozitieOperatie = i; Trigonometrica = true; }//  i -= arcsin(i);
                            else if (s[i - 2] == 's') { Operatie = new Operatie('s'); PozitieOperatie = i; Trigonometrica = true; }
                        }
                        else if (s[i - 1] == 'l')
                        { Operatie = new Operatie('n'); PozitieOperatie = i; Trigonometrica = true; }
                    }
                    else if (i < s.Length - 1 && s[i] == 's')
                    {
                        if (i >= 5 && s[i - 3] == 'c')
                        { Operatie = new Operatie('C'); PozitieOperatie = i; Trigonometrica = true; }// arccos(i);
                        else if (s[i - 2] == 'c') { Operatie = new Operatie('c'); PozitieOperatie = i; Trigonometrica = true; }
                    }
                    if (i < s.Length - 1 && s[i] == 'g')
                    {
                        if (s[i - 1] == 't')
                        {
                            if (i > 1 && s[i - 2] == 'c')
                            {
                                if (i > 2 && s[i - 3] == 'r')
                                { Operatie = new Operatie('T'); PozitieOperatie = i; Trigonometrica = true; }
                                else if (i > 2 && s[i - 3] == 'c')
                                { Operatie = new Operatie('U'); PozitieOperatie = i; Trigonometrica = true; }
                                else { Operatie = new Operatie('u'); PozitieOperatie = i; Trigonometrica = true; }

                            }
                            else { Operatie = new Operatie('t'); PozitieOperatie = i; Trigonometrica = true; }
                        }
                        else { Operatie = new Operatie('g'); PozitieOperatie = i; Trigonometrica = true; }
                    }
                }
            }
            return PozitieOperatie;
        }
        bool FloatTerminal()
        {
            return Terminal && !XTerminal;
        }
        void SimplifyIdentities()
        {
            SorteazaLista();
            SimplificaNetriviale();
            bool HaveToAddMinus = false;
            if (Operatie == '+' || Operatie == '*')
            {
                for (int i = 0; i < Subfunctii.Count; i++)
                {
                    if (Operatie == '*')//Eliminam toate minusurile si adaugam doar unul la final
                    {
                        if (Subfunctii[i].MinusESemn)
                        {
                            HaveToAddMinus = !HaveToAddMinus;
                            Subfunctii[i].Import(Subfunctii[i].Subfunctii[0]);
                        }
                        else if (Subfunctii[i].FloatTerminal() && Subfunctii[i].ValoareTerminal < 0)
                        {
                            HaveToAddMinus = !HaveToAddMinus;
                            Subfunctii[i] = new Functie(-Subfunctii[i].ValoareTerminal);
                        }
                    }
                    if (Subfunctii[i].Operatie.Value == Operatie.Value)
                    {
                        Subfunctii.InsertRange(i + 1, Subfunctii[i].Subfunctii);
                        Subfunctii.RemoveAt(i);
                        i--;
                    }
                }
                float TotalTerminals = Operatie.ElementNeutru();
                for (int i = 0; i < Subfunctii.Count; i++)
                {
                    if (Subfunctii[i].FloatTerminal())
                    {
                        if (Operatie.ElementIdentitate() != float.NaN && Subfunctii[i].ValoareTerminal == Operatie.ElementIdentitate())
                        {
                            Subfunctii.Clear();
                            Import(new Functie(Operatie.ElementIdentitate()));
                            goto Done;
                        }
                        TotalTerminals = Operatie.Execute(TotalTerminals, Subfunctii[i].ValoareTerminal);
                        Subfunctii.Remove(Subfunctii[i]);
                        i--;
                    }
                }
                if (TotalTerminals != Operatie.ElementNeutru())
                {
                    Subfunctii.Add(new Functie(TotalTerminals));
                }
                if (Subfunctii.Count == 0)
                {
                    //Subfunctii = new List<Functie>();
                    Import(new Functie(Operatie.ElementNeutru()));
                }
                else if (Subfunctii.Count == 1)
                {
                    Import(Subfunctii[0]);
                }
                if (HaveToAddMinus)
                {
                    Functie tempRezult = new Functie(0);
                    tempRezult.Import(this);
                    Import(new Functie('-', tempRezult));
                }
            Done:;
            }
            else if (Operatie == '-' && !MinusESemn || Operatie == '/')
            {
                if (Subfunctii[1].FloatTerminal() && Subfunctii[1].ValoareTerminal == Operatie.ElementNeutru())
                {
                    Functie temp = Subfunctii[0];
                    Import(temp);
                }
            }
            else if (Operatie == '^')
            {
                if (Subfunctii[1].FloatTerminal() && Subfunctii[1].ValoareTerminal == 0)
                {
                    Import(new Functie(1));
                }
                else if (Subfunctii[0].FloatTerminal() && Subfunctii[0].ValoareTerminal == 1)
                {
                    Import(new Functie(1));
                }
            }
            else if (MinusESemn)
            {
                if (Subfunctii[0].Operatie == '-' && Subfunctii[0].MinusESemn)
                    Import(Subfunctii[0].Subfunctii[0]);
            }
        }
        bool egal(Functie functie)
        {
            return !(this > functie || functie > this);
        }
        void SimplificaNetriviale()
        {

            if (Operatie == '/')
            {
                if (!(Subfunctii[0].FloatTerminal() && Subfunctii[0].ValoareTerminal == 1))
                {
                    if (Subfunctii[0].Operatie == '*' && Subfunctii[1].Operatie == '*')
                    {
                        for (int i = 0, j = 0; i < Subfunctii[0].Subfunctii.Count && j < Subfunctii[1].Subfunctii.Count;)
                        {
                            Functie RadacinaNumarator = null, RadacinaNumitor = null, ExponentNumarator = null, ExponentNumitor = null;
                            if (Subfunctii[0].Subfunctii[i].Operatie == '^') { RadacinaNumarator = Subfunctii[0].Subfunctii[i].Subfunctii[0]; ExponentNumarator = Subfunctii[0].Subfunctii[i].Subfunctii[1]; }
                            else { RadacinaNumarator = Subfunctii[0].Subfunctii[i]; ExponentNumarator = new Functie(1); }
                            if (Subfunctii[1].Subfunctii[j].Operatie == '^') { RadacinaNumitor = Subfunctii[1].Subfunctii[j].Subfunctii[0]; ExponentNumitor = Subfunctii[1].Subfunctii[j].Subfunctii[1]; }
                            else { RadacinaNumitor = Subfunctii[1].Subfunctii[j]; ExponentNumitor = new Functie(1); }

                            if (RadacinaNumarator > RadacinaNumitor) j++;
                            else if (RadacinaNumitor > RadacinaNumarator) i++;
                            else
                            {
                                Subfunctii[0].Subfunctii.RemoveAt(i);
                                Subfunctii[1].Subfunctii.RemoveAt(j);
                                Functie DeltaExponent = new Functie('-', ExponentNumarator.Fork(), ExponentNumitor.Fork());
                                // if (DeltaExponent.FloatTerminal() || DeltaExponent.MinusESemn)
                                {
                                    if (DeltaExponent.FloatTerminal() && DeltaExponent.ValoareTerminal < 0 || DeltaExponent.MinusESemn)
                                    {
                                        if (DeltaExponent.FloatTerminal()) DeltaExponent.ValoareTerminal = -DeltaExponent.ValoareTerminal;
                                        else DeltaExponent.Import(DeltaExponent.Subfunctii[0]);
                                        DeltaExponent.ReconstruiesteCorp();
                                        if (DeltaExponent.ValoareTerminal == 1)
                                        {
                                            Subfunctii[1].Subfunctii.Add(RadacinaNumitor);
                                        }
                                        else Subfunctii[1].Subfunctii.Add(new Functie('^', RadacinaNumitor, DeltaExponent));
                                    }
                                    else if (DeltaExponent.FloatTerminal() && DeltaExponent.ValoareTerminal > 0 || !DeltaExponent.FloatTerminal())
                                    {
                                        if (DeltaExponent.FloatTerminal() && DeltaExponent.ValoareTerminal == 1)
                                        {
                                            Subfunctii[0].Subfunctii.Add(RadacinaNumarator.Fork());
                                        }
                                        else Subfunctii[0].Subfunctii.Add(new Functie('^', RadacinaNumarator.Fork(), DeltaExponent.Fork()));
                                    }
                                    Subfunctii[0].SorteazaLista();
                                    Subfunctii[1].SorteazaLista();
                                    i = 0; j = 0;

                                }
                            }

                        }
                        if (Subfunctii[0].Subfunctii.Count == 0)
                            Subfunctii[0].Import(new Functie(1));
                        if (Subfunctii[1].Subfunctii.Count == 0)
                            Subfunctii[1].Import(new Functie(1));
                        if (Subfunctii[0].Subfunctii.Count == 1)
                            Subfunctii[0].Import(Subfunctii[0].Subfunctii[0]);
                        if (Subfunctii[1].Subfunctii.Count == 1)
                            Subfunctii[1].Import(Subfunctii[1].Subfunctii[0]);

                    }
                    else if (Subfunctii[0].Operatie == '*' && Subfunctii[1].Operatie != '*')
                    {
                        Functie RadacinaNumarator = null, RadacinaNumitor = null, ExponentNumarator = null, ExponentNumitor = null;
                        if (Subfunctii[1].Operatie == '^') { RadacinaNumitor = Subfunctii[1].Subfunctii[0]; ExponentNumitor = Subfunctii[1].Subfunctii[1]; }
                        else { RadacinaNumitor = Subfunctii[1]; ExponentNumitor = new Functie(1); }

                        for (int i = 0; i < Subfunctii[0].Subfunctii.Count;)
                        {
                            if (Subfunctii[0].Subfunctii[i].Operatie == '^') { RadacinaNumarator = Subfunctii[0].Subfunctii[i].Subfunctii[0]; ExponentNumarator = Subfunctii[0].Subfunctii[i].Subfunctii[1]; }
                            else { RadacinaNumarator = Subfunctii[0].Subfunctii[i]; ExponentNumarator = new Functie(1); }

                            if (RadacinaNumarator > RadacinaNumitor) break;
                            else if (RadacinaNumitor > RadacinaNumarator) i++;
                            else
                            {

                                Functie DeltaExponent = new Functie('-', ExponentNumarator.Fork(), ExponentNumitor.Fork());
                                if (DeltaExponent.FloatTerminal() && DeltaExponent.ValoareTerminal < 0 || DeltaExponent.MinusESemn)
                                {
                                    if (DeltaExponent.FloatTerminal()) DeltaExponent.ValoareTerminal = -DeltaExponent.ValoareTerminal;
                                    else DeltaExponent.Import(DeltaExponent.Subfunctii[0]);
                                    DeltaExponent.ReconstruiesteCorp();
                                    if (DeltaExponent.ValoareTerminal == 1)
                                    {
                                        Subfunctii[1].Import(RadacinaNumitor);
                                    }
                                    else Subfunctii[1].Import(new Functie('^', RadacinaNumitor, DeltaExponent));
                                    Subfunctii[0].Subfunctii.RemoveAt(i);
                                }
                                else
                                {
                                    Import(Subfunctii[0]);
                                    if (DeltaExponent.FloatTerminal() && DeltaExponent.ValoareTerminal == 1)
                                    {
                                        Subfunctii[i].Import(Subfunctii[i].Subfunctii[0].Fork());
                                    }
                                    else if (DeltaExponent.FloatTerminal() && DeltaExponent.ValoareTerminal == 0)
                                    {
                                        Subfunctii.RemoveAt(i);
                                    }
                                    else Subfunctii[i].Subfunctii[1].Import(DeltaExponent);
                                    SorteazaLista();
                                    i = 0;
                                }

                                break;
                            }
                        }

                    }
                    else if (Subfunctii[0].Operatie != '*' && Subfunctii[1].Operatie == '*')
                    {

                        Functie RadacinaNumarator = null, RadacinaNumitor = null, ExponentNumarator = null, ExponentNumitor = null;
                        if (Subfunctii[0].Operatie == '^') { RadacinaNumarator = Subfunctii[0].Subfunctii[0]; ExponentNumarator = Subfunctii[0].Subfunctii[1]; }
                        else { RadacinaNumarator = Subfunctii[0]; ExponentNumarator = new Functie(1); }

                        for (int i = 0; i < Subfunctii[1].Subfunctii.Count;)
                        {
                            if (Subfunctii[1].Subfunctii[i].Operatie == '^') { RadacinaNumitor = Subfunctii[1].Subfunctii[i].Subfunctii[0]; ExponentNumitor = Subfunctii[1].Subfunctii[i].Subfunctii[1]; }
                            else { RadacinaNumitor = Subfunctii[1].Subfunctii[i]; ExponentNumitor = new Functie(1); }

                            if (RadacinaNumitor > RadacinaNumarator) break;
                            else if (RadacinaNumarator > RadacinaNumitor) i++;
                            else
                            {
                                Functie DeltaExponent = new Functie('-', ExponentNumarator.Fork(), ExponentNumitor.Fork());
                                if (DeltaExponent.FloatTerminal() && DeltaExponent.ValoareTerminal < 0 || DeltaExponent.MinusESemn)
                                {
                                    Subfunctii[0].Import(new Functie(1));
                                    if (DeltaExponent.FloatTerminal()) DeltaExponent.ValoareTerminal = -DeltaExponent.ValoareTerminal;
                                    else DeltaExponent.Import(DeltaExponent.Subfunctii[0]);
                                    DeltaExponent.ReconstruiesteCorp();
                                    if (DeltaExponent.ValoareTerminal == 1)
                                    {
                                        Subfunctii[1].Subfunctii[i].Import(RadacinaNumitor);
                                    }
                                    else Subfunctii[1].Subfunctii[i].Import(new Functie('^', RadacinaNumitor, DeltaExponent));
                                    Subfunctii[1].SorteazaLista();
                                    i = 0;
                                }
                                else
                                {
                                    if (DeltaExponent.FloatTerminal() && DeltaExponent.ValoareTerminal == 1)
                                    {
                                        Subfunctii[0].Import(RadacinaNumarator);
                                        Subfunctii[1].Subfunctii.RemoveAt(i);
                                    }
                                    else if (DeltaExponent.FloatTerminal() && DeltaExponent.ValoareTerminal == 0)
                                    {
                                        Subfunctii[0].Import(new Functie(1));
                                        Subfunctii[1].Subfunctii.RemoveAt(i);
                                    }
                                    else
                                    {
                                        Subfunctii[0].Import(new Functie('^', RadacinaNumarator.Fork(), DeltaExponent.Fork()));
                                        Subfunctii[1].Subfunctii.RemoveAt(i);
                                    }
                                }
                                break;
                            }
                        }
                        if (Subfunctii[1].Subfunctii.Count == 0)
                            Subfunctii[1].Import(new Functie(1));
                        if (Subfunctii[1].Subfunctii.Count == 1)
                            Subfunctii[1].Import(Subfunctii[1].Subfunctii[0]);

                    }
                    else if (Subfunctii[0].Operatie != '*' && Subfunctii[1].Operatie != '*')
                    {
                        Functie RadacinaNumarator = null, RadacinaNumitor = null, ExponentNumarator = null, ExponentNumitor = null;
                        if (Subfunctii[0].Operatie == '^') { RadacinaNumarator = Subfunctii[0].Subfunctii[0]; ExponentNumarator = Subfunctii[0].Subfunctii[1]; }
                        else { RadacinaNumarator = Subfunctii[0]; ExponentNumarator = new Functie(1); }
                        if (Subfunctii[1].Operatie == '^') { RadacinaNumitor = Subfunctii[1].Subfunctii[0]; ExponentNumitor = Subfunctii[1].Subfunctii[1]; }
                        else { RadacinaNumitor = Subfunctii[1]; ExponentNumitor = new Functie(1); }

                        if (!(RadacinaNumarator > RadacinaNumitor || RadacinaNumitor > RadacinaNumarator))
                        {
                            Functie DeltaExponent = new Functie('-', ExponentNumarator.Fork(), ExponentNumitor.Fork());
                            Import(new Functie('^', RadacinaNumarator.Fork(), DeltaExponent));
                        }

                    }
                }

            }
            else if (Operatie == '*')
            {
                for (int i = 0, j = 0; i < Subfunctii.Count && j < Subfunctii.Count;)
                {
                    Functie RadacinaNumarator = null, RadacinaNumitor = null, ExponentNumarator = null, ExponentNumitor = null;
                    if (Subfunctii[i].Operatie == '^') { RadacinaNumarator = Subfunctii[i].Subfunctii[0]; ExponentNumarator = Subfunctii[i].Subfunctii[1]; }
                    else { RadacinaNumarator = Subfunctii[i]; ExponentNumarator = new Functie(1); }
                    if (Subfunctii[j].Operatie == '^') { RadacinaNumitor = Subfunctii[j].Subfunctii[0]; ExponentNumitor = Subfunctii[j].Subfunctii[1]; }
                    else { RadacinaNumitor = Subfunctii[j]; ExponentNumitor = new Functie(1); }

                    if (i == j) { i++; continue; }
                    else if (RadacinaNumarator > RadacinaNumitor) j++;
                    else if (RadacinaNumitor > RadacinaNumarator) i++;
                    else
                    {
                        Subfunctii[i].Import(new Functie('^', RadacinaNumarator.Fork(), new Functie('+', ExponentNumarator.Fork(), ExponentNumitor.Fork())));
                        Subfunctii.RemoveAt(j);
                        SorteazaLista();
                        i = 0; j = 0;
                    }

                }
                if (Subfunctii.Count == 1)
                    Import(Subfunctii[0]);
            }
            ////////////////////////////////////////////////////////////////Aditiv
            else if (Operatie == '-')
            {
                if (!(Subfunctii[0].FloatTerminal() && Subfunctii[0].ValoareTerminal == 1))
                {
                    if (Subfunctii[0].Operatie == '+' && Subfunctii[1].Operatie == '+')
                    {
                        for (int i = 0, j = 0; i < Subfunctii[0].Subfunctii.Count && j < Subfunctii[1].Subfunctii.Count;)
                        {/*
                            Tuple<Functie, Functie, Functie> fc = FactorComun(Subfunctii[0].Subfunctii[i], Subfunctii[1].Subfunctii[j]);
                            Functie factorComun = fc.Item1;
                            Functie remainingA = fc.Item2;
                            Functie remainingB = fc.Item3;
                            if (factorComun != null)
                            {
                                Subfunctii[0].Subfunctii[i].Import(new Functie('*', factorComun, new Functie('-', remainingA, remainingB)));
                                if (Subfunctii[0].Subfunctii[i].egal(new Functie(0)))
                                    Subfunctii[0].Subfunctii.RemoveAt(i);
                                Subfunctii[1].Subfunctii.RemoveAt(j);
                                if (Subfunctii[1].Subfunctii.Count == 0) break;
                                i = j = 0; continue;
                            }*/

                            if (Subfunctii[0].Subfunctii[i] > Subfunctii[1].Subfunctii[j]) j++;
                            else if (Subfunctii[1].Subfunctii[j] > Subfunctii[0].Subfunctii[i]) i++;
                            else
                            {
                                Subfunctii[0].Subfunctii.RemoveAt(i);
                                Subfunctii[1].Subfunctii.RemoveAt(j);
                            }

                        }
                        if (Subfunctii[0].Subfunctii.Count == 0)
                            Subfunctii[0].Import(new Functie(0));
                        if (Subfunctii[1].Subfunctii.Count == 0)
                            Subfunctii[1].Import(new Functie(0));
                        if (Subfunctii[0].Subfunctii.Count == 1)
                            Subfunctii[0].Import(Subfunctii[0].Subfunctii[0]);
                        if (Subfunctii[1].Subfunctii.Count == 1)
                            Subfunctii[1].Import(Subfunctii[1].Subfunctii[0]);

                    }
                    else if (Subfunctii[0].Operatie == '+' && Subfunctii[1].Operatie != '+')
                    {

                        for (int i = 0; i < Subfunctii[0].Subfunctii.Count;)
                        {
                            if (Subfunctii[0].Subfunctii[i] > Subfunctii[1]) break;
                            else if (Subfunctii[1] > Subfunctii[0].Subfunctii[i]) i++;
                            else
                            {
                                Import(Subfunctii[0]);
                                Subfunctii.RemoveAt(i);
                                break;
                            }
                        }

                    }
                    else if (Subfunctii[0].Operatie != '+' && Subfunctii[1].Operatie == '+')
                    {

                        for (int i = 0; i < Subfunctii[1].Subfunctii.Count;)
                        {
                            if (Subfunctii[1].Subfunctii[i] > Subfunctii[0]) break;
                            else if (Subfunctii[0] > Subfunctii[1].Subfunctii[i]) i++;
                            else
                            {
                                Subfunctii[0].Import(new Functie(0));
                                Subfunctii[1].Subfunctii.RemoveAt(i);
                                break;
                            }
                        }
                        if (Subfunctii[1].Subfunctii.Count == 0)
                            Subfunctii[1].Import(new Functie(0));
                        if (Subfunctii[1].Subfunctii.Count == 1)
                            Subfunctii[1].Import(Subfunctii[1].Subfunctii[0]);

                    }
                    else if (Subfunctii[0].Operatie != '+' && Subfunctii[1].Operatie != '+')
                    {
                        if (Subfunctii[0].egal(Subfunctii[1]))
                        {
                            Import(new Functie(0));
                        }

                    }
                }

            }
        }
        Tuple<Functie, Functie, Functie> FactorComun(Functie a, Functie b)//Returneaza factorul comun, ce ramane din prima functie , ce ramane din a doua functie
        {
            Functie factorComun = null, aDif = a.Fork(), bDif = b.Fork();
            List<Functie> elementeComune = new List<Functie>();
            List<Functie> elementeA;
            if (aDif.Operatie == '*') elementeA = aDif.Subfunctii;
            else
            {
                elementeA = new List<Functie>();
                elementeA.Add(a);
            }
            List<Functie> elementeB;
            if (bDif.Operatie == '*') elementeB = bDif.Subfunctii;
            else
            {
                elementeB = new List<Functie>();
                elementeB.Add(a);
            }

            for (int i = 0; i < elementeA.Count; i++)
            {
                for (int j = i; j < elementeB.Count; j++)
                {

                    Functie RadacinaNumarator = null, RadacinaNumitor = null, ExponentNumarator = null, ExponentNumitor = null;
                    if (elementeA[i].Operatie == '^') { RadacinaNumarator = elementeA[i].Subfunctii[0]; ExponentNumarator = elementeA[i].Subfunctii[1]; }
                    else { RadacinaNumarator = elementeA[i]; ExponentNumarator = new Functie(1); }
                    if (elementeB[j].Operatie == '^') { RadacinaNumitor = elementeB[j].Subfunctii[0]; ExponentNumitor = elementeB[j].Subfunctii[1]; }
                    else { RadacinaNumitor = elementeB[j]; ExponentNumitor = new Functie(1); }

                    if (RadacinaNumarator.egal(RadacinaNumitor))
                    {
                        bool Found = false;
                        foreach (var element in elementeComune)
                        {
                            Functie RadacinaNumaratorComun = null, ExponentNumaratorComun = null;
                            if (element.Operatie == '^') { RadacinaNumaratorComun = element.Subfunctii[0]; ExponentNumaratorComun = element.Subfunctii[1]; }
                            else { RadacinaNumaratorComun = element; ExponentNumaratorComun = new Functie(1); }
                            if (RadacinaNumaratorComun.egal(RadacinaNumitor))
                            {
                                Found = true;
                                Functie min;
                                if (ExponentNumarator.FloatTerminal() && ExponentNumitor.FloatTerminal())
                                {
                                    min = new Functie(Math.Max(ExponentNumarator.ValoareTerminal, ExponentNumitor.ValoareTerminal) - Math.Min(ExponentNumarator.ValoareTerminal, ExponentNumitor.ValoareTerminal));
                                }
                                else if (ExponentNumarator < ExponentNumitor)
                                {
                                    min = new Functie('-', ExponentNumitor, ExponentNumarator);
                                }
                                else min = new Functie('-', ExponentNumarator, ExponentNumitor);

                                element.Import(new Functie('^', RadacinaNumaratorComun, new Functie('+', ExponentNumaratorComun, min)));
                                elementeA[i].Import(new Functie('^', RadacinaNumarator, new Functie('+', ExponentNumarator, min)));
                                elementeB[i].Import(new Functie('^', RadacinaNumitor, new Functie('+', ExponentNumitor, min)));
                            }
                        }
                        if (!Found)
                        {
                            Functie RadacinaNumaratorComun = RadacinaNumarator.Fork();
                            Functie min;
                            if (ExponentNumarator.FloatTerminal() && ExponentNumitor.FloatTerminal())
                            {
                                min = new Functie(Math.Min(ExponentNumarator.ValoareTerminal, ExponentNumitor.ValoareTerminal));
                            }
                            else if (ExponentNumarator < ExponentNumitor)
                            {
                                min =  ExponentNumarator;
                            }
                            else min = ExponentNumitor;

                            elementeComune.Add(new Functie('^', RadacinaNumaratorComun, min));
                            elementeA[i].Import(new Functie('^', RadacinaNumarator, new Functie('-', ExponentNumarator,min)));
                            elementeB[i].Import(new Functie('^', RadacinaNumitor, new Functie('-', ExponentNumitor,min)));
                        }
                    }
                }
            }
            if (elementeComune.Count > 0)
                factorComun = new Functie('*', elementeComune);
            else factorComun = null;
            if (elementeA.Count > 0)
                aDif = new Functie('*', elementeA);
            else aDif = null;
            if (elementeB.Count > 0)
                bDif = new Functie('*', elementeB);
            else bDif = null;
            return new Tuple<Functie, Functie, Functie>(factorComun, aDif, bDif);

        }
        void InitializeazaListaSubfunctii()
        {//1/(sqrt(sin(x)^2)*2)*2*sin(x)
         //1/(sqrt(sin(x)*sin(x))*2)*(cos(x)*sin(x)+sin(x)*cos(x))
            Subfunctii = new List<Functie>();
            if (Operatie == '+' || Operatie == '*')
            {
                if (Functie2.Operatie.Value == Operatie.Value + 1 && (!Functie2.MinusESemn))
                {
                    if (Functie1.Operatie == Operatie) Subfunctii.AddRange(Functie1.Subfunctii);
                    else if (Functie1.Operatie.Value == Operatie.Value + 1 && !Functie1.MinusESemn)
                        Subfunctii.Add(Functie1.Subfunctii[0]);
                    else Subfunctii.Add(Functie1);
                    Subfunctii.Add(Functie2.Subfunctii[0]);
                    Functie t1 = new Functie(Operatie.OPERATIE, Subfunctii);
                    Subfunctii.Clear();
                    Subfunctii.Add(t1);
                    if (Functie1.Operatie.Value == Operatie.Value + 1 && !Functie1.MinusESemn)
                        Subfunctii.Add(new Functie('*', Functie1.Subfunctii[1], Functie2.Subfunctii[1]));
                    else
                        Subfunctii.Add(Functie2.Subfunctii[1]);
                    Operatie = new Operatie(Functie2.Operatie.OPERATIE);
                    ReconstruiesteCorp();
                }
                else
                {
                    if (Operatie == Functie1.Operatie)
                    {
                        Subfunctii = new List<Functie>(Functie1.Subfunctii);
                    }
                    else Subfunctii.Add(Functie1);

                    if (Operatie == Functie2.Operatie)
                    {
                        Subfunctii.AddRange(Functie2.Subfunctii);
                    }
                    else Subfunctii.Add(Functie2);
                }
                /* float TotalTerminals = Operatie.ElementNeutru();
                 for (int i=0;i<Subfunctii.Count;i++)
                 {
                     if (Subfunctii[i].FloatTerminal())
                     {
                         if(Operatie.ElementIdentitate()!=float.NaN &&  Subfunctii[i].ValoareTerminal==Operatie.ElementIdentitate())
                         {
                             Subfunctii.Clear();
                             Subfunctii.Add(new Functie(Operatie.ElementIdentitate()));
                             goto Done;
                         }
                         TotalTerminals = Operatie.Execute(TotalTerminals, Subfunctii[i].ValoareTerminal);
                         Subfunctii.Remove(Subfunctii[i]);
                         i--;
                     }
                 }
                 if(TotalTerminals!= Operatie.ElementNeutru())
                 {
                     Subfunctii.Add(new Functie(TotalTerminals));
                 }
                 if(Subfunctii.Count == 0)
                 {
                     Subfunctii = new List<Functie>();
                     Subfunctii.Add(new Functie(Operatie.ElementNeutru()));
                 }
             Done:;*/
                Functie1 = null;
                Functie2 = null;
            }
            else if (Operatie == '-' && !MinusESemn || Operatie == '/')
            {
                List<Functie> l1 = new List<Functie>();
                List<Functie> l2 = new List<Functie>();
                if ((Operatie != Functie1.Operatie || Functie1.MinusESemn) && (Operatie != Functie2.Operatie || Functie2.MinusESemn))
                {
                    l1.Add(Functie1);
                    l2.Add(Functie2);
                }
                else if ((Operatie == Functie1.Operatie && !Functie1.MinusESemn) && (Operatie == Functie2.Operatie && !Functie2.MinusESemn))//Operatie == Functie1.Operatie == Functie2.Operatie 
                {
                    if (Functie1.Subfunctii[0].Operatie.Value == Operatie.Value - 1)
                        l1.AddRange(Functie1.Subfunctii[0].Subfunctii);
                    else l1.Add(Functie1.Subfunctii[0]);

                    if (Functie2.Subfunctii[1].Operatie.Value == Operatie.Value - 1)
                        l1.AddRange(Functie2.Subfunctii[1].Subfunctii);
                    else l1.Add(Functie2.Subfunctii[1]);

                    if (Functie1.Subfunctii[1].Operatie.Value == Operatie.Value - 1)
                        l2.AddRange(Functie1.Subfunctii[1].Subfunctii);
                    else l2.Add(Functie1.Subfunctii[1]);

                    if (Functie2.Subfunctii[0].Operatie.Value == Operatie.Value - 1)
                        l2.AddRange(Functie2.Subfunctii[0].Subfunctii);
                    else l2.Add(Functie2.Subfunctii[0]);
                }
                else if (Operatie == Functie1.Operatie && !Functie1.MinusESemn)
                {
                    if (Functie1.Subfunctii[0].Operatie.Value == Operatie.Value - 1)
                        l1 = Functie1.Subfunctii[0].Subfunctii;
                    else l1.Add(Functie1.Subfunctii[0]);
                    if (Functie1.Subfunctii[1].Operatie.Value == Operatie.Value - 1)
                        l2 = Functie1.Subfunctii[1].Subfunctii;
                    else l2.Add(Functie1.Subfunctii[1]);
                    if (Functie2.Operatie.Value == Operatie.Value + 1)
                        l2.AddRange(Functie2.Subfunctii);
                    else l2.Add(Functie2);
                }
                else if (Operatie == Functie2.Operatie && !Functie2.MinusESemn)
                {
                    if (Functie1.Operatie.Value == Operatie.Value - 1)
                        l1.AddRange(Functie1.Subfunctii);
                    else l1.Add(Functie1);
                    if (Functie1.Subfunctii[0].Operatie.Value == Operatie.Value - 1)
                        l2 = Functie2.Subfunctii[0].Subfunctii;
                    else l2.Add(Functie2.Subfunctii[0]);
                    if (Functie2.Subfunctii[1].Operatie.Value == Operatie.Value - 1)
                        l1.AddRange(Functie2.Subfunctii[1].Subfunctii);
                    else
                        l1.Add(Functie2.Subfunctii[1]);
                }

                char oper; if (Operatie == '-') oper = '+'; else oper = '*';
                // Functie1 = new Functie(oper, l1);
                // Functie2 = new Functie(oper, l2);
                Subfunctii.Add(new Functie(oper, l1));
                Subfunctii.Add(new Functie(oper, l2));
                Functie1 = null;
                Functie2 = null;
            }
            else if (Operatie == '^')
            {
                List<Functie> l1 = new List<Functie>();
                List<Functie> l2 = new List<Functie>();
                if (Operatie != Functie1.Operatie && Operatie != Functie2.Operatie)
                {
                    // if (Functie1.AreNevoieDePAranteze('^'))
                    //    Functie1.IncadreazaInParanteze();
                    // if (Functie2.AreNevoieDePAranteze('^', false))
                    //   Functie2.IncadreazaInParanteze();
                    Subfunctii.Add(Functie1);
                    Subfunctii.Add(Functie2);
                }
                else if (Operatie == Functie1.Operatie && Operatie == Functie2.Operatie)
                {
                    l2.Add(Functie1.Subfunctii[1]);
                    l2.Add(Functie2);
                    Subfunctii.Add(Functie1.Subfunctii[0]);
                    Subfunctii.Add(new Functie('*', l2));
                }
                else if (Operatie == Functie1.Operatie)
                {
                    l2.Add(Functie1.Subfunctii[1]);
                    l2.Add(Functie2);
                    Subfunctii.Add(Functie1.Subfunctii[0]);
                    Subfunctii.Add(new Functie('*', l2));
                }
                else if (Operatie == Functie2.Operatie)
                {
                    Subfunctii.Add(Functie1);
                    Subfunctii.Add(Functie2);
                }
                Functie1 = null;
                Functie2 = null;
            }
            else if (Operatie != '0' && Operatie != 'X')
            {
                if (Operatie == '-' && MinusESemn && Functie1.Operatie == '-' && Functie1.MinusESemn)
                    Import(Functie1.Subfunctii[0]);
                else
                    Subfunctii.Add(Functie1);
                Functie1 = null;
                return;
            }
            SimplifyIdentities();
            ReconstruiesteCorp();
        }
        void SorteazaLista()
        {
            bool ok = false;
            Functie temp;
            if (Operatie != '+' && Operatie != '*') return;
            while (!ok)
            {
                ok = true;
                for (int i = 0; i < Subfunctii.Count - 1; i++)
                {

                    if (Subfunctii[i] > Subfunctii[i + 1])
                    {
                        temp = Subfunctii[i];
                        Subfunctii[i] = Subfunctii[i + 1];
                        Subfunctii[i + 1] = temp;
                        ok = false;
                    }
                }
            }
        }
        //Metode secundare
        string CorpulPrincipal(string s)
        {
            while (s[0] == '(' && s[s.Length - 1] == ')' && PerecheaParantezei0(s))
            {
                s = s.Remove(0, 1);
                s = s.Remove(s.Length - 1, 1);
            }
            if (s[0] == '-')
            {
                while (s[1] == '-') s = s.Remove(1, 1);
            }
            return s;
        }
        bool PerecheaParantezei0(string s)
        {
            int nrdeschise = 1;
            int i = 1;
            while (nrdeschise > 0 && i < s.Length) { if (s[i] == '(') nrdeschise++; else if (s[i] == ')') nrdeschise--; i++; }
            return i == s.Length;
        }
        bool InExteriorulParantezelor(int i, string s)
        {
            int des = 0;
            for (int j = i; j >= 0; j--)
            {
                if (s[j] == '(') des++;
                else if (s[j] == ')') des--;
            }
            if (des == 0)
                return true;
            else return false;
        }
        bool MinusEsteSemn(string s, int i)
        {
            if (i == 0) return true;
            if (s[i - 1] == '+' || s[i - 1] == '-' || s[i - 1] == '*' || s[i - 1] == '/' || s[i - 1] == '^' || s[i - 1] == '(')
                return true;
            return false;
        }
        bool AreNevoieDePAranteze(char OperandExterior, bool InainteaSemnului = true)
        {
            Operatie Exterior = new Operatie(OperandExterior);
            if (Exterior == '+') return false;
            else if (Exterior == '-')
            {
                if (Operatie == '-' && !InainteaSemnului) return true;
                else return false;
            }
            else if (Exterior == '*')
            {
                if (Operatie < Exterior) return true;
                return false;
            }
            else if (Exterior == '/')
            {
                if (Operatie < new Operatie('*')) return true;
                return false;
            }
            else if (Exterior == '^')
            {
                if (Operatie < Exterior) return true;
                return false;
            }
            return false;

        }
        void IncadreazaInParanteze()
        {
            corp = corp.Insert(0, "(");
            corp = corp.Insert(corp.Length, ")");
        }
        bool Equals(Functie f)
        {

            if (Operatie == f.Operatie && Subfunctii.Count == f.Subfunctii.Count)
            {
                if (Operatie == '0' && ValoareTerminal == f.ValoareTerminal) return true;
                else if (Operatie == 'X') return true;
                for (int i = 0; i < Subfunctii.Count; i++)
                {
                    if (!(Subfunctii[i] == f.Subfunctii[i])) return false;
                }
                return true;
            }
            return false;
        }
        void ReconstruiesteCorp()
        {
            if (Subfunctii != null)
            {
                foreach (var subfunctie in Subfunctii)
                    subfunctie?.ReconstruiesteCorp();
            }
            if (Operatie == 'X') corp = "x";
            else if (Operatie == '0')
            {
                if (ValoareTerminal == float.Parse(Convert.ToSingle(Math.E).ToString()))
                    corp = "e";
                else if (ValoareTerminal == float.Parse(Convert.ToSingle(Math.PI).ToString()))
                    corp = "π";
                else
                    corp = ValoareTerminal.ToString();
            }
            else if (Operatie.Value > 5 && Operatie.Value < 17)
            {
                //fctii trigonometrice
                corp = Operatie.UnarOperator() + Subfunctii[0].Corp + ")";
            }
            else
            {
                string MinusOptional = null;
                if (MinusESemn) MinusOptional = "-";
                if (Subfunctii[0].AreNevoieDePAranteze(Operatie.OPERATIE, true)) corp = MinusOptional + "(" + Subfunctii[0].Corp + ")";
                else corp = MinusOptional + Subfunctii[0].Corp;
            }
            for (int i = 1; i < Subfunctii.Count; i++) if (Subfunctii[i].AreNevoieDePAranteze(Operatie.OPERATIE, false)) corp += Operatie.OPERATIE + "(" + Subfunctii[i].Corp + ")"; else corp += Operatie.OPERATIE + Subfunctii[i].Corp;
        }
        //Operatori
        public static bool operator <(Functie f1, Functie f2)
        {
            if (f1.Operatie < f2.Operatie) return true;
            else if (f1.Operatie == f2.Operatie)
            {
                if (f1.Operatie == '0')
                {
                    return f1.ValoareTerminal < f2.ValoareTerminal;
                }
                //if (f1.Subfunctii.Count > f2.Subfunctii.Count) return true;//Ori asa, ori 2 cate doua comparate pana se termina una din ele
                if (f1.Subfunctii.Count == f2.Subfunctii.Count)
                {
                    int i;
                    for (i = 0; i < f1.Subfunctii.Count; i++)
                    {
                        if (i == f2.Subfunctii.Count) return false;
                        if (f1.Subfunctii[i] < f2.Subfunctii[i]) return true;
                        else if (f2.Subfunctii[i] < f1.Subfunctii[i]) return false;
                    }
                    if (i < f2.Subfunctii.Count - 1) return true;
                }
                return false;
            }
            return false;
        }
        public static bool operator >(Functie f1, Functie f2)
        {
            if (f1.Operatie > f2.Operatie) return true;
            else if (f1.Operatie == f2.Operatie)
            {
                if (f1.Operatie == '0')
                {
                    return f1.ValoareTerminal > f2.ValoareTerminal;
                }
                //if (f1.Subfunctii.Count > f2.Subfunctii.Count) return true;//Ori asa, ori 2 cate doua comparate pana se termina una din ele
                if (f1.Subfunctii.Count == f2.Subfunctii.Count)
                {
                    int i;
                    for (i = 0; i < f1.Subfunctii.Count; i++)
                    {
                        if (i == f2.Subfunctii.Count) return true;
                        if (f1.Subfunctii[i] > f2.Subfunctii[i]) return true;
                        else if (f2.Subfunctii[i] > f1.Subfunctii[i]) return false;
                    }
                    if (i < f2.Subfunctii.Count - 1) return false;
                }
                return false;
            }
            return false;
        }
        Functie Fork()
        {
            Functie New = new Functie();
            New.corp = corp;
            New.Derivata = Derivata?.Fork();
            New.Functie1 = Functie1?.Fork();
            New.Functie2 = Functie2?.Fork();
            New.Subfunctii = Subfunctii?.Select(s => s?.Fork()).ToList();
            New.ValoriSubfunctii = ValoriSubfunctii?.Select(s => s).ToList();
            New.LastValue = LastValue;
            New.IntermediarValue = IntermediarValue;
            New.Subderivate = Subderivate?.Select(s => s?.Fork()).ToList();
            New.Operatie = Operatie;
            New.MinusESemn = MinusESemn;
            New.Trigonometrica = Trigonometrica;
            New.ValoareTerminal = ValoareTerminal;
            New.PozitieOperatie = PozitieOperatie;
            New.Terminal = Terminal;
            New.XTerminal = XTerminal;
            New.ETerminal = ETerminal;
            New.PITerminal = PITerminal;
            return New;
        }
        void Import(Functie From)
        {
            corp = From.corp;
            Derivata = From.Derivata;
            Functie1 = From.Functie1;
            Functie2 = From.Functie2;
            Subfunctii = From.Subfunctii;
            ValoriSubfunctii = From.ValoriSubfunctii;
            LastValue = From.LastValue;
            IntermediarValue = From.IntermediarValue;
            Subderivate = From.Subderivate;
            Operatie = From.Operatie;
            MinusESemn = From.MinusESemn;
            Trigonometrica = From.Trigonometrica;
            ValoareTerminal = From.ValoareTerminal;
            PozitieOperatie = From.PozitieOperatie;
            Terminal = From.Terminal;
            XTerminal = From.XTerminal;
            ETerminal = From.ETerminal;
            PITerminal = From.PITerminal;
        }
        //Proprietati
        public Functie Derivata;
        Functie Functie1, Functie2, Derivata1, Derivata2;
        public List<Functie> Subfunctii;
        List<float> ValoriSubfunctii;
        public float LastValue = 0, IntermediarValue = 0;
        public List<Functie> Subderivate;
        Operatie Operatie;
        bool MinusESemn = false, Trigonometrica = false; float ValoareTerminal; int PozitieOperatie;
        public bool Terminal = false, XTerminal = false, ETerminal = false, PITerminal = false;
    }
}
/* Valori Operatie:
 * {
 *      sin s
 *      cos c
 *      tg t
 *      ctg u
 *      sqrt q
 *      ln n
 *      lg g
 *      
 *      URMEAZA:
 *      arcsin S
 *      arccos C
 *      arctg T
 *      arcctg U
 * }
 */
// REZOLVAT: Operatie trebuie schimbat din caracter in clasa
// REZOLVAT lg(x)'' == -lg(x)''
// REZOLVAT daca minus e semn trebuie sa puna paranteze obligatoriu fiindca -x^2 != -(x^2) SAU minus semn sa fie efectuat ultimul deci -x^2 == -(x^2)
//          Daca avem f-g g trebuie pus in paranteze daca g.Operatie ={-,+}
//          x^-1 != x^(-1)
//          (x^2/x^3)' wrong
//          Constructorii in anumite cazuri nu modifica Operatie: (exemplu neverificat) corp:(x+1)^1 ->x+1 ; Operatie: '^'->'^'!='+' 
//          reinlocuieste e.value, pi.value cu e si pi
// IDEE: a/b/c/d = (ad)/(bc), a/b*c/d=(ac)/(bd)
// ~IDEE: arccos, acsin, arctg, arccot,...sume,produse,logaritmi in alte baze
// NECESAR: XTerminal trebuie marcat cu 'X'
// NECESAR:a+(b-c) -> (a+b)-c
// POSIBIL: Corp->set->Creeaza(corp) poate fi cauza incetinirii functionarii dupa multe derivari
// NU FUNCTIONEAZA OREC: 1/sin(x^2)^(-1)