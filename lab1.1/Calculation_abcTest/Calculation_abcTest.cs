using lab1.class_laba2;
namespace UnitTest;

[TestClass]
public class Calculation_abcTest
{
    [TestMethod]
    public void Calculation_all_even()
    {
        var calc = new Calculation_abc(2, 4, 6);

        var testRes = 48;

        var res = calc.Calculate();

        Assert.AreEqual(testRes, res);
    }

    [TestMethod]

    public void Calculation_notAll_even()
    {
        var calc = new Calculation_abc(2, 4, 5);

        var testRes = 1600;

        var res = calc.Calculate();

        Assert.AreEqual(testRes, res);
    }

    [TestMethod]

    public void CalculationSum()
    {
        var calc = new Calculation_abc(-1, -2, 5);

        var testRes = 2;

        var res = calc.Calculate();

        Assert.AreEqual(testRes, res);
    }



}
