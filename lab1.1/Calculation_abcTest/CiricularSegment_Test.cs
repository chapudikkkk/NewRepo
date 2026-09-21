using lab1.class_laba2;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace UnitTest;

[TestClass]
public class CiricularSegment_Test
{
     [TestMethod]
        public void NegativeDimensions()
        {
            Assert.ThrowsException<ArgumentException>(() => new CircularSegment(-5, 12));
        }
    [TestMethod]
        public void DiametrSmallerThanTheChord()
        {
        Assert.ThrowsException<ArgumentException>(() => new CircularSegment(5, 12));
        }

    [TestMethod]
        public void GetCentralAngle()
        {
        var segment = new CircularSegment(10, 10);
        double testResAngle = Math.PI / 3;
        double resAngle = segment.GetCentralAngle();
        Assert.AreEqual(testResAngle, resAngle, 0.0001);
        }

        [TestMethod]
        public void GetAreaAndPerimeter()
        {
        var segment = new CircularSegment(10, 10);

        double testResArc = 10 * (Math.PI / 3);
        double testResPerimeter = testResArc + 10;
        double testResArea = 0.5 * 100 * (Math.PI / 3 - Math.Sin(Math.PI / 3));

        double resArc = segment.GetArcLength();
        double resPerimeter = segment.GetPerimeter();
        double resArea = segment.GetArea();

        Assert.AreEqual(testResArc, resArc, 0.0001);
        Assert.AreEqual(testResPerimeter, resPerimeter, 0.0001);
        Assert.AreEqual(testResArea, resArea, 0.0001);
        }
}
