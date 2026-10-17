using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class SeaCruise : TouristTour, IFuelCalculation
    {
        private string shipName = "";

        public string ShipName
        {
            get { return shipName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Назва корабля не може бути порожньою.", nameof(value));
                }
                shipName = value;
            }
        }

        public SeaCruise(string country, int days, double price, int peopleCount, string shipName): base(country, days, price, peopleCount)
        {
            ShipName = shipName;
        }

        public double CalculateFuel()
        {

            return Days * 7800.0;
        }

        public override string Book()
        {
            string baseInfo = base.Book();
            var protocol = new StringBuilder();
            protocol.AppendLine(baseInfo);
            protocol.AppendLine($"Лайнер: \"{ShipName}\"");
            protocol.AppendLine($"Витрати на паливо: {CalculateFuel()}");
            return protocol.ToString();
        }

        public double CalculateTotal (){
            return CalculateFuel()/ Days;
        }
    }
}
