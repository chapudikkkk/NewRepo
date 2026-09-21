using lab1.class_laba2;
namespace UnitTest;

[TestClass]
public class Calculation_abTest
{
    [TestMethod]
    public void Calculation_a_great_b()
    {
        var calc = new Calculation_ab(2, 1);
        Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => calc.Quantity());
    }

    [TestMethod]
    public void Calulation_a_equal_b()
    {
        var calc = new Calculation_ab(1, 1);
        var testRes = 0;
        var res = calc.Quantity();
        Assert.AreEqual(testRes, res);
    }

    [TestMethod]
    public void EvenNumbers()
    {
        var calc = new Calculation_ab(2, 6);
        var testRes = 0;
        var res = calc.Quantity();
        Assert.AreEqual(testRes, res);
    }

    [TestMethod]
    public void Odd_NoneDivisibleBy7()
    {
        var calc = new Calculation_ab(1, 5);
        var testRes = 0;
        var res = calc.Quantity();
        Assert.AreEqual(testRes, res);
    }

    [TestMethod]
    public void OddAndDivisibleBy7()
    {
        var calc = new Calculation_ab(1, 22); 
        var testRes = 2;
        var res = calc.Quantity();
        Assert.AreEqual(testRes, res);
    }

}

