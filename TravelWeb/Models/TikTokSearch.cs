using System;

namespace TravelWeb.Models
{
    public class TikTokSearch
    {
        public string Keyword { get; set; }
        public int SearchCount { get; set; }
        public DateTime LastSearch { get; set; }
    }
}
