using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.class_laba2
{
    public class Calculation_abc
    {
        private int a;
        private int b;
        private int c;

        public Calculation_abc(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public Calculation_abc()
        {
            a = 0;
            b = 0;
            c = 0;
        }
        public Calculation_abc(int number)
        {
            a = number;
            b = number;
            c = number;
        }

        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public double Calculate()
        {
            double p = a * b * c;
            if (a % 2 == 0 && b % 2 == 0 && c % 2 == 0)
            {
                return p;
            }
            else
            {
                return p * p; 
            }
        }
    }
}
