using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using lab5;

namespace laba5
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void Constructor_WithValidArguments_CreatesStudentObject()
        {
            string fullName = "Іваненко Іван Іванович";
            int birthYear = 2005;
            int school = 15;
            string id = "#123";

            var student = new Student(fullName, birthYear, school, id);

            Assert.AreEqual(fullName, student.FullName);
            Assert.AreEqual(birthYear, student.BirthYear);
            Assert.AreEqual(school, student.School);
            Assert.AreEqual(id, student.Id);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Constructor_WithInvalidFullName_ThrowsArgumentException(string invalidName)
        {
            Assert.ThrowsException<ArgumentException>(() => new Student(invalidName, 2005, 15, "#123"));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void Constructor_WithInvalidId_ThrowsArgumentException(string invalidId)
        {
            Assert.ThrowsException<ArgumentException>(() => new Student("Іван", 2005, 15, invalidId));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-15)]
        public void Constructor_WithInvalidSchool_ThrowsArgumentException(int invalidSchool)
        {
            Assert.ThrowsException<ArgumentException>(() => new Student("Іван", 2005, invalidSchool, "#123"));
        }

        [TestMethod]
        public void Constructor_WithFutureBirthYear_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Student("Іван", DateTime.Now.Year + 1, 15, "#123"));
        }

        [TestMethod]
        public void Constructor_WithLastBirthYear_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Student("Іван", 1920, 15, "#123"));
        }

        [TestMethod]
        public void Compare_WhenStudentsHaveDifferentBirthYears_ReturnsCorrectOrder()
        {
            var student1 = new Student("Іваненко І.", 2003, 1, "#1");
            var student2 = new Student("Петренко П.", 2005, 2, "#2");
            var comparer = new StudentFullNameComparer();
            int result = comparer.Compare(student1, student2);
            Assert.IsTrue(result < 0, "Студент з меншим роком народження має йти першим.");
        }
    }
}