using System.Collections.Generic;

namespace TravelWeb.Models
{
    public class TripPlan
    {
        public string Destination { get; set; }
        public int SuggestedDays { get; set; }
        public double TotalBudget { get; set; }
        public double EstimatedTotalCost { get; set; }
        public List<DailyExpense> DailyPlans { get; set; }
        public List<string> TransportOptions { get; set; }
        public List<string> HotelSuggestions { get; set; }
    }
}
