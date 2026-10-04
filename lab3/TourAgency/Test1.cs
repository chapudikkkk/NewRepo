using lab3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TourAgency.Tests
{
    [TestClass]
    public class SeaCruiseTests
    {
        [TestMethod]
        public void Constructor_WithValidData_InitializesCorrectly()
        {
            string country = "Італія";
            int days = 7;
            double price = 25000;
            int peopleCount = 2;
            string shipName = "Ocean Princess";

            var cruise = new SeaCruise(country, days, price, peopleCount, shipName);

            Assert.AreEqual(country, cruise.Country);
            Assert.AreEqual(days, cruise.Days);
            Assert.AreEqual(price, cruise.Price);
            Assert.AreEqual(peopleCount, cruise.PeopleCount);
            Assert.AreEqual(shipName, cruise.ShipName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_WithInvalidDays_ThrowsArgumentException()
        {
            var cruise = new SeaCruise("Італія", -5, 25000, 2, "Ocean Princess");
        }

        [TestMethod]
        public void CalculateTotalPrice_CalculatesCorrectlyForMultiplePeople()
        {
            var cruise = new SeaCruise("Італія", 10, 20000, 3, "Ocean Princess");
            double expectedTotal = 20000 * 3; // 

            double actualTotal = cruise.CalculateTotalPrice();

            Assert.AreEqual(expectedTotal, actualTotal);
        }
    }

    [TestClass]
    public class SightseeingTourTests
    {
        [TestMethod]
        public void CalculateCateringCost_CalculatesCorrectlyForGroup()
        {
            var tour = new SightseeingTour("Франція", 5, 18000, 4, 3);
            double expectedCatering = 5 * 3 * 4 * 500.0; 

            double actualCatering = tour.CalculateCateringCost();

            Assert.AreEqual(expectedCatering, actualCatering);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CitiesCount_WithZeroValue_ThrowsArgumentException()
        {
            var tour = new SightseeingTour("Німеччина", 4, 12000, 2, 0);
        }
    }

    [TestClass]
    public class TourAnalysisTests
    {
        [TestMethod]
        public void GetTotalCateringCost_ReturnsCorrectSum()
        {
            var tours = new List<TouristTour>
            {
                new SeaCruise("Греція", 10, 30000, 2, "Poseidon"),
                new SightseeingTour("Німеччина", 4, 12000, 2, 2),
                new SightseeingTour("Польща", 3, 8000, 3, 4)      
            };

            double totalCatering = TourAnalysis.GetTotalCateringCost(tours);

            Assert.AreEqual(26000.0, totalCatering);
        }
    }
}