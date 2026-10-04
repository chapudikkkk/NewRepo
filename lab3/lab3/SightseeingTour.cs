using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace lab3
    {
        public class SightseeingTour : TouristTour
        {
            private int citiesCount;

            public int CitiesCount
            {
                get { return citiesCount; }
                set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentException("Кількість міст повинна бути більшою за 0.", nameof(value));
                    }
                    citiesCount = value;
                }
            }

            public SightseeingTour(string country, int days, double price, int peopleCount, int citiesCount): base(country, days, price, peopleCount)
            {
                CitiesCount = citiesCount;
            }

            public double CalculateCateringCost()
            {
                return Days * CitiesCount * PeopleCount * 350.0;
            }

            public override string Book()
            {
                string baseInfo = base.Book();
                var protocol = new StringBuilder();
                protocol.AppendLine(baseInfo);
                protocol.AppendLine($"Міст у програмі: {CitiesCount}");
                protocol.AppendLine($"Вартість харчування на групу ({PeopleCount} осіб): {CalculateCateringCost():C}");
                return protocol.ToString();
            }
        }
    }

