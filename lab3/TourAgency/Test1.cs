using lab3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TourAgency.Tests
{
    [TestClass]
    public class SeaCruiseTests
    {
        [TestMethod]
        public void InitializesCorrectly()
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
        public void ThrowsArgumentException()
        {
            var cruise = new SeaCruise("Італія", -5, 25000, 2, "Ocean Princess");
        }

        [TestMethod]
        public void CalculatesCorrectlyForMultiplePeople()
        {
            var cruise = new SeaCruise("Італія", 10, 20000, 3, "Ocean Princess");
            double expectedTotal = 20000 * 3;

            double actualTotal = cruise.CalculateTotalPrice();

            Assert.AreEqual(expectedTotal, actualTotal);
        }


        [TestMethod]
        public void CalculateFuel()
        {
            var cruise = new SeaCruise("Італія", 10, 20000, 3, "Ocean Princess");
            double expectedFuel = 78000.0;

            double actualTotal = cruise.CalculateFuel();

            Assert.AreEqual(expectedFuel, actualTotal);
        }
    }

    [TestClass]
    public class SightseeingTourTests
    {
        [TestMethod]
        public void CalculatesCorrectlyForGroup()
        {
            var tour = new SightseeingTour("Франція", 5, 18000, 4, 3);
            double expectedCatering = 5 * 3 * 4 * 500.0;

            double actualCatering = tour.CalculateCateringCost();

            Assert.AreEqual(expectedCatering, actualCatering);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsArgumentException()
        {
            var tour = new SightseeingTour("Німеччина", 4, 12000, 2, 0);
        }
    }

    [TestClass]
    public class TourAnalysisTests
    {
        [TestMethod]
        public void ReturnsCorrectSum()
        {
            List<ITour> tours = new List<ITour>
        {
        new SeaCruise("Греція", 10, 30000, 2, "Poseidon"),
        new SightseeingTour("Німеччина", 4, 12000, 2, 2),
        new SightseeingTour("Польща", 3, 8000, 3, 4)
        };

            double totalCatering = TourAnalysis.GetTotalCateringCost(tours);
            Assert.AreEqual(26000.0, totalCatering);
        }


        [TestMethod]
        public void CityBusTourFuelCalculation()
        {
            var busTour = new CityBusTour("Україна", 5, 1000, 2, 200.0);
            double fuelCost = busTour.CalculateFuel();
            //200 км * 15 * 5 днів
            Assert.AreEqual(15000.0, fuelCost);
        }

        [TestMethod]
        public void HotelBeachTour_CateringCalculation()
        {
            var hotelTour = new HotelBeachTour("Єгипет", 7, 12000, 2, "Hilton", 5);
            double cateringCost = hotelTour.CalculateCateringCost();
            //7 днів * 2 особи * 800 грн
            Assert.AreEqual(11200.0, cateringCost);
        }

    }
}


