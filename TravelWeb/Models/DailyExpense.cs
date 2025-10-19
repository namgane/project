using System.Collections.Generic;

namespace TravelWeb.Models
{
    public class DailyExpense
    {
        public int DayNumber { get; set; }
        public List<Activity> Activities { get; set; }
        public double TotalCost => CalculateTotal();

        private double CalculateTotal()
        {
            double total = 0;
            foreach (var a in Activities)
                total += a.Cost;
            return total;
        }
    }
}
