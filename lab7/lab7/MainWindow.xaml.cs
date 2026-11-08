using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StockExchange exchange;
        private OilCompany oilCompany;
        private InsuranceCompany insuranceCompany;
        private TradingBot tradingBot;
        private bool isDecreasing = false;

        public MainWindow()
        {
            InitializeComponent();

            exchange = new StockExchange(75);
            oilCompany = new OilCompany();
            insuranceCompany = new InsuranceCompany();
            tradingBot = new TradingBot();

            exchange.PriceChanged += oilCompany.OnPriceChanged;
            exchange.PriceChanged += insuranceCompany.OnPriceChanged;
            exchange.PriceChanged += tradingBot.OnPriceChanged;

            exchange.MarketClosed += oilCompany.OnMarketClosed;
            exchange.MarketClosed += insuranceCompany.OnMarketClosed;
            exchange.MarketClosed += tradingBot.OnMarketClosed;

            oilCompany.MessageGenerated += LogMessage;
            insuranceCompany.MessageGenerated += LogMessage;
            tradingBot.MessageGenerated += LogMessage;

            oilCompany.ProfitCalculated += LogMessage;
            insuranceCompany.RiskCalculated += LogMessage;
            tradingBot.TradeExecuted += LogMessage;
        }

        private void btnChangePrice_Click(object sender, RoutedEventArgs e)
        {
            logListBox.Items.Clear();

            if (double.TryParse(txtPrice.Text, out double price))
            {
                try
                {
                    exchange.ChangePrice(price);
                }
                catch (Exception ex)
                {
                    LogMessage("Помилка: " + ex.Message);
                }
            }
            else
            {
                LogMessage("Введіть коректну ціну.");
            }
        }

        private void btnCloseMarket_Click(object sender, RoutedEventArgs e)
        {
            exchange.CloseMarket();
        }

        private void LogMessage(string message)
        {
            logListBox.Items.Add(
                $"[{DateTime.Now:HH:mm:ss}] {message}");

            logListBox.ScrollIntoView(
                logListBox.Items[logListBox.Items.Count - 1]);
        }

        private async void btnDecreasePrice_Click(object sender, RoutedEventArgs e)
        {
            if (isDecreasing)
                return;

            isDecreasing = true;
            btnDecreasePrice.IsEnabled = false;

            while (exchange.OilPrice > 50)
            {
                double newPrice = exchange.OilPrice - 5;

                if (newPrice < 50)
                    newPrice = 50;

                exchange.ChangePrice(newPrice);

                await Task.Delay(1000);
            }

            LogMessage("Досягнуто мінімальної ціни нафти.");

            isDecreasing = false;
            btnDecreasePrice.IsEnabled = true;
        }
    }
}