using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimGraph
{
    class Operatie
    {
        public int Value;
        char OP;
        public char OPERATIE
        {
            get { return OP; }
            set
            {
                OP = value;
                switch (value)
                {
                    case '+': Value = 1; break;
                    case '-': Value = 2; break;
                    case '*': Value = 3; break;
                    case '/': Value = 4; break;
                    case '^': Value = 5; break;
                    case 's': Value = 6; break;
                    case 'c': Value = 7; break;
                    case 't': Value = 8; break;
                    case 'u': Value = 9; break;
                    case 'q': Value = 10; break;
                    case 'n': Value = 11; break;
                    case 'g': Value = 12; break;
                    case 'S': Value = 13; break;
                    case 'C': Value = 14; break;
                    case 'T': Value = 15; break;
                    case 'U': Value = 16; break;
                    case '0': Value = 17; break;
                    case 'E': Value = 18; break;
                    case 'P': Value = 19; break;
                    case 'X': Value = 20; break;
                }
            }
        }
        public float ElementNeutru()
        {
            switch (OPERATIE)
            {
                case '+':
                    return 0;
                case '*':
                    return 1;
                case '^':
                    return 1;
                case '/':
                    return 1;
                case '-':
                    return 0;
            }
            return 0;
        }
        public float ElementIdentitate()
        {
            switch (OPERATIE)
            {
                case '*':
                    return 0;
            }
            return float.NaN;
        }
        public string UnarOperator()
        {
            if (OPERATIE == '-') return "-";
            else if (OPERATIE == 's') return "sin(";
            else if (OPERATIE == 'c') return "cos(";
            else if (OPERATIE == 't') return "tg(";
            else if (OPERATIE == 'u') return "ctg(";
            else if (OPERATIE == 'S') return "arcsin(";
            else if (OPERATIE == 'C') return "arccos(";
            else if (OPERATIE == 'T') return "arctg(";
            else if (OPERATIE == 'U') return "arcctg(";
            else if (OPERATIE == 'q') return "sqrt(";
            else if (OPERATIE == 'n') return "ln(";
            else if (OPERATIE == 'g') return "lg(";
            return null;
        }
        public float Execute(float t1, float t2)
        {
            switch (OPERATIE)
            {
                case '+':
                    return t1+t2;
                case '*':
                    return t1*t2;
                case '^':
                    return Convert.ToSingle(Math.Pow(t1,t2));
                case '/':
                    return t1/t2;
                case '-':
                    return t1-t2;
            }
            return 0;
        }
        public Operatie(char c)
        {
            OPERATIE = c;
        }
        public static bool operator ==(Operatie o1, Operatie o2)
        {
            if (o1.Value == o2.Value) return true; return false;
        }
        public static bool operator !=(Operatie o1, Operatie o2)
        {
            if (o1.Value != o2.Value) return true; return false;
        }
        public static bool operator >(Operatie o1, Operatie o2)
        {
            if (o1.Value > o2.Value) return true; return false;
        }
        public static bool operator <(Operatie o1, Operatie o2)
        {
            if (o1.Value < o2.Value) return true; return false;
        }
        public static bool operator >=(Operatie o1, Operatie o2)
        {
            if (o1.Value >= o2.Value) return true; return false;
        }
        public static bool operator <=(Operatie o1, Operatie o2)
        {
            if (o1.Value <= o2.Value) return true; return false;
        }
        public static bool operator ==(Operatie o1, char c)
        {
            Operatie o2 = new Operatie(c);
            if (o1.Value == o2.Value) return true; return false;
        }
        public static bool operator !=(Operatie o1, char c)
        {
            Operatie o2 = new Operatie(c);
            if (o1.Value != o2.Value) return true; return false;
        }
        public static bool operator >(Operatie o1, char c)
        {
            Operatie o2 = new Operatie(c);
            if (o1.Value > o2.Value) return true; return false;
        }
        public static bool operator <(Operatie o1, char c)
        {
            Operatie o2 = new Operatie(c);
            if (o1.Value < o2.Value) return true; return false;
        }
        public static bool operator >=(Operatie o1, char c)
        {
            Operatie o2 = new Operatie(c);
            if (o1.Value >= o2.Value) return true; return false;
        }
        public static bool operator <=(Operatie o1, char c)
        {
            Operatie o2 = new Operatie(c);
            if (o1.Value <= o2.Value) return true; return false;
        }
    }
}
