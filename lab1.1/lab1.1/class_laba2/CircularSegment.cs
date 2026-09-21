using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.class_laba2
{
    public class CircularSegment
    {
        double r, l;
        public CircularSegment(double r, double l)
        {
            if (r <= 0 || l <= 0)
                throw new ArgumentException("Радіус и хорда повинні бути додатними");
            if (l > 2 * r)
                throw new ArgumentException("Хорда не може бути більшою за діаметр");

            this.r = r;
            this.l = l;
        }
        public CircularSegment(double number)
        {
            this.r = number;
            this.l = number;
        }
        public CircularSegment()
        {
            this.r = 1;
            this.l = 1;
        }
        public double R { get; set; }
        public double A { get; set; }

        public double GetCentralAngle()
        {
            return 2 * Math.Asin(l / (2 * r));
        }
        public double GetArcLength()
        {
            return r * GetCentralAngle();
        }
        public double GetPerimeter()
        {
            return GetArcLength() + l;
        }
        public double GetArea()
        {
            double alpha = GetCentralAngle();
            return 0.5 * r * r * (alpha - Math.Sin(alpha));
        }
    }
}
