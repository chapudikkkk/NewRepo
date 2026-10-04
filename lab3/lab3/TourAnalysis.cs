using System;
using System.Collections.Generic;

namespace lab3
{
    public class TourAnalysis
    {
        public static double GetTotalToursCost(List<TouristTour> tours)
        {
            double totalCost = 0;
            if (tours == null) return 0;

            foreach (var tour in tours)
            {
                totalCost += tour.CalculateTotalPrice();
            }

            return totalCost;
        }

        public static double GetTotalCateringCost(List<TouristTour> tours)
        {
            double totalCatering = 0;
            if (tours == null) return 0;

            foreach (var tour in tours)
            {
                if (tour is SightseeingTour sightseeingTour)
                {
                    totalCatering += sightseeingTour.CalculateCateringCost();
                }
            }

            return totalCatering;
        }
    }
}