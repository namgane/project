using System.Collections.Generic;

namespace TravelWeb.Models
{
    public class CuisineProvinceViewModel
    {
        public string Province { get; set; } = string.Empty;
        public List<CuisineItem> Items { get; set; } = new List<CuisineItem>();
        public decimal OverallAveragePrice { get; set; }
        public Dictionary<string, double> AverageRatings { get; set; } = new Dictionary<string, double>();
        public Dictionary<string, int> RatingsCount { get; set; } = new Dictionary<string, int>();
    }
}


