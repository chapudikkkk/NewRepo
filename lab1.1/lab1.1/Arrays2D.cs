using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1._1
{
    internal class Arrays2D
    {
        public bool error = false;
        int[,] a;
        int x_length = 5;
        int y_length = 5;


        public int X_length
        {
            get { return x_length; }
            set { x_length = value; }
        }

        public int Y_length
        {
            get { return y_length; }
            set { y_length = value; }
        }


        public int this[int i, int j]
        {
            get
            {
                if (0 <= i && i < x_length && 0 <= j && j < y_length)
                    return a[i,j];
                else
                {
                    error = true;
                    return 0;
                }
            }
            set
            {
                if (0 <= i && i < x_length && 0 <= j && j < y_length)
                    a[i,j] = value;
                else
                    error = true;
            }
        }

        void generate()
        {
            for (int i = 0; i < x_length; i++)
            {
                for (int j = 0; j < y_length; j++)
                {
                    this[i, j] = i + j;
                }
            }
        }
        public Arrays2D(int a_length, int b_length)
        {
            X_length = a_length;
            Y_length = b_length;
            a = new int[x_length, y_length];
            generate();
        }

        public Arrays2D()
        {
            a = new int[x_length, y_length];
            generate();
        }

        public int[] Sum()
        {
            int[] rowSum = new int[x_length];
            for (int i = 0; i < x_length; i++)
            {
                int sum = 0;

                for (int j = 0; j < y_length; j++)
                {
                    sum += a[i, j];
                }
                rowSum[i] = sum;
            }
            return rowSum;
        }

    }
}
