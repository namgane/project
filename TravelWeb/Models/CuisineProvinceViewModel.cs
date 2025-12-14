using System.Collections.Generic;

namespace TravelWeb.Models
{
    public class CuisineProvinceViewModel
    {
        // Trang Index
        public List<string> Provinces { get; set; } = new();
        public Dictionary<string, List<string>> ProvincesByRegion { get; set; } = new();

        // Trang Province
        public string Province { get; set; } = string.Empty;
        public List<CuisineItem> Items { get; set; } = new();

        // Giá
        public decimal OverallAveragePrice { get; set; }
        public decimal PriceMin { get; set; }
        public decimal PriceMax { get; set; }

        // Ðánh giá
       
        public Dictionary<string, double> AverageRatings { get; set; } = new();
        public Dictionary<string, int> RatingsCount { get; set; } = new();
        public Dictionary<string, List<ReviewItem>> Reviews { get; set; } = new();

        public int Total => Items.Count;
    }
}
