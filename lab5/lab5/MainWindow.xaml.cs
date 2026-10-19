using System;
using System.IO;
using System.Windows;

namespace lab5
{
    public partial class MainWindow : Window
    {
        private StudentCatalog _catalog;
        private readonly string _filePath;

        public MainWindow()
        {
            InitializeComponent();

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _filePath = Path.Combine(documentsPath, "students.json");

            _catalog = new StudentCatalog(new JsonFileWriter(), new JsonFileReader(), _filePath);

            StudentsDataGrid.ItemsSource = _catalog.Students;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fullName = FullNameTextBox.Text;
                string id = IdTextBox.Text;

                if (!int.TryParse(YearTextBox.Text, out int year))
                {
                    throw new Exception("Рік народження має бути числом.");
                }
                if (!int.TryParse(SchoolTextBox.Text, out int school))
                {
                    throw new Exception("Номер школи має бути числом.");
                }

                var newStudent = new Student(fullName, year, school, id);
                _catalog.AddStudent(newStudent);

                FullNameTextBox.Clear();
                YearTextBox.Clear();
                SchoolTextBox.Clear();
                IdTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDataGrid.SelectedItem is Student selectedStudent)
            {
                _catalog.DeleteStudent(selectedStudent);
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть студента для видалення.", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            _catalog.SortByBirthYear();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            _catalog.FilterByBirthYear(2004);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await _catalog.SaveAsync();
            MessageBox.Show("Каталог успішно збережено.", "Збереження", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            await _catalog.LoadAsync();
            MessageBox.Show("Каталог успішно завантажено.", "Завантаження", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}