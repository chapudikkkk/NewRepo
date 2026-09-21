using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.class_laba2
{
    public class Calculation_ab
    {
        int a, b;
        public Calculation_ab(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
        public Calculation_ab()
        {
            a = 0;
            b = 0;
        }
        public Calculation_ab(int number)
        {
            a = number;
            b = number;
        }
        public int A{ get { return a; } }
        public int B { get { return b; } }

        public int Quantity()
        {
            if (a > b)
            {
                throw new ArgumentOutOfRangeException("A should be less than B");
            }

            if (a == b) return 0;

            else
            {
                int count = 0;
                for (int i = a; i <= b; i++)
                {
                    if (i % 2 != 0 && i % 7 == 0)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
    }
}
