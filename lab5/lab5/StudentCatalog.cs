using lab5;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class StudentCatalog
    {
        private readonly IFileSaver _fileSaver;
        private readonly IFileLoader _fileLoader;
        private readonly string _filePath;
        public ObservableCollection<Student> Students { get; }
        private List<Student> _allStudents;
        public StudentCatalog(IFileSaver fileSaver, IFileLoader fileLoader, string filePath)
        {
            _fileSaver = fileSaver;
            _fileLoader = fileLoader;
            _filePath = filePath;
            Students = new ObservableCollection<Student>();
            _allStudents = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
            _allStudents.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            Students.Remove(student);
            _allStudents.Remove(student);
        }

        public void SortByBirthYear()
        {
            var sortedStudents = _allStudents
            .OrderBy(s => s.BirthYear)
            .ToList();
            UpdateVisibleCollection(sortedStudents);
        } 
        public void FilterByBirthYear(int year)
        {
            var filtered = _allStudents.Where(s => s.BirthYear > year).ToList();
            UpdateVisibleCollection(filtered);
        }

        public void ResetFilter()
        {
            UpdateVisibleCollection(_allStudents);
        }

        private void UpdateVisibleCollection(List<Student> newList)
        {
            Students.Clear();
            foreach (var student in newList)
            {
                Students.Add(student);
            }
        }
        public void SearchStudents(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                ResetFilter();
                return;
            }

            var searchResult = _allStudents
                .Where(s =>
                    s.FullName.Contains(searchTerm,
                        StringComparison.OrdinalIgnoreCase) ||
                    s.Id.Contains(searchTerm,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();

            UpdateVisibleCollection(searchResult);
        }

        public List<int> GetUniqueSchools()
        {
            var uniqueSchools = _allStudents
                .Select(s => s.School)
                .Distinct()
                .OrderBy(s => s)
                .ToList();

            return uniqueSchools;
        }

        public void FindThreeOldestStudents()
        {
            var oldestStudents = _allStudents
                .OrderBy(s => s.BirthYear)
                .ThenBy(s => s.FullName)
                .Take(3)
                .ToList();

            UpdateVisibleCollection(oldestStudents);
        }

        public double CalculateAverageBirthYear()
        {
            if (_allStudents.Count == 0)
            {
                return 0;
            }

            return _allStudents.Average(s => s.BirthYear);
        }

        public string FindLongestFullName()
        {
            if (_allStudents.Count == 0)
            {
                return "Каталог порожній";
            }

            return _allStudents.OrderByDescending(s => s.FullName.Length).First().FullName;
        }

        public int MaxSchool()
        {
            if (_allStudents.Count == 0)
            {
                return 0;
            }

            return _allStudents
                .GroupBy(s => s.School)
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key)
                .First()
                .Key;
        }

        public async Task SaveAsync()
        {
            await _fileSaver.SaveAsync(_filePath, _allStudents);
        }

        public async Task LoadAsync()
        {
            var loadedStudents =
                await _fileLoader.LoadAsync<Student>(_filePath);

            if (loadedStudents != null)
            {
                _allStudents = loadedStudents;
                UpdateVisibleCollection(_allStudents);
            }
        }
    }   
        
}