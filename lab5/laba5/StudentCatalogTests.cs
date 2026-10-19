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
    public void SortByName_ShouldSortStudentsAlphabetically()
    {
        var catalog = new StudentCatalog(_fileSaver, _fileLoader, _testFilePath);
        catalog.AddStudent(new Student("Шевченко Тарас", 2004, 10, "#3"));
        catalog.AddStudent(new Student("Андрієнко Андрій", 2005, 15, "#1"));
        catalog.AddStudent(new Student("Бойко Богдан", 2006, 12, "#2"));

        catalog.SortByName();

        Assert.AreEqual("Андрієнко Андрій", catalog.Students[0].FullName);
        Assert.AreEqual("Бойко Богдан", catalog.Students[1].FullName);
        Assert.AreEqual("Шевченко Тарас", catalog.Students[2].FullName);
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
}