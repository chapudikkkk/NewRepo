using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    internal delegate void PriceEventHandler(double price);
    internal class StockExchange
    {
        public event PriceEventHandler PriceChanged;
        public event Action MarketClosed;

        public double OilPrice { get; private set; }

        public StockExchange(double price)
        {
            OilPrice = price;
        }

        public void ChangePrice(double price)
        {
            if (price <= 0)
                throw new Exception("Ціна повинна бути більшою за 0");

            OilPrice = price;
            PriceChanged?.Invoke(price);
        }

        public void CloseMarket()
        {
            MarketClosed?.Invoke();
        }
    }
}
