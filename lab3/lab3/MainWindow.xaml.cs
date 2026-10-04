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
        private List<TouristTour> _tours = new List<TouristTour>();

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
            if (CruiseFields == null || SightseeingFields == null) return;

            if (TourTypeComboBox.SelectedIndex == 0)
            {
                CruiseFields.Visibility = Visibility.Visible;
                SightseeingFields.Visibility = Visibility.Collapsed;
            }
            else
            {
                CruiseFields.Visibility = Visibility.Collapsed;
                SightseeingFields.Visibility = Visibility.Visible;
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

                TouristTour tour;

                if (TourTypeComboBox.SelectedIndex == 0)
                {
                    string shipName = ShipNameTextBox.Text;
                    tour = new SeaCruise(country, days, price, peopleCount, shipName);
                }
                else
                {
                    if (!int.TryParse(CitiesCountTextBox.Text, out int citiesCount))
                    {
                        MessageBox.Show("Будь ласка, введіть коректну кількість міст.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    tour = new SightseeingTour(country, days, price, peopleCount, citiesCount);
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
            double totalToursCost = TourAnalysis.GetTotalToursCost(_tours);
            double totalCatering = TourAnalysis.GetTotalCateringCost(_tours);

            TotalToursCostTextBlock.Text = $"{totalToursCost:C}";
            TotalCateringTextBlock.Text = $"{totalCatering:C}";
        }
    }
}