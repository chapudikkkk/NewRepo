using lab7;
using System;
namespace TestProject1

{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void ChangePriceTest()
        {
            StockExchange exchange = new StockExchange(75);

            exchange.ChangePrice(90);

            Assert.AreEqual(90, exchange.OilPrice);
        }

        [TestMethod]
        public void InvalidPriceTest()
        {
            StockExchange exchange = new StockExchange(75);

            Assert.ThrowsException<Exception>(() =>
                exchange.ChangePrice(0));
        }

        [TestMethod]
        public void OilCompanyProfitTest()
        {
            OilCompany company = new OilCompany();

            company.OnPriceChanged(82);

            Assert.AreEqual(8200, company.Profit);
        }

        [TestMethod]
        public void InsuranceCompanyRiskTest()
        {
            InsuranceCompany company = new InsuranceCompany();

            company.OnPriceChanged(80);

            Assert.AreEqual(8.0, company.Risk, 0.001);
        }

        [TestMethod]
        public void TradingBotDecisionTest()
        {
            TradingBot bot = new TradingBot();

            bot.OnPriceChanged(70);

            Assert.AreEqual("Купити", bot.Decision);

            bot.OnPriceChanged(90);

            Assert.AreEqual("Продати", bot.Decision);
        }

        [TestMethod]
        public void PriceChangedEventTest()
        {
            StockExchange exchange = new StockExchange(75);

            int count = 0;

            exchange.PriceChanged += price => count++;
            exchange.PriceChanged += price => count++;
            exchange.PriceChanged += price => count++;

            exchange.ChangePrice(85);

            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void MarketClosedEventTest()
        {
            StockExchange exchange = new StockExchange(75);

            int count = 0;

            exchange.MarketClosed += () => count++;
            exchange.MarketClosed += () => count++;
            exchange.MarketClosed += () => count++;

            exchange.CloseMarket();

            Assert.AreEqual(3, count);
        }
    }
}
