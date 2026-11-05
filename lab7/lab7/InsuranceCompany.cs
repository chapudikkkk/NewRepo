using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    internal class InsuranceCompany : IPriceObserver, IReport
    {
        public event Action<string> MessageGenerated;
        public event Action<string> RiskCalculated;

        public double Risk { get; private set; }

        public void OnPriceChanged(double price)
        {
            Risk = price * 0.1;

            MessageGenerated?.Invoke("Страхова компанія: " + GetReport());
            RiskCalculated?.Invoke("Ризики перераховано: " + Risk);
        }

        public string GetReport()
        {
            return "Ризик = " + Risk;
        }

        public void OnMarketClosed()
        {
            MessageGenerated?.Invoke("Страхова компанія завершила роботу.");
        }
    }
}
