using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    internal class OilCompany : IPriceObserver, IReport
    {
        public event Action<string> MessageGenerated;
        public event Action<string> ProfitCalculated;

        public double Profit { get; set; }

        public void OnPriceChanged(double price)
        {
            Profit = price * 100;

            MessageGenerated?.Invoke("Нафтова компанія: " + GetReport());
            ProfitCalculated?.Invoke("Прибуток перераховано: " + Profit);
        }

        public string GetReport()
        {
            return "Прибуток = " + Profit;
        }

        public void OnMarketClosed()
        {
            MessageGenerated?.Invoke("Нафтова компанія завершила роботу.");
        }
    }
}
