using lab5;

namespace lab5;

[TestClass]
public class FileHeandlerTests
{

    private string _testFilePath;

    [TestInitialize]
    public void TestInitialize()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".json");
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
    public async Task SaveAsync_ShouldWriteDataToFile()
    {
        var students = new List<Student>
            {
                new Student("Іваненко Іван Іванович", 2005, 15, "#1"),
                new Student("Петренко Петро Петрович", 2004, 12, "#2")
            };
        var fileSaver = new JsonFileWriter();
        await fileSaver.SaveAsync(_testFilePath, students);
        Assert.IsTrue(File.Exists(_testFilePath), "Файл не було створено.");
        string fileContent = await File.ReadAllTextAsync(_testFilePath);
        Assert.IsFalse(string.IsNullOrWhiteSpace(fileContent), "Файл порожній.");
        var fileReader = new JsonFileReader();
        var loadedStudents = await fileReader.LoadAsync<Student>(_testFilePath);
        Assert.IsNotNull(loadedStudents, "Десеріалізація не вдалася.");
        Assert.AreEqual(2, loadedStudents.Count, "Кількість десеріалізованих об'єктів не збігається.");
    }

    [TestMethod]
    public async Task LoadAsync_ShouldReadDataFromFile()
    {
        var jsonContent = "[{\"FullName\":\"Іваненко Іван Іванович\",\"BirthYear\":2005,\"School\":15,\"Id\":\"ST-1\"}]";
        await File.WriteAllTextAsync(_testFilePath, jsonContent);
        var fileReader = new JsonFileReader();
        var loadedStudents = await fileReader.LoadAsync<Student>(_testFilePath);
        Assert.IsNotNull(loadedStudents, "Об'єкти не були завантажені.");
        Assert.AreEqual(1, loadedStudents.Count, "Завантажена некоректна кількість об'єктів.");
        Assert.AreEqual("Іваненко Іван Іванович", loadedStudents[0].FullName);
        Assert.AreEqual(2005, loadedStudents[0].BirthYear);
        Assert.AreEqual(15, loadedStudents[0].School);
        Assert.AreEqual("ST-1", loadedStudents[0].Id);
    }
    [TestMethod]
    public async Task LoadAsync_WhenFileDoesNotExist_ReturnsNull()
    {
        var fileReader = new JsonFileReader();
        var loadedStudents = await fileReader.LoadAsync<Student>(_testFilePath);
        Assert.IsNull(loadedStudents, "Повинен повертати null, якщо файл не існує.");
    }
}