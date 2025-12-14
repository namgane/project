using System;

namespace TravelWeb.Models
{
    public class ReviewItem
    {
        public int CuisineId { get; set; }
        public string UserName { get; set; } = "?n danh";
        public int Stars { get; set; }   // 1–5
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
