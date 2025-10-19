using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelWeb.Models
{
    public static class TikTokSearchService
    {
        private static List<TikTokSearch> searchHistory = new List<TikTokSearch>();

        public static void AddSearch(string keyword)
        {
            var existing = searchHistory.FirstOrDefault(s => s.Keyword.Equals(keyword, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                existing.SearchCount++;
                existing.LastSearch = DateTime.Now;
            }
            else
            {
                searchHistory.Add(new TikTokSearch
                {
                    Keyword = keyword,
                    SearchCount = 1,
                    LastSearch = DateTime.Now
                });
            }
        }

        public static List<TikTokSearch> GetTopSearchesThisMonth(int top = 5)
        {
            var now = DateTime.Now;
            return searchHistory
                .Where(s => s.LastSearch.Month == now.Month && s.LastSearch.Year == now.Year)
                .OrderByDescending(s => s.SearchCount)
                .Take(top)
                .ToList();
        }
    }
}
