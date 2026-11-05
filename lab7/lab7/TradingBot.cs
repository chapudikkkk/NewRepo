using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    internal class TradingBot : IPriceObserver, IReport, ITrading
    {
        public event Action<string> MessageGenerated;
        public event Action<string> TradeExecuted;

        public string Decision { get; set; }

        public void OnPriceChanged(double price)
        {
            Decision = MakeDecision(price);

            MessageGenerated?.Invoke("Торговий бот: " + GetReport());
            TradeExecuted?.Invoke("Виконано торгову операцію: " + Decision);
        }

        public string MakeDecision(double price)
        {
            if (price < 80)
                return "Купити";
            else
                return "Продати";
        }

        public string GetReport()
        {
            return "Рішення = " + Decision;
        }

        public void OnMarketClosed()
        {
            MessageGenerated?.Invoke("Торговий бот завершив роботу.");
        }
    }
}
