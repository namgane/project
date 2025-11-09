namespace TravelWeb.Models
{
    public class CuisineItem
    {
        public string Province { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal AveragePrice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Popularity { get; set; } = 0; // Higher means more popular
    }
}


