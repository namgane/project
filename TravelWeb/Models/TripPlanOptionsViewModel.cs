
namespace TravelWeb.Models
{
    public class TripPlanOptionsViewModel
    {
        public List<TripPlan> Plans { get; set; } = new List<TripPlan>();
        public int SelectedIndex { get; set; } = 0;

        public TripPlan? SelectedPlan
        {
            get
            {
                if (Plans == null || Plans.Count == 0) return null;
                if (SelectedIndex < 0 || SelectedIndex >= Plans.Count) return Plans[0];
                return Plans[SelectedIndex];
            }
        }

        public bool IsVegetarian { get; set; } = false;
    }
}