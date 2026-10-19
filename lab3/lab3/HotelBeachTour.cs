using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class HotelBeachTour : TouristTour, ICateringCalculation
    {
        private string hotelName = "";

        public string HotelName
        {
            get { return hotelName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Назва готелю не може бути порожньою.", nameof(value));
                }
                hotelName = value;
            }
        }

        public HotelBeachTour(string country, int days, double price, int peopleCount, string hotelName): base(country, days, price, peopleCount)
        {
            HotelName = hotelName;
        }

        public double CalculateCateringCost()
        {
            return Days * PeopleCount * 800.0;
        }

        public override string Book()
        {
            string baseInfo = base.Book();
            var protocol = new StringBuilder();
            protocol.AppendLine(baseInfo);
            protocol.AppendLine($"Готель: \"{HotelName}\"");
            protocol.AppendLine($"Вартість харчування в готелі: {CalculateCateringCost()}");
            return protocol.ToString();
        }
    }
}   