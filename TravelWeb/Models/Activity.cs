using System.Collections.Generic;
namespace TravelWeb.Models
{
    public class Activity
    {
        public string Time { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double Cost { get; set; }
        public List<Suggestion> Suggestions { get; set; }
        public class SuggestionItem
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
    }
}