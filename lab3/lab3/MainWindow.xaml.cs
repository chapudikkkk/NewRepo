using System;
using System.Collections.Generic;
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

namespace lab3
{
    public partial class MainWindow : Window
    {
        private List<ITour> _tours = new List<ITour>();

        public MainWindow()
        {
            InitializeComponent();
            TourTypeComboBox.SelectedIndex = 0;
            SetInitialUiState();
        }

        private void TourTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetInitialUiState();
        }

        private void SetInitialUiState()
        {
            if (CruiseFields == null || SightseeingFields == null || BusFields == null || HotelFields == null) return;

            CruiseFields.Visibility = Visibility.Collapsed;
            SightseeingFields.Visibility = Visibility.Collapsed;
            BusFields.Visibility = Visibility.Collapsed;
            HotelFields.Visibility = Visibility.Collapsed;

            switch (TourTypeComboBox.SelectedIndex)
            {
                case 0: 
                    CruiseFields.Visibility = Visibility.Visible;
                    break;
                case 1: 
                    SightseeingFields.Visibility = Visibility.Visible;
                    break;
                case 2: 
                    BusFields.Visibility = Visibility.Visible;
                    break;
                case 3: 
                    HotelFields.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void BookTourButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string country = CountryTextBox.Text;

                if (!int.TryParse(DaysTextBox.Text, out int days))
                {
                    MessageBox.Show("Будь ласка, введіть коректну кількість днів.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(PriceTextBox.Text, out double price))
                {
                    MessageBox.Show("Будь ласка, введіть коректну ціну.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int peopleCount = 1;
                if (PeopleCountComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag != null)
                {
                    if (int.TryParse(selectedItem.Tag.ToString(), out int parsedCount))
                    {
                        peopleCount = parsedCount;
                    }
                }

                ITour tour;
                int selectedIndex = TourTypeComboBox.SelectedIndex;

                if (selectedIndex == 0) 
                {
                    string shipName = ShipNameTextBox.Text;
                    tour = new SeaCruise(country, days, price, peopleCount, shipName);
                }
                else if (selectedIndex == 1) 
                {
                    if (!int.TryParse(CitiesCountTextBox.Text, out int citiesCount))
                    {
                        MessageBox.Show("Будь ласка, введіть коректну кількість міст.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    tour = new SightseeingTour(country, days, price, peopleCount, citiesCount);
                }
                else if (selectedIndex == 2)
                {
                    if (!double.TryParse(RouteDistanceTextBox.Text, out double routeDistance))
                    {
                        MessageBox.Show("Будь ласка, введіть коректну дистанцію маршруту.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    tour = new CityBusTour(country, days, price, peopleCount, routeDistance);
                }
                else
                {
                    string hotelName = HotelNameTextBox.Text;
                    tour = new HotelBeachTour(country, days, price, peopleCount, hotelName);
                }

                _tours.Add(tour);

                string protocol = tour.Book();
                ToursListBox.Items.Add(protocol);

                UpdateAnalytics();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася неочікувана помилка: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateAnalytics()
        {
            double totalToursCost = 0;
            foreach (var t in _tours)
            {
                totalToursCost += t.CalculateTotalPrice();
            }

            double totalCatering = TourAnalysis.GetTotalCateringCost(_tours);
            double totalFuel = TourAnalysis.GetTotalFuelExpenses(_tours);

            TotalToursCostTextBlock.Text = $"{totalToursCost:C}";
            TotalCateringTextBlock.Text = $"{totalCatering:C}";
            TotalFuelTextBlock.Text = $"{totalFuel:C}";
        }
    }
}