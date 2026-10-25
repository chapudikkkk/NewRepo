namespace lab5;

[TestClass]
public class StudentCatalogTests
{
    private string _testFilePath;
    private IFileSaver _fileSaver;
    private IFileLoader _fileLoader;

    [TestInitialize]
    public void TestInitialize()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".json");
        _fileSaver = new JsonFileWriter();
        _fileLoader = new JsonFileReader();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        if (File.Exists(_testFilePath))
        {
            File.Delete(_testFilePath);
        }
    }

    [TestMethod]
    public void AddStudent_ShouldAddStudentToCatalog()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);
        var student = new Student("Іваненко Іван", 2005, 15, "#1");

        catalog.AddStudent(student);

        Assert.AreEqual(1, catalog.Students.Count);
        Assert.AreEqual(student, catalog.Students[0]);
    }

    [TestMethod]
    public void DeleteStudent_ShouldRemoveStudentFromCatalog()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);
        var student = new Student("Іваненко Іван", 2005, 15, "#1");

        catalog.AddStudent(student);
        catalog.DeleteStudent(student);

        Assert.AreEqual(0, catalog.Students.Count);
    }

    [TestMethod]
    public void SortByName_ShouldSortStudentsByYear()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);
        catalog.AddStudent(new Student("Шевченко Тарас", 2004, 10, "#3"));
        catalog.AddStudent(new Student("Андрієнко Андрій", 2005, 15, "#1"));
        catalog.AddStudent(new Student("Бойко Богдан", 2006, 12, "#2"));

        catalog.SortByBirthYear();

        Assert.AreEqual(2004, catalog.Students[0].BirthYear);
        Assert.AreEqual(2005, catalog.Students[1].BirthYear);
        Assert.AreEqual(2006, catalog.Students[2].BirthYear);
    }


    [TestMethod]
    public async Task SaveAndLoadAsync_ShouldPersistAndRestoreData()
    {
        var catalog1 = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);
        catalog1.AddStudent(new Student("Іваненко Іван", 2005, 15, "#1"));
        await catalog1.SaveAsync();

        var catalog2 = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);
        await catalog2.LoadAsync();

        Assert.AreEqual(1, catalog2.Students.Count);
        Assert.AreEqual("Іваненко Іван", catalog2.Students[0].FullName);
        Assert.AreEqual(2005, catalog2.Students[0].BirthYear);
        Assert.AreEqual(15, catalog2.Students[0].School);
        Assert.AreEqual("#1", catalog2.Students[0].Id);
    }

    [TestMethod]
    public void MaxSchoolTest()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);
        catalog.AddStudent(new Student("Шевченко Тарас", 2004, 1, "#3"));
        catalog.AddStudent(new Student("Андрієнко Андрій", 2005, 1, "#1"));
        catalog.AddStudent(new Student("Бойко Богдан", 2006, 1, "#2"));
        catalog.AddStudent(new Student("Бой Богдана", 2003, 2, "#2"));


        int resultSchool = catalog.MaxSchool();
        Assert.AreEqual(1, resultSchool);
    }


    [TestMethod]
    public void SearchStudents_WithPartOfName()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);

        var student1 = new Student("Іваненко Іван Іванович", 2005, 15, "#1");
        var student2 = new Student("Петренко Петро Петрович", 2004, 12, "#2");
        var student3 = new Student("Іваненко Олег Петрович", 2003, 15, "#3");

        catalog.AddStudent(student1);
        catalog.AddStudent(student2);
        catalog.AddStudent(student3);

        catalog.SearchStudents("Іваненко");

        Assert.AreEqual(2, catalog.Students.Count);
        Assert.AreEqual(student1, catalog.Students[0]);
        Assert.AreEqual(student3, catalog.Students[1]);
    }

    [TestMethod]
    public void SearchStudents_WithEmptyTerm()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);

        catalog.AddStudent(new Student("Іваненко Іван", 2005, 15, "#1"));
        catalog.AddStudent(new Student("Петренко Петро", 2004, 12, "#2"));

        catalog.SearchStudents(" ");

        Assert.AreEqual(2, catalog.Students.Count);
    }

    [TestMethod]
    public void GetUniqueSchools()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);

        catalog.AddStudent(new Student("Іваненко Іван", 2005, 15, "#1"));
        catalog.AddStudent(new Student("Петренко Петро", 2004, 12, "#2"));
        catalog.AddStudent(new Student("Бойко Олег", 2003, 15, "#3"));

        var result = catalog.GetUniqueSchools();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(12, result[0]);
        Assert.AreEqual(15, result[1]);
    }

    [TestMethod]
    public void FindThreeOldestStudents()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);

        catalog.AddStudent(new Student("Студент 1", 2006, 15, "#1"));
        catalog.AddStudent(new Student("Студент 2", 2001, 12, "#2"));
        catalog.AddStudent(new Student("Студент 3", 2004, 15, "#3"));
        catalog.AddStudent(new Student("Студент 4", 2002, 10, "#4"));

        catalog.FindThreeOldestStudents();

        Assert.AreEqual(3, catalog.Students.Count);
        Assert.AreEqual(2001, catalog.Students[0].BirthYear);
        Assert.AreEqual(2002, catalog.Students[1].BirthYear);
        Assert.AreEqual(2004, catalog.Students[2].BirthYear);
    }

    [TestMethod]
    public void CalculateAverageBirthYear()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);

        catalog.AddStudent(new Student("Студент 1", 2000, 15, "#1"));
        catalog.AddStudent(new Student("Студент 2", 2004, 12, "#2"));
        catalog.AddStudent(new Student("Студент 3", 2005, 10, "#3"));

        double result = catalog.CalculateAverageBirthYear();

        Assert.AreEqual(2003.0, result);
    }

    [TestMethod]
    public void CalculateAverageBirthYear_WhenCatalogIsEmpty()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);

        double result = catalog.CalculateAverageBirthYear();

        Assert.AreEqual(0.0, result);
    }

    [TestMethod]
    public void FindLongestFullName()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);
        catalog.AddStudent(new Student("Анна Коваль", 2004, 15, "1"));
        catalog.AddStudent(new Student("Олександр Шевченко", 2003, 12, "2"));
        catalog.AddStudent(new Student("Іван Петренко", 2005, 15, "3"));
        string result = catalog.FindLongestFullName();
        Assert.AreEqual("Олександр Шевченко", result);
    }
    [TestMethod]
    public void FindLongestFullName_WhenCatalogIsEmpty()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);
        string result = catalog.FindLongestFullName();
        Assert.AreEqual("Каталог порожній", result);
    }


    [TestMethod]
    public void SortByBirthYear()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);

        catalog.AddStudent(new Student("Студент 1", 2006, 15, "#1"));
        catalog.AddStudent(new Student("Студент 2", 2002, 12, "#2"));
        catalog.AddStudent(new Student("Студент 3", 2004, 10, "#3"));

        catalog.SortByBirthYear();

        Assert.AreEqual(2002, catalog.Students[0].BirthYear);
        Assert.AreEqual(2004, catalog.Students[1].BirthYear);
        Assert.AreEqual(2006, catalog.Students[2].BirthYear);
    }

    [TestMethod]
    public void FilterByBirthYear()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);

        catalog.AddStudent(new Student("Студент 1", 2003, 15, "#1"));
        catalog.AddStudent(new Student("Студент 2", 2005, 12, "#2"));
        catalog.AddStudent(new Student("Студент 3", 2006, 10, "#3"));

        catalog.FilterByBirthYear(2004);

        Assert.AreEqual(2, catalog.Students.Count);
        Assert.AreEqual(2005, catalog.Students[0].BirthYear);
        Assert.AreEqual(2006, catalog.Students[1].BirthYear);
    }



}