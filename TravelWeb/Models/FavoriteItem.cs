namespace TravelWeb.Models
{
    public class FavoriteItem
    {
        public string Id { get; set; } = string.Empty; // unique key, e.g., cuisine:Province:Name
        public string Type { get; set; } = string.Empty; // cuisine, activity, hotel, etc.
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty; // deep link back to page
        public string ImageUrl { get; set; } = string.Empty;
    }
}


