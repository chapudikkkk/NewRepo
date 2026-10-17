using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class CityBusTour : TouristTour, IFuelCalculation
    {
        private double routeDistance;

        public double RouteDistance
        {
            get { return routeDistance; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Довжина маршруту повинна бути більшою за 0.", nameof(value));
                }
                routeDistance = value;
            }
        }

        public CityBusTour(string country, int days, double price, int peopleCount, double routeDistance): base(country, days, price, peopleCount)
        {
            RouteDistance = routeDistance;
        }

        public double CalculateFuel()
        {
            return RouteDistance * 15.0 * Days;
        }

        public override string Book()
        {
            string baseInfo = base.Book();
            var protocol = new StringBuilder();
            protocol.AppendLine(baseInfo);
            protocol.AppendLine($"Оглядовий автобусний тур (Маршрут: {RouteDistance} км)");
            protocol.AppendLine($"Витрати на паливо для автобуса: {CalculateFuel()}");
            return protocol.ToString();
        }
    }
}
