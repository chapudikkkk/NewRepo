
using Microsoft.EntityFrameworkCore;
using srr1.Date;
using srr1.Services;
using srr1.Repositories;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace srr1
{
    public partial class MainWindow : Window
    {
        private string _jsonPath;
        private string _dbPath;
        private ParkingCatalog _catalog = null!;

        public MainWindow()
        {
            InitializeComponent();

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            _jsonPath = Path.Combine(documentsPath, "parking.json");
            _dbPath = Path.Combine(documentsPath, "parking.db");

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDataAndRefreshUiAsync();
        }

        private async Task LoadDataAndRefreshUiAsync()
        {
            try
            {
                if (!File.Exists(_jsonPath))
                {
                    MessageBox.Show($"Файл JSON не знайдено:\n{_jsonPath}", "Помилка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlite($"Data Source={_dbPath}")
                    .Options;

                var dbContext = new AppDbContext(options);

                var jsonReader = new JsonFileReader();
                var initializer = new DbInitializer(dbContext, jsonReader);

                await initializer.InitializeAsync(_jsonPath);

                IParkingRepository repository = new SQLiteParkingRepository(dbContext);
                _catalog = new ParkingCatalog(repository);

                var zones = await _catalog.GetCatalogAsync();
                var sessions = zones.SelectMany(z => z.ParkingSessions).ToList();

                ParkingDataGrid.ItemsSource = sessions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка завантаження даних із бази SQLite:\n{ex.Message}",
                    "Помилка шару даних", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ParkingDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Payments" ||
                e.PropertyName == "Penalties" ||
                e.PropertyName == "ParkingZone")
            {
                e.Cancel = true;
            }
        }

        private async void ShowDataButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadDataAndRefreshUiAsync();
        }

        private async void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(MinAmountTextBox.Text, out decimal minAmount))
            {
                MessageBox.Show("Введіть коректну суму платежів!", "Помилка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var sessions = await _catalog.FilterSessionsAsync(minAmount);
                ParkingDataGrid.ItemsSource = sessions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка фільтрації:\n{ex.Message}");
            }
        }

        private async void PaymentStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await _catalog.GetPaymentStatisticsAsync();
                ParkingDataGrid.ItemsSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка отримання статистики платежів:\n{ex.Message}");
            }
        }

        private async void ZoneStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await _catalog.GetZoneStatisticsAsync();
                ParkingDataGrid.ItemsSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка отримання статистики зон:\n{ex.Message}");
            }
        }

        private async void AnomaliesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await _catalog.GetAnomaliesAsync();
                ParkingDataGrid.ItemsSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка пошуку аномалій:\n{ex.Message}");
            }
        }

        private async void PenaltyStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await _catalog.GetPenaltyStatisticsAsync();
                ParkingDataGrid.ItemsSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка отримання статистики штрафів:\n{ex.Message}");
            }
        }
    }
}
