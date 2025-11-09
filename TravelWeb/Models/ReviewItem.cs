using System;

namespace TravelWeb.Models
{
    public class ReviewItem
    {
        public string CuisineId { get; set; } = string.Empty; // cuisine:{Province}:{Name}
        public string DisplayName { get; set; } = string.Empty; // public name of reviewer
        public int Rating { get; set; } // 1..5
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}


