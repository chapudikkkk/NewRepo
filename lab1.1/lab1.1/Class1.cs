using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Resolvers;

namespace lab1._1
{
    internal class Tabul
    {
        public double[,] xy = new double[1000, 2];
        public int n = 0;
        
        private double f1(double x)
        {
            return Math.Log(x * x + 5);
        }

        public double f2(double x)
        {
            return Math.Sin(Math.Exp(x) + 2);
        }

        public double f3(double x)
        {
            return Math.Tan(5 * x + 1);
        }


        public void tab(double xn = -2.90, double xk = 26.45, double h = 0.25, double a = 0.8)
        {
            double x = xn, y;
            int i = 0;
            while (i <= xk)
            {
                if (x <= 0) { 
                    y = f1(x); 
                }
                else {
                    if ((x > 0) && (x < a))
                    {
                        y = f2(x);
                    }
                    else {
                        y = f3(x);
                    }
                }
                xy[i, 0] = x;
                xy[i, 1] = y;

                x = x + h;
                i++;
                
            }
            n = i;
        }
    }
}
