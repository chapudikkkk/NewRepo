using System;
using System.Collections.Generic;

namespace lab3
{
    public class TourAnalysis
    {
        public static double GetTotalCateringCost(List<ITour> tours)
        {
            double totalCatering = 0;
            if (tours == null) return 0;

            foreach (var tour in tours)
            {
                if (tour is ICateringCalculation cateringTour)
                {
                    totalCatering += cateringTour.CalculateCateringCost();
                }
            }

            return totalCatering;
        }

        public static double GetTotalFuelExpenses(List<ITour> tours)
        {
            double totalFuel = 0;
            if (tours == null) return 0;

            foreach (var tour in tours)
            {
                if (tour is IFuelCalculation fuelTour)
                {
                    totalFuel += fuelTour.CalculateFuel();
                }
            }

            return totalFuel;
        }
    }
}